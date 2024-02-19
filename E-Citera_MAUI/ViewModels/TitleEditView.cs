using CommunityToolkit.Mvvm.Messaging;
using E_Citera_MAUI.Messages;
using System.Collections.ObjectModel;

namespace E_Citera_MAUI.ViewModels;


// For a note on the purpose of 'TitleEditView' and its View 'TitleEditPage', see 'TitleEditPage.xaml.cs'

[QueryProperty(nameof(Title_id), nameof(Title_id))]
[QueryProperty(nameof(CurrentTitle), nameof(CurrentTitle))]
public partial class TitleEditView : ObservableObject, IRecipient<StylesChangedMessage>
{
    [ObservableProperty]
    private int title_id = 0;

    [ObservableProperty]
    private Title currentTitle;

    [ObservableProperty]
    private bool shouldRefresh = false;

    [ObservableProperty]
    public bool authorsRefresh;
    [ObservableProperty]
    public bool editorsRefresh;

    [ObservableProperty]
    private string outPutText;

    [ObservableProperty]
    private List<string> styleNames;

    [ObservableProperty]
    private string styleSelected;

    [ObservableProperty]
    private CitationStyle currentCitationStyle;

    private string titleShortNoticeMsg = "Title may not be an empty field and at least one character long.\n" +
        "Sorry, title will not be saved.";

    private string yearNotANumberMsg = 
        "Seems like year of publication is not a number.\n" +
        "Sorry, year must be an integer (a whole number).\n" +
        "Otherwise only the last valid value will be saved.\n" +
        "If you entered nothing before this will be '0'.";

    private string titleNotSavedMsg =
        "Sorry, seems you haven't saved this title yet.\n" +
        "Please save your title first before it can be cited.";

    private string noCitationStyleSelectedMsg =
        "Sorry, seems you haven't selected a style.\n" +
        "Please select a citation style first to cite the title.";



    public TitleEditView()
    {  
        currentTitle = new Title();
        StyleNames = new List<string>(DB_Handler.GetStyleNames());
        WeakReferenceMessenger.Default.Register<StylesChangedMessage>(this);
    }

    [RelayCommand]
    private void CheckYearText(object year)
    {
        Entry yearAsEntry = year as Entry;
        if (yearAsEntry != null)
        {
            if(!int.TryParse(yearAsEntry.Text, out int textOut))
            {
                Shell.Current.DisplayAlert("Value Error:", yearNotANumberMsg, "OK");
            }
        }
    }

    [RelayCommand]
    public void AddAuthor()
    {
        CurrentTitle.Authors.Add(new Author());
    }

    [RelayCommand]
    public void AddEditor()
    {
        CurrentTitle.Editors.Add(new Author());
    }

    [RelayCommand]
    public void NewTitle()
    {
        CurrentTitle = new Title();
        OutPutText = string.Empty;
        AddEmptyAuthorEditorFields();
    }

    /* Authors and Editors (both Lists<> of Type 'Author', see 'Title'-class)
     * are bound to a CollectionView which itself contains entries for 
     * first names and last names of authors and editors.
     * Because the number of those entries (input fields) 
     * are dependent on the 'Count' of those Lists
     * this method ensures that there are always at least two entries 
     * for the first and last names of at least one Author and one Editor
     * if there are currently no items in the Author or Editor-Lists.
     * This happens either when the user navigates to the Title-Edit-Page
     * via the tab bar after application start up or when the user
     * hit the 'New'-Button. In both cases 'CurrentTitle'-values will be empty.
     * So without this method there would also be no entries for first and last names
     * because 'Count' of Author and Editor-List would be '0'.
     */
    public void AddEmptyAuthorEditorFields()
    {
        if (CurrentTitle.Authors.Count == 0)
            AddAuthor();
        if (CurrentTitle.Editors.Count == 0)
            AddEditor();
    }

    [RelayCommand]
    public void Save()
    {
        if (CurrentTitle.ItemTitle.Length == 0)
            Shell.Current.DisplayAlert("Note:", titleShortNoticeMsg, "OK");

        else
        {            
            InsertOrUpdateTitle();    
            HandleAuthorLinks();            
            SendAddItemNotification();            
            DB_Handler.CloseConnection();            
            AddEmptyAuthorEditorFields();
        }
    }

    private void InsertOrUpdateTitle()
    {
        InsertOrUpdateAuthors(CurrentTitle.Authors);
        InsertOrUpdateAuthors(CurrentTitle.Editors);
        InsertOrUpdateSeries();
        InsertTitle();
    }

    private void InsertOrUpdateAuthors(ObservableCollection<Author> authors)
    {
        for(int i = 0; i < authors.Count; i++)
        {
            authors[i].FirstName = authors[i].FirstName.Trim();
            authors[i].LastName = authors[i].LastName.Trim();
            authors[i] = InsertOrUpdateAuthor(authors[i]);
        }
    }    

    private Author InsertOrUpdateAuthor(Author author)
    {
        AuthorTableObj authorTableEntry = TitleConverter.ConvertToAuthorTableObjectRawWithoutID(author);

        if (author.AuthorId > 0)
        {
            int existingAuthorID = DB_Handler.AuthorIsAlreadyInDB_CompareName(author.FirstName, author.LastName);

            if (existingAuthorID != author.AuthorId)
            {
                if (existingAuthorID != 0)
                    author = new Author(existingAuthorID, author.FirstName, author.LastName);

                else
                {
                    if (author.LastName.Length > 0)
                    {
                        int newAuthorID = DB_Handler.InsertAuthorReturnID(authorTableEntry);
                        author = new Author(newAuthorID, author.FirstName, author.LastName);
                    }
                }
            }
        }

        else
        {
            int existingAuthorID = DB_Handler.AuthorIsAlreadyInDB_CompareName(author.FirstName, author.LastName);

            if (existingAuthorID != 0)
                author = new Author(existingAuthorID, author.FirstName, author.LastName);

            else
            {
                if (author.LastName.Length > 0)
                {
                    int newAuthorID = DB_Handler.InsertAuthorReturnID(authorTableEntry);
                    author = new Author(newAuthorID, author.FirstName, author.LastName);
                }
            }
        }

        return author;
    }

    private void InsertOrUpdateSeries()
    {
        SeriesTableObj seriesTableEntry = TitleConverter.ConvertToSeriesTitleObjectRawWithoutID(CurrentTitle);
        
        if(CurrentTitle.SeriesID > 0)
        {
            int existingSeriesID = DB_Handler.SeriesIsAlreadyInDB_CompareStrings(CurrentTitle.SeriesTitle);

            if(existingSeriesID != CurrentTitle.SeriesID)
            {
                if(existingSeriesID != 0)
                    CurrentTitle.SeriesID = existingSeriesID;

                else
                {
                    if (CurrentTitle.SeriesTitle.Length > 0)
                    {
                        int newSeriesID = DB_Handler.InsertSeriesReturnID(seriesTableEntry);
                        CurrentTitle.SeriesID = newSeriesID;
                    }
                }
            }
        }

        else
        {
            int existingSeriesID = DB_Handler.SeriesIsAlreadyInDB_CompareStrings(CurrentTitle.SeriesTitle);
            if (existingSeriesID > 0)
                CurrentTitle.SeriesID = existingSeriesID;

            else
            {
                if (CurrentTitle.SeriesTitle.Length > 0)
                {
                    int newSeriesID = DB_Handler.InsertSeriesReturnID(seriesTableEntry);
                    CurrentTitle.SeriesID = newSeriesID;
                }
            }
        }
    }


    private void InsertTitle()
    {
        TitleTableObj titleTableEntry = TitleConverter.ConvertToTitleTableObjectWithoutTitleID(CurrentTitle);

        if (CurrentTitle.Title_ID > 0)
        {
            titleTableEntry.ID = CurrentTitle.Title_ID;
            DB_Handler.UpdateTitle(titleTableEntry);
        }

        else
        {
            int newTitleID = DB_Handler.InsertTitleReturnID(titleTableEntry);
            CurrentTitle.Title_ID = newTitleID;
        }

    }

    private void HandleAuthorLinks()
    {
        CleanUpAuthorsAndEditors();
        LinkAuthors();
        DeleteObsoleteAuthorLinks();
    }

    private void CleanUpAuthorsAndEditors()
    {
        CleanUpAuthorList(CurrentTitle.Authors);
        CleanUpAuthorList(CurrentTitle.Editors);
    }

    private void CleanUpAuthorList(ObservableCollection<Author> authors)
    {
        for (int i = authors.Count - 1; i >= 0; i--)
        {
            if (authors[i].AuthorId == 0)
            {
                if (i >= 0 && i < authors.Count)
                    authors.RemoveAt(i);
            }

            if (authors.Count > 0 && i < authors.Count)
            {
                if (authors[i].LastName.Length == 0)
                {
                    if (i >= 0 && i < authors.Count)
                        authors.RemoveAt(i);
                }
            }
        }
    }

    private void LinkAuthors()
    {
        InsertOrUpdateAuthorLinks(CurrentTitle.Authors, "author");
        InsertOrUpdateAuthorLinks(CurrentTitle.Editors, "editor");
    }

    private void InsertOrUpdateAuthorLinks(ObservableCollection<Author> authors, string role)
    {
        for (int i = 0; i < authors.Count; i++)
        {
            AuthorTitleLinkTableObj authorTitleLinkTableObj = 
                TitleConverter.ConvertToAuthorTitleLinkTableObject(
                    CurrentTitle.Title_ID, 
                    authors[i].AuthorId, 
                    role,
                    authors.IndexOf(authors[i]),
                    1);


            int existingAuthorLinkEntryID = DB_Handler.AuthorAlreadyLinkedToTitle(authorTitleLinkTableObj);
            if (existingAuthorLinkEntryID > 0)
            {
                authorTitleLinkTableObj.Entry_ID = existingAuthorLinkEntryID;
                DB_Handler.UpdateAuthorsAndTitles(authorTitleLinkTableObj);
            }

            else
            {
                DB_Handler.InsertIntoAuthorsAndTitles(authorTitleLinkTableObj);
            }
        }
    }

    private void DeleteObsoleteAuthorLinks()
    {
        List<AuthorTitleLinkTableObj> linkedAuthors = DB_Handler.GetTitleAuthorLinks_FromTitleID_and_AuthorRole(CurrentTitle.Title_ID, "author");
        foreach (AuthorTitleLinkTableObj linkedAuthor in linkedAuthors)
        {
            if (linkedAuthor.Position > CurrentTitle.Authors.Count - 1 && linkedAuthor.Position != 0)
            {
                linkedAuthor.IsActive = 0;
                DB_Handler.UpdateAuthorsAndTitles(linkedAuthor);
            }
        }

        List<AuthorTitleLinkTableObj> linkedEditors = DB_Handler.GetTitleAuthorLinks_FromTitleID_and_AuthorRole(CurrentTitle.Title_ID, "editor");
        foreach (AuthorTitleLinkTableObj linkedEditor in linkedEditors)
        {
            if (linkedEditor.Position > CurrentTitle.Editors.Count - 1 && linkedEditor.Position != 0)
            {
                linkedEditor.IsActive = 0;
                DB_Handler.UpdateAuthorsAndTitles(linkedEditor);
            }
        }

    }

    private void SendAddItemNotification()
    {        
        WeakReferenceMessenger.Default.Send(new AddItemMessage(CurrentTitle));
    }

    [RelayCommand]
    private async Task DeleteAction()
    {
        if(CurrentTitle.Title_ID != 0)
        {
            await DB_Handler.DeleteTitleByIdAsync(CurrentTitle.Title_ID);
            await DB_Handler.DeleteAllLinkedTitlesAsync_WithTitleID(CurrentTitle.Title_ID);
            SendItemDeletedNotification();
        }
        NewTitle();
        
    }

    [RelayCommand]
    private void CiteTitle()
    {
        LoadCitationStyle();
        if (CurrentCitationStyle != null)
        {
            if(CurrentTitle.Title_ID > 0)
            {
                if (!string.IsNullOrEmpty(OutPutText))
                    OutPutText = string.Empty;

                OutPutText = CitationHandler.CreateCitationString(CurrentTitle, CurrentCitationStyle);
            }
            else
                Shell.Current.DisplayAlert("Title unsaved:", titleNotSavedMsg, "OK");
        }

        else
            Shell.Current.DisplayAlert("No style selected:", noCitationStyleSelectedMsg, "OK");
    }

    private void LoadCitationStyle()
    {
        if (!string.IsNullOrEmpty(StyleSelected))
        {
            CitationStyle styleLoaded = DB_Handler.Get_CitationStyle_by_Name(StyleSelected);
            if (styleLoaded != null)
                CurrentCitationStyle = styleLoaded;
        }
    }

    private void SendItemDeletedNotification()
    {
        WeakReferenceMessenger.Default.Send(new ItemDeletedMessage(CurrentTitle));
    }

    public void Receive(StylesChangedMessage stylesChangedMessage)
    {
        StyleNames = new List<string>(DB_Handler.GetStyleNames());
    }
}


