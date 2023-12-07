

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

        }
    }

}
