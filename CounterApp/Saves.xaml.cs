namespace CounterApp;

public partial class Saves : ContentPage
{
    public Saves()
    {
        InitializeComponent();
    }
    private void Indietro(object sender, EventArgs e)
    {
        Application.Current.MainPage = new MainPage();
    }

    private void Home(object sender, EventArgs e)
    {
        Application.Current.MainPage = new MainPage();
    }
}