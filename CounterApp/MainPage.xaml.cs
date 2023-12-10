

namespace CounterApp
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }
        private void Spese_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new Expenses();
        }
        private void Login(object sender, EventArgs e)
        {
            App.Current.MainPage = new LoginPage();
        }

        private void Saves_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new Saves();
        }

        private void Home(object sender, EventArgs e)
        {
            App.Current.MainPage = new MainPage();
        }

        private void Aggiungi(object sender, EventArgs e)
        {
            int u = 0;
            if (u == 0)
                App.Current.MainPage = new AggiungiElemento();
            else
            {
                //Apri Login Page
            }
        }
    }

}
