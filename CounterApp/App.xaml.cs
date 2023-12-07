namespace CounterApp
{
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(@"Ngo9BigBOggjHTQxAR8/V1NHaF1cWWhIYVVpR2Nbe05xdF9HYlZRQmYuP1ZhSXxQd0djXn5ccHVVRmdcVkU=");
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}
