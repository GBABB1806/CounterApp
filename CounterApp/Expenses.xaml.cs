
using Microsoft.Maui.Controls.Shapes;
using System.Collections;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace CounterApp;

/// <summary>
/// Crea un'istanza della pagina dedicata alle spese con al suo interno un grafico a donut/anello
/// </summary>
public partial class Expenses : ContentPage
{

    List<Spesa> listaSpese;

    public Expenses()
    {
        InitializeComponent();

        // Inizializza la lista delle spese
        listaSpese = App.UtenteInUso.Spesa;
        double sommaSpese = 0;
        if (listaSpese.Count > 0)
        {
            foreach (var spesa in listaSpese)
                sommaSpese += spesa.Valore;
            SpesaTotale.Text = sommaSpese.ToString() + " €";
        }
        // Popola la griglia con le spese esistenti
        AggiornaGrigliaSpese();

    }


    // Metodo chiamato quando viene cliccato il pulsante "Aggiungi Spesa"
    private void OnAggiungiSpesaClicked(object sender, EventArgs e)
    {
        // Passa il riferimento corrente a AggiungiElemento
        var aggiungiElementoPage = new AggiungiElemento(this);
        Navigation.PushModalAsync(aggiungiElementoPage);
    }

    // Metodo per aggiornare la griglia delle spese
    public void AggiornaGrigliaSpese(Spesa spesaN)
    {
        double sommaSpese = 0;
        // Pulisci lo stack layout delle spese
        speseStackLayout.Children.Clear();
        listaSpese.Add(spesaN);
        // Aggiungi dinamicamente le righe per le spese
        if (listaSpese.Count > 0)
            foreach (var spesa in listaSpese)
            {
                var labelNome = new Label { Text = spesa.Nome };
                var labelValore = new Label { Text = spesa.Valore.ToString() };

                var row = new Grid
                {
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    }
                };
                sommaSpese += spesa.Valore;
                row.BackgroundColor = new Color(0, 202, 220);
                row.WidthRequest = 300;
                row.HeightRequest = 75;
                row.VerticalOptions = LayoutOptions.CenterAndExpand;
                row.HorizontalOptions = LayoutOptions.CenterAndExpand;
                labelNome.HorizontalOptions = LayoutOptions.Center;
                labelNome.VerticalOptions = LayoutOptions.Center;
                labelValore.HorizontalOptions = LayoutOptions.Center;
                labelValore.VerticalOptions = LayoutOptions.Center;
                row.Children.Add(labelNome);
                row.Children.Add(labelValore);
                row.SetColumn(labelNome, 0);
                row.SetColumn(labelValore, 1);
                speseStackLayout.Children.Add(row);
                speseStackLayout.Children.Add(new Label { HeightRequest = 5, BackgroundColor = new Color(0, 0, 0, 0) });
                SpesaTotale.Text = sommaSpese.ToString() + " €";
                App.UtenteInUso.Spesa = listaSpese;
            }
    }
    public void AggiornaGrigliaSpese()
    {
        
        // Pulisci lo stack layout delle spese
        speseStackLayout.Children.Clear();
        // Aggiungi dinamicamente le righe per le spese
        if (listaSpese.Count > 0)
            foreach (var spesa in listaSpese)
            {
                var labelNome = new Label { Text = spesa.Nome };
                var labelValore = new Label { Text = spesa.Valore.ToString() };
            
                var row = new Grid
                {
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    }

                };
                row.BackgroundColor = new Color(0, 202, 220);
                row.WidthRequest = 300;
                row.HeightRequest = 75;
                row.VerticalOptions = LayoutOptions.CenterAndExpand;
                row.HorizontalOptions = LayoutOptions.CenterAndExpand;
                labelNome.HorizontalOptions = LayoutOptions.Center;
                labelNome.VerticalOptions = LayoutOptions.Center;
                labelValore.HorizontalOptions = LayoutOptions.Center;
                labelValore.VerticalOptions = LayoutOptions.Center;
                row.Children.Add(labelNome);
                row.Children.Add(labelValore);
                row.SetColumn(labelNome, 0);
                row.SetColumn(labelValore, 1);
                speseStackLayout.Children.Add(row);
                speseStackLayout.Children.Add(new Label { HeightRequest = 5, BackgroundColor = new Color(0,0,0,0)});
                App.UtenteInUso.Spesa = listaSpese;
            
            }
    }

    private void Indietro(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
        App.Current.MainPage = new MainPage();
    }
}
