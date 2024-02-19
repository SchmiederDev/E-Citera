namespace E_Citera_MAUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DB_Handler.CreateDatabase();
            MainPage = new AppShell();
        }
    }
}