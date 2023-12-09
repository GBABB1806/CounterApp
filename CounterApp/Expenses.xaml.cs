
namespace CounterApp;

/// <summary>
/// Crea un'istanza della pagina dedicata alle spese con al suo interno un grafico a donut/anello
/// </summary>
public partial class Expenses : ContentPage
{
    public string[] nomi { get; set; }
    public double[] spese { get; set; }
    public Expenses()
    {
        InitializeComponent();
        nomi = new string[Tabella.RowDefinitions.Count];
        spese = new double[Tabella.RowDefinitions.Count];
        //Creo Variabili Accessibili alla classe VediProdotto e assegno loro dei valori
        for (int i = 1; i < Tabella.RowDefinitions.Count + 1; i++)
        {
            //Qui ho la possibilit� di variare la grandezza del
            //grafico in base a quanto la tabella � lunga
            string nomeControllo = $"Nome{i}";
            string spesaControllo = $"Spesa{i}";
            Label controlloN = Tabella.FindByName<Label>(nomeControllo);
            Label controlloS = Tabella.FindByName<Label>(spesaControllo);
            nomi[i - 1] = controlloN.Text;
            spese[i - 1] = Convert.ToDouble(controlloS.Text);
        }
        //Istanzio una lista di prodotti con i valori all'interno
        VediProdotto listaProdotti = new VediProdotto(nomi, spese);
        //Pesco il grafico a cui dare come sorgente i dati con queste due righe
        DGHSeries.ItemsSource = listaProdotti.Prodotti;
        BindingContext = listaProdotti;
    }
    /// <summary>
    /// Metodo che ci consente di tornare alla pagina precedente
    /// </summary>
    private void Indietro(object sender, EventArgs e)
    {
        Application.Current.MainPage = new MainPage();
    }

    private void Home(object sender, EventArgs e)
    {
        Application.Current.MainPage = new MainPage();
    }
}