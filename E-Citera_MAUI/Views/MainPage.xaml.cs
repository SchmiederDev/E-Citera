namespace E_Citera_MAUI.Views;

/* - The MainPage, titled 'Title Overview', displays all of the titles currently in the data base.

'MainPage.XAML/MainPage.XAML.cs' corresponds with 'TitleView.cs' following the MVVM-pattern
- From MainPage users will be forwarded to the sub-pages to perform different operations on or with the Titles:
	
	+ Editing Titles -> TitleEditPage-TitleEditView
	+ Adding Titles to Reference Lists -> ReferenceListPage-ReferenceListView

- This forwarding is handled by TitleView via navigation commands (bound to UI-Elements on the MainPage),
  which also passes the data needed (objects of type 'Title') to TitleEditView or ReferenceListView.

- From the MainPage Titles can also be deleted, which is handled by TitleView and forwarded to the 'DB_Handler'-class 

- MainPage allows for searching the titles in the database (currently by 'ItemTitle' and 'Author'). 
	+ The Search (user input in the Search-Bar) is handled by member methods of TitleView.
	+ Search-Results are kept as separate ObservableCollection to fill the ListView on MainPage bound 
    to the Search-Results. 
 */
public partial class MainPage : ContentPage
{
    TitleView myTitleview;

    public MainPage(TitleView titleView)
    {
        InitializeComponent();
        BindingContext = titleView;
        myTitleview = titleView;
    }
}