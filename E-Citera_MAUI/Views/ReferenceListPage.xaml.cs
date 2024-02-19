namespace E_Citera_MAUI.Views;


/* The Reference-List-Page is where users can generate (add & remove), 
 * store (save) and load collections of titles,
 * e.g. to create bibliographies for their research or their own papers
 * or a 'books I'd like to read' or 'books I have already read'
 * or for any other purpose.
 * 
 * One of its most important functionalities is to create
 * formatted citations (strings) following the pattern of a 'CitationStyle'
 * 
 * 'ReferenceListView' is the ViewModel of this page.
 */
public partial class ReferenceListPage : ContentPage
{
	private ReferenceListView referenceListView;
	public ReferenceListPage()
	{
		InitializeComponent();
		referenceListView = new ReferenceListView();
		BindingContext = referenceListView;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        referenceListView.ShouldRefresh = true;
        referenceListView.AddInjected();		
    }
}