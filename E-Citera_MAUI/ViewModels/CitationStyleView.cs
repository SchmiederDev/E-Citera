using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Messaging;
using E_Citera_MAUI.Messages;

namespace E_Citera_MAUI.ViewModels;

// For a note on the general purpose and functionality of 'CitationStylesPage' 
// and this ViewModel, see 'CitationStylesPage.xaml.cs'
public partial class CitationStyleView : ObservableObject
{
    [ObservableProperty]
    public CitationStyle currentCitationStyle;

    public ObservableCollection<CitationStyle> CitationStyles;

    [ObservableProperty]
    private string authorsEtAlNumberAsString;

    [ObservableProperty]
    private string editorNumberAsString;

    [ObservableProperty]
    private string outPutText;

    [ObservableProperty]
    private List<string> citationFieldSelectionOptions;

    [ObservableProperty]
    private List<string> separatorFieldSelectionOptions;

    [ObservableProperty]
    private List<string> authorSeparatorOptions;

    [ObservableProperty]
    private List<string> finalizerOptions;

    [ObservableProperty]
    private List<string> styleNames;

    [ObservableProperty]
    private string styleSelected;

    public CitationStyleView() 
    { 
        CurrentCitationStyle = new CitationStyle();
        CitationStyles = new ObservableCollection<CitationStyle>();
               
        InitSelectionOptions();
        LoadStyleNames();
    }

    /* Users are supposed to choose only from a limited amount of characters
     * serving as separators between the fields of a 'CitationStyle'
     * Therefore they may choose this limited amount from the items
     * of picker-menus (or drop-down-menus).
     * This method initializes the values for these selection options
     * on the picker menus based on the characters allowed - declared
     * in the 'CitationHandler'-Class.
     */
    private void InitSelectionOptions()
    {
        CitationFieldSelectionOptions = new List<string>();
        CitationFieldSelectionOptions.AddRange(CitationHandler.CitationFieldNames.ToList());

        SeparatorFieldSelectionOptions = new List<string>();
        for (int i = 0; i < CitationHandler.Separators.Length; i++)
        {
            string separatorOption = CitationHandler.Separators[i].ToString();
            SeparatorFieldSelectionOptions.Add(separatorOption);
        }

        AuthorSeparatorOptions = new List<string>();
        for (int i = 0; i < CitationHandler.AuthorSeparatorCharacters.Length; i++)
        {
            string authorSeparatorOption = CitationHandler.AuthorSeparatorCharacters[i].ToString();
            AuthorSeparatorOptions.Add(authorSeparatorOption);
        }

        FinalizerOptions = new List<string>();
        for (int i = 0; i < CitationHandler.FinalizerCharacters.Length; i++)
        {
            string finalizerOption = CitationHandler.FinalizerCharacters[i].ToString();
            FinalizerOptions.Add(finalizerOption);
        }
    }

    private void LoadStyleNames()
    {
        StyleNames = new List<string>(DB_Handler.GetStyleNames());
    }

    private int CheckNumberInput(string input)
    {
        int number = 0;
        if (int.TryParse(input, out int stringOut))
        {
            if(stringOut > 0)
                number = stringOut;
        }

        return number;
    }

    [RelayCommand]
    private void NewCitationStyle()
    {
        CurrentCitationStyle = new CitationStyle();
    }

    [RelayCommand]
    private void SaveCitationStyle()
    {
        CurrentCitationStyle.NumberOfAuthorsMentioned = CheckNumberInput(AuthorsEtAlNumberAsString);
        CurrentCitationStyle.NumberOfEditorsMentioned = CheckNumberInput(EditorNumberAsString);
        
        if (CurrentCitationStyle.StyleID > 0)
            DB_Handler.UpdateCitationStyle(CurrentCitationStyle);
        else
        {
            int newStyleID = DB_Handler.InsertCitationStyleReturnID(CurrentCitationStyle);
            CurrentCitationStyle.StyleID = newStyleID; 
        }

        SendStylesChangedMessage();
        LoadStyleNames();
    }

    [RelayCommand]
    private void LoadCitationStyle()
    {
        CitationStyle styleLoaded = DB_Handler.Get_CitationStyle_by_Name(StyleSelected);
        if(styleLoaded != null)
            CurrentCitationStyle = styleLoaded;
    }

    [RelayCommand]
    private void DeleteCitationStyle()
    {
        if(CurrentCitationStyle.StyleID > 0)
        {
            if (!CurrentCitationStyle.IsDefaultStyle)
            {
                DB_Handler.DeleteCitationStyle(CurrentCitationStyle.StyleID);
                CurrentCitationStyle = new CitationStyle();
            }

            else
            {
                Shell.Current.DisplayAlert("WARNING:", "The style you are trying to delete is a default citation style." +
                    "Sorry, but this not allowed.", "OK");
            }
        }
    }

    /* If a new citation style is created 'CitationStyle-View' notifies
     * 'TitleEditView' and 'ReferenceListView' via the 'StylesChangedMessage',
     * so they may make the new citation style available for citing.
     */
    private void SendStylesChangedMessage()
    {
        WeakReferenceMessenger.Default.Send(new StylesChangedMessage(true));
    }

}
