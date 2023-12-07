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
        //Creo Variabili Accessibili alla classe VediProdotto e assegno loro dei valori
        nomi = new string[] { Nome1.Text, Nome2.Text, Nome3.Text };
        spese = new double[] { Convert.ToDouble(Spesa1.Text), Convert.ToDouble(Spesa2.Text), Convert.ToDouble(Spesa3.Text) };
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