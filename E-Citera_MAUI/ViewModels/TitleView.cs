using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Messaging;
using E_Citera_MAUI.Messages;

namespace E_Citera_MAUI.ViewModels;

// For are note on the purpose of 'TitleView' and its View 'MainPage', see: 'MainPage.xaml.cs'
public partial class TitleView : ObservableObject, IRecipient<AddItemMessage>, IRecipient<ItemDeletedMessage>
{
    public ObservableCollection<Title> TitleList { get; set; }
    public ObservableCollection<Title> SearchResults { get; private set; }

    [ObservableProperty]
    public bool shouldRefresh;

    [ObservableProperty]
    private string searchText;

    [ObservableProperty]
    private Title selectedFromSearch;

    [ObservableProperty]
    private bool searchShouldRefresh;

    [ObservableProperty]
    private string searchInputText;

    public TitleView()
    {
        RegisterMessages();
        InitializeCollections();        
        Load_TitleList();        
    }

    private void InitializeCollections()
    {
        TitleList = new ObservableCollection<Title>();
        SearchResults = new ObservableCollection<Title>();
    }

    private void RegisterMessages()
    {
        WeakReferenceMessenger.Default.Register<AddItemMessage>(this);
        WeakReferenceMessenger.Default.Register<ItemDeletedMessage>(this);
    }

    // Fetches all titles currently in the database which are displayed
    // as a CollectionView on MainPage.

    [RelayCommand]
    public void Load_TitleList()
    {
        if (TitleList.Count != 0)
            TitleList.Clear();
        
        List<TitleTableObj> TitleObjList = DB_Handler.GetTitlesAll();

        if (TitleObjList != null && TitleObjList.Count > 0)
        {
            foreach (TitleTableObj titleTableObj in TitleObjList)
            {
                Title TitleItem = TitleConverter.ConvertToTitle(titleTableObj);
                TitleList.Add(TitleItem);
            }

            ShouldRefresh = false;
        }

        else
        {
            ShouldRefresh = false;
            return;
        }
    }

    // Forwards users to 'TitleEditPage' to edit a title they selected on the MainPage.
    [RelayCommand]
    private async Task GoToTitleEditPage(Title title)
    {
        if (title == null)
            return;
        else
        {
            await Shell.Current.GoToAsync($"{nameof(TitleEditPage)}?TitleId={title.Title_ID}",
                                            new Dictionary<string, object> { ["CurrentTitle"] = title });
        }
    }

    // Forwards users to 'ReferenceListPage' where the title selected on MainPage
    // is passed to and injected into the instance of the 'LastInjectedTitle' property of 'ReferenceListView'
    // and also added to a list of all the titles of the current Reference-List as the
    // most recent element in the List (ObservableCollection) displayed on 'ReferenceListPage'.
    [RelayCommand]
    private async Task GoToTitleReferenceListPage(Title title)
    {        
        if (title == null)
            return;
        else
        {
            await Shell.Current.GoToAsync($"{nameof(ReferenceListPage)}",
                                            new Dictionary<string, object> { ["LastInjectedTitle"] = title });
        }
    }

    // 'Title View' is notified from 'TitleEditPage' when a new Title was added to the database
    // so that its own collection may update and display of the Titles in the DB may refresh.
    public void Receive(AddItemMessage message)
    {
        AddOrChangeTitle(message.Value);
        ShouldRefresh = false;
    }

    /* Checks whether a the Title passed by the AddItemMessage from TitleEditView
     * was already in the TitleList -> then values of this item will be overwritten
     * or if was not yet in TitleList just add's it to TitleList.
     */
    private void AddOrChangeTitle(Title changedTitle)
    {
        var results = TitleList.Where(titleItem => titleItem.Title_ID == changedTitle.Title_ID);

        Title formerTitle = null;
        if (results.Any())
        {
            formerTitle = results.First();

            if (formerTitle != null)
            {
                int index = TitleList.IndexOf(formerTitle);
                if (index >= 0 && index < TitleList.Count)
                    TitleList[index] = changedTitle;
            }
        }

        else
            TitleList.Add(changedTitle);

        ShouldRefresh = true;
    }

    public void Receive(ItemDeletedMessage message)
    {
        ShouldRefresh = true;
    }

    /* Bound to the Delete-Button on every item of the CollectionView on MainPage
    * representing one Title in the Titles-Collection.
    * Makes a call to DB_Handler so that the Title may be deleted permanently from the database.*/
    [RelayCommand]
    private async Task DeleteAction(Title title)
    {
        if (title.Title_ID != 0)
        {
            await DB_Handler.DeleteTitleByIdAsync(title.Title_ID);
            await DB_Handler.DeleteAllLinkedTitlesAsync_WithTitleID(title.Title_ID);
            ShouldRefresh = true;
        }

        await Task.Delay(100);
        ShouldRefresh = false;
    }

    /* Performs Search on the Title-Collection as soon as the user 
     * starts to enter or changes the text in the Search-Bar at the top of MainPage.
     * Fills the ListView on MainPage displaying the search results.
     * 
     * I decided to perform the search on the runtime list/ representation of the Titles
     * to avoid unneccessary calls to the database (DB_Handler-class).
     */
    [RelayCommand]
    private void SearchTitles()
    {
        if(!string.IsNullOrEmpty(SearchText))
        {
            if (SearchResults.Count > 0)
            {
                SearchResults.Clear();
            }

            var searchResults = TitleList.Where(
                titleEntry => !string.IsNullOrEmpty(titleEntry.ItemTitle)
                && titleEntry.ItemTitle.Contains(SearchText, StringComparison.OrdinalIgnoreCase)).ToList();

            if (searchResults != null && searchResults.Count > 0)
            {
                foreach (var item in searchResults)
                {
                    SearchResults.Add(item);
                }
                SearchShouldRefresh = true;
            }

            else
            {
                var searchResultsAuthors = TitleList.Where(
                titleEntry => !string.IsNullOrEmpty(titleEntry.Authors[0].LastName)
                && titleEntry.Authors[0].FirstName.Contains(SearchText, StringComparison.OrdinalIgnoreCase)
                || titleEntry.Authors[0].LastName.Contains(SearchText, StringComparison.OrdinalIgnoreCase)).ToList();

                if (searchResultsAuthors != null && searchResultsAuthors.Count > 0)
                {
                    foreach (var item in searchResultsAuthors)
                    {
                        SearchResults.Add(item);
                    }
                    SearchShouldRefresh = true;
                }
                
                else
                {
                    SearchShouldRefresh = false;
                    SearchInputText = string.Empty;
                }
            }

            SearchShouldRefresh = false;
        }
    }

    /* If users selects one of the titles displayed by the ListView
     * that represents the search results of 'SearchTitles()'
     * this command will be called and lets them choose whether
     * to edit a title they searched for or to add it to the current list
     * and than navigates to the respective page passing the selected item ('Title'-object).
     */
    [RelayCommand]
    private async Task ChooseEditOrReference(Title title)
    {
        string action =
            await Shell.Current.DisplayActionSheet("Choose action", "Cancel", null, "Edit", "Add to reference list");

        if (action == "Edit")
            await GoToTitleEditPage(title);
        else if (action == "Add to reference list")
            await GoToTitleReferenceListPage(title);
        else
            return;
    }
}


