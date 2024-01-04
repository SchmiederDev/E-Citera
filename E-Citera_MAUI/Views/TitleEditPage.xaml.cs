namespace E_Citera_MAUI.Views;

/* The TitleEditPage is where users can create new titles and store them to the database 
   or edit existing ones as well as create a citation of a single title
   (a formatted string following the pattern of a specific citation style 
   (see Readme, CitationStyle-class, CitationHandler, and CitationStylesPage/-View).
   They may also delete the currently loaded title permantely from the database
   The ViewModel for this page is 'TitleEditView'.
 */
public partial class TitleEditPage : ContentPage
{
    TitleEditView myTitleEditView;

    public TitleEditPage() 
    {
        InitializeComponent();
    }


    public TitleEditPage(TitleEditView titleEditView)
    {
        InitializeComponent();
        BindingContext = titleEditView;
        myTitleEditView = titleEditView;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        myTitleEditView.AddEmptyAuthorEditorFields();
    }
}