namespace E_Citera_MAUI.Views;

/* The Citation-Styles-Page a standalone page where new citation styles can be created, 
 * existing ones changed (loaded and opened them for editing) or deleted if not needed anymore.
 * The Vievmodel for this page 'CitationStyleView' handles the user-input on the Page (its bound member properties) 
 * and forwards the runtime instance of a CitationStyle-object to the DB_Handler-class 
 * which performs the actual CRUD-operations on the database-equivalent.
 * If a new citation style is created 'CitationStyle-View' notifies 
 * 'TitleEditView' and 'ReferenceListView' via the 'StylesChangedMessage', 
 * so they may make the new citation style available for citing.
 */

public partial class CitationStylesPage : ContentPage
{
	private CitationStyleView citationStyleView;
	public CitationStylesPage()
	{
		InitializeComponent();
		citationStyleView = new CitationStyleView();
		BindingContext = citationStyleView;
    }
}