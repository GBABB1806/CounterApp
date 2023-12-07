using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using Syncfusion.Maui.Charts;
using System.Diagnostics;

namespace CounterApp;

/// <summary>
/// Crea un'istanza della pagina dedicata alle spese con al suo interno un grafico a donut/anello
/// </summary>
public partial class Expenses : ContentPage
{
    public string[] nomi {get;set;}
    public double[] spese {get;set;}
	public Expenses()
	{
        InitializeComponent();
        nomi = new string[Tabella.RowDefinitions.Count];
        spese = new double[Tabella.RowDefinitions.Count];
        //Creo Variabili Accessibili alla classe VediProdotto e assegno loro dei valori
        for (int i = 0; i < Tabella.RowDefinitions.Count; i++)
        {
            //Qui ho la possibilità di variare la grandezza del
            //grafico in base a quanto la tabella è lunga
            string nomeControllo = $"nome{i}";
            string spesaControllo = $"spesa{i}";
            Label controlloN = Tabella.FindByName<Label>(nomeControllo);
            Label controlloS = Tabella.FindByName<Label>(spesaControllo);
            nomi[i] = controlloN.Text;
            spese[i] = Convert.ToDouble(controlloS.Text);
        }
        //Istanzio una lista di prodotti con i valori all'interno
        VediProdotto listaProdotti = new VediProdotto(nomi,spese);
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
}