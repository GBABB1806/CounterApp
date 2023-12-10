namespace CounterApp;

public partial class AggiungiElemento : ContentPage
{
    public AggiungiElemento()
    {
        InitializeComponent();
    }

    private void SceltaModalita_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void SceltaTipo(object sender, EventArgs e)
    {
        switch (sceltaTipo.SelectedIndex)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;

        }

    }

    private void Button_Clicked(object sender, EventArgs e)
    {

    }

    private void Indietro(object sender, EventArgs e)
    {
        App.Current.MainPage = new MainPage();
    }
}