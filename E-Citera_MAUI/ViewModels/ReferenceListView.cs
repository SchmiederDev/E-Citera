using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Messaging;
using E_Citera_MAUI.Messages;

namespace E_Citera_MAUI.ViewModels;

/* For a note on the purpose of 'ReferenceListPage' and its ViewModel 'ReferenceListView',
        see 'ReferenceListPage.xaml.cs */

/* [NOTES ON THE FUNCTIONALITY OF 'REFERENCE-LIST-VIEW':]
 * The Reference-List-Page displays a collection of Title-Objects which are either passed to it (one at a time) 
 * from the 'MainPage'/'TitleView' or loaded and opened for editing from the database via DB_Handler.
 * 
 * In accordance with this Reference-List-View receives these Title-Objects via injection 
 * or sends a request to DB_Handler to retrieve the equivalent database-objects 
 * which are than converted to runtime-objects by 'TitleConverter'.
 * 
 * This ViewModel has a member inheriting from 'Observable-Property' called 'lastInjectTitle'.
 * 
 * - Any Title-Object received from the MainPage-TitleView via the GoToCommand is first injected 
 *   as value of this member property into the instance of 'ReferenceListView' held by its parent-page. 
 *   
 * - Then it also added to the ObservableCollection 'ReferenceListTitles' 
 *   which serves as the 'ItemSource' for 'ListView' on the Reference-List-Page 
 *   responsable for displaying the collection.
 *   
 *   Titles will either be injected from the general 'Title Overview' on MainPage
 *   if the user clicks on the add-button (plus-Sign) or from the search results
 *   if the user searches for titles on the MainPage.
 */

[QueryProperty(nameof(LastInjectedTitle), nameof(LastInjectedTitle))]
public partial class ReferenceListView : ObservableObject, IRecipient<StylesChangedMessage>
{
    [ObservableProperty]
    private string currentListName;

    private string currentListID;

    [ObservableProperty]
    private bool listNameEntryIsEnabled;

    [ObservableProperty]
    private string listNameSelected;

    [ObservableProperty]
    private List<string> referenceListNames;

    [ObservableProperty]
    private Title selectedTitle;

    [ObservableProperty]
    private Title lastInjectedTitle;

    [ObservableProperty]
    private ObservableCollection<Title> referenceListTitles;

    [ObservableProperty]
    private bool shouldRefresh;

    [ObservableProperty]
    List<string> styleNames;

    [ObservableProperty]
    private string citationOut;

    [ObservableProperty]
    string styleSelected;

    [ObservableProperty]
    CitationStyle currentCitationStyle;

    public ReferenceListView() 
    { 
        WeakReferenceMessenger.Default.Register<StylesChangedMessage>(this);
        SelectedTitle = new Title();
        LastInjectedTitle = new Title();
        ReferenceListTitles = new ObservableCollection<Title>();
        ReferenceListNames = DB_Handler.GetReferenceListNames();
        ListNameEntryIsEnabled = true;
        StyleNames = new List<string>(DB_Handler.GetStyleNames());
    }

    // For a note on where and how titles are injected (= assigned) to 'LastInjectedTitle',
    // see: 'NOTES ON THE FUNCTIONALITY OF 'REFERENCE-LIST-VIEW'
    public void AddInjected()
    {
        if(LastInjectedTitle.Title_ID != 0)
        {
            bool isAlreadyInList = ReferenceListTitles.Where(listEntry => listEntry.Title_ID == LastInjectedTitle.Title_ID).Any();
            if (!isAlreadyInList)
            {
                ReferenceListTitles.Add(LastInjectedTitle);
            }
        }
        ShouldRefresh = false;
    }

    [RelayCommand]
    private void NewReferenceList()
    {
        ResetProperties();
        ShouldRefresh = false;
    }

    private void ResetProperties()
    {
        ReferenceListTitles.Clear();

        currentListID = string.Empty;
        CurrentListName = string.Empty;
        
        ListNameSelected = string.Empty;
        
        LastInjectedTitle = null;
        SelectedTitle = null;        
        ListNameEntryIsEnabled = true;

        CurrentCitationStyle = null;
        StyleSelected = null;
        CitationOut = string.Empty;

        ShouldRefresh = true;
    }

    [RelayCommand]
    private void EnableListNameEdit()
    {
        if(!ListNameEntryIsEnabled)
            ListNameEntryIsEnabled = true;
    }

    private async Task SaveReferenceList(bool calledOnNavigation)
    {
        if (string.IsNullOrEmpty(CurrentListName))
        {
            if(!calledOnNavigation)
            {
                await Shell.Current.DisplayAlert("Empty List Name", "Sorry, please enter a title for this list " +
                "Otherwise in cannot be saved.", "Ok");
            }
        }

        else
        {
            if(ReferenceListTitles.Count > 0)
            {
                bool basedOnID = true;
                bool referenceList_ID_exists = DB_Handler.ReferenceListIDExists(currentListID);

                if (!referenceList_ID_exists)
                {
                    bool listNameExists = DB_Handler.ReferenceListNameExists(CurrentListName);
                    if (!listNameExists)
                    {
                        currentListID = CurrentListName;
                        DB_Handler.InsertReferenceListBulk(GenerateReferenceList());
                    }
                    else
                    {
                        string userResponse =
                            await Shell.Current.DisplayActionSheet(
                                "List name already exists:",
                                "Cancel",
                                null,
                                "Overwrite existing list");
                        if (userResponse == "Overwrite existing list")
                        {
                            basedOnID = false;
                            UpdateReferenceList(basedOnID);
                        }
                    }
                }

                else
                {
                    UpdateReferenceList(basedOnID);
                }

                ListNameEntryIsEnabled = false;
                ReferenceListNames = DB_Handler.GetReferenceListNames();
            }

            else
            {
                await Shell.Current.DisplayAlert("No Titles in List:", "Sorry, there must at least one title " +
                    "in your reference list to save it", "Ok");
            }
        }
    }

    private void UpdateReferenceList(bool basedOnID)
    {
        List<ReferenceListTableObj> existingReferenceListObjects;
        
        if (basedOnID)
            existingReferenceListObjects = DB_Handler.Get_ReferenceList_by_ID(currentListID);
        else
            existingReferenceListObjects = DB_Handler.Get_ReferenceList_by_Name(CurrentListName);

        List<ReferenceListTableObj> newReferenceListObjects = GenerateReferenceList();
        
        InsertNewReferences(existingReferenceListObjects, newReferenceListObjects);
        DeleteObsoleteReferences(existingReferenceListObjects, newReferenceListObjects, basedOnID);
        UpdateNewListID();
    }

    private List<ReferenceListTableObj> GenerateReferenceList()
    {
        List<ReferenceListTableObj> referenceObjects = new List<ReferenceListTableObj>();
        foreach (Title referenceTitle in ReferenceListTitles)
        {
            ReferenceListTableObj referenceListTableObj = new ReferenceListTableObj();
            referenceListTableObj.ListID = currentListID;
            referenceListTableObj.ReferenceListName = CurrentListName;
            referenceListTableObj.TitleID = referenceTitle.Title_ID;
            referenceObjects.Add(referenceListTableObj);
        }
        return referenceObjects;
    }

    private void InsertNewReferences(List<ReferenceListTableObj> existingReferenceListObjects, List<ReferenceListTableObj> newReferenceListObjects)
    {
        if(existingReferenceListObjects != null)
        {
            foreach (ReferenceListTableObj reference in newReferenceListObjects)
            {
                ReferenceListTableObj existingReference = existingReferenceListObjects.Find(existingReference => existingReference.TitleID == reference.TitleID);
                if (existingReference == null)
                {
                    DB_Handler.InsertReferenceListObject(reference);
                }

                else
                {
                    existingReference.ReferenceListName = CurrentListName;
                    DB_Handler.UpdateReferenceListObject(existingReference);
                }
            }
        }
    }

    private void DeleteObsoleteReferences(List<ReferenceListTableObj> existingReferenceListObjects, List<ReferenceListTableObj> newReferenceListObjects, bool basedOnId)
    {
        if(existingReferenceListObjects != null)
        {
            foreach (ReferenceListTableObj formerReference in existingReferenceListObjects)
            {
                ReferenceListTableObj stillReferencedTitle = newReferenceListObjects.Find(newReference => newReference.TitleID == formerReference.TitleID);
                if (stillReferencedTitle == null)
                {
                    if (basedOnId)
                        DB_Handler.Delete_Single_ReferenceListObject(currentListID, formerReference.TitleID);
                    else
                        DB_Handler.Delete_Single_ReferenceListObject(CurrentListName, formerReference.TitleID);
                }
            }
        }
    }

    private void UpdateNewListID()
    {         
        List<ReferenceListTableObj> references = DB_Handler.Get_ReferenceList_by_ID(currentListID);
        if(references != null)
        {
            foreach (ReferenceListTableObj reference in references)
            {
                reference.ListID = CurrentListName;
                DB_Handler.UpdateReferenceListObject(reference);
            }
        }
        currentListID = CurrentListName;
    }

    [RelayCommand]
    private void LoadReferenceList()
    {
        List<ReferenceListTableObj> references = DB_Handler.Get_ReferenceList_by_ID(ListNameSelected);
        if (references != null && references.Count > 0)
        {
            if (ReferenceListTitles.Count > 0)
                ReferenceListTitles.Clear();

            foreach (ReferenceListTableObj reference in references)
            {
                TitleTableObj titleTableObj = DB_Handler.GetTitleById(reference.TitleID);
                if (titleTableObj !=  null)
                {
                    Title title = TitleConverter.ConvertToTitle(titleTableObj);
                    ReferenceListTitles.Add(title);
                }
            }

            ListNameEntryIsEnabled = false;
            CurrentListName = ListNameSelected;
            currentListID = ListNameSelected;
        }
    }

    [RelayCommand]
    private void RemoveTitle(int titleID)
    {
        Title removedTitle = ReferenceListTitles.Where(referenceTitle => referenceTitle.Title_ID == titleID).First();
        if (removedTitle != null)
        {
            ReferenceListTitles.Remove(removedTitle);
        }
    }

    [RelayCommand]
    private async Task DeleteEntireReferenceList()
    {
        if (!string.IsNullOrEmpty(CurrentListName))
        {
            string answer =
                await Shell.Current.DisplayActionSheet("Delete entire reference list", "Cancel", null, "Yes, delete!");
            if (answer == "Yes, delete!")
            {
                DB_Handler.DeleteReferenceListByName(CurrentListName);
                ResetProperties();
                ReferenceListNames = DB_Handler.GetReferenceListNames();
                ShouldRefresh = false;
            }
        }

        else
            return;
    }

    [RelayCommand]
    public async Task SaveOnSaveButtonClick()
    {
        await SaveReferenceList(false);
    }

    public async void SaveOnPageLeave()
    {
        await SaveReferenceList(true);
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

    [RelayCommand]
    private void CiteReferenceList()
    {
        LoadCitationStyle();
        if (CurrentCitationStyle != null)
        {
            if (!string.IsNullOrEmpty(CitationOut))
                CitationOut = string.Empty;
            
            if (ReferenceListTitles != null && ReferenceListTitles.Count > 0)
            {
                foreach(Title reference in  ReferenceListTitles)
                {
                    string citation = CitationHandler.CreateCitationString(reference, CurrentCitationStyle);
                    if(!string.IsNullOrEmpty(citation))
                    {
                        CitationOut += citation + "\n";
                        CitationOut += "\n";
                    }
                }
            }

            else
            {
                Shell.Current.DisplayAlert("Empty Referene List:", 
                    "Sorry, there seem to be no titles in the current reference list." +
                    "Please add some titles or load a reference list first.", "OK");
            }
        }

        else
            Shell.Current.DisplayAlert("No style selected:", "Sorry, seems you haven't selected a style." +
                    "Please select a citation style first to cite the title.", "OK");
    }

    public void Receive(StylesChangedMessage stylesChangedMessage)
    {
        StyleNames = new List<string>(DB_Handler.GetStyleNames());
    }

}
