namespace E_Citera_MAUI
{
    public partial class AppShell : Shell
    {
        
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(TitleEditPage), typeof(TitleEditPage));
            Routing.RegisterRoute(nameof(CitationStylesPage), typeof(CitationStylesPage));
            Routing.RegisterRoute(nameof(ReferenceListPage), typeof(ReferenceListPage));
        }
    }
}