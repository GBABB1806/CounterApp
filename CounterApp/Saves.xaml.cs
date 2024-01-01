using Syncfusion.Maui.Charts;
using System.Diagnostics;

namespace CounterApp;

public partial class Saves : ContentPage
{
    public DateTime ora { get; set; }
    public DateTime unAnnoFa { get; set; }
    public Saves()
    {
        ora= DateTime.Now;
        unAnnoFa= ora.AddYears(-1);
        InitializeComponent();
        ViewModel vM = new ViewModel(App.UtenteInUso.Risparmio);
        var primaryAxis = new DateTimeAxis()
        {
            
            Interval = 2,
            Minimum = unAnnoFa,
            Maximum = ora,
            MajorTickStyle = new ChartAxisTickStyle { TickSize = 0 },
            LabelStyle = new ChartAxisLabelStyle { LabelFormat = "" }
        };
        Grafico.XAxes.Add(primaryAxis);
        NumericalAxis secondaryAxis = new NumericalAxis();
        secondaryAxis.Minimum = 0;
        secondaryAxis.Maximum = App.UtenteInUso.Risparmio.Pensione.Max();
        Grafico.YAxes.Add(secondaryAxis);
        GraficoColonne.ItemsSource = vM.PensionSavings;
        GraficoColonne.XBindingPath = "Months";
        GraficoColonne.YBindingPath = "Savings";
        GraficoColonne.Spacing = 1;
        GraficoColonne.Width = 0.7;
        BindingContext = vM;
        Grafico2.YAxes.Add(secondaryAxis);
        Grafico2.XAxes.Add(primaryAxis);
        GraficoColonne2.ItemsSource = vM.PensionSavings;
        GraficoColonne2.XBindingPath = "Months";
        GraficoColonne2.YBindingPath = "Savings";
        GraficoColonne2.Spacing = 1;
        GraficoColonne2.Width = 0.7;
        Grafico3.YAxes.Add(secondaryAxis);
        Grafico3.XAxes.Add(primaryAxis);
        GraficoColonne3.ItemsSource = vM.PensionSavings;
        GraficoColonne3.XBindingPath = "Months";
        GraficoColonne3.YBindingPath = "Savings";
        GraficoColonne3.Spacing = 1;
        GraficoColonne3.Width = 0.7;
    }
    private void Indietro(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }

    private void Home(object sender, EventArgs e)
    {
        Application.Current.MainPage = new MainPage();
    }
}