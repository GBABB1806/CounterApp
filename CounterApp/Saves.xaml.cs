using Microsoft.Maui.Platform;
using Syncfusion.Maui.Charts;
using System.Diagnostics;
using System.Globalization;

namespace CounterApp;

public partial class Saves : ContentPage
{
    public DateTime ora { get; set; }
    public DateTime unAnnoFa { get; set; }
    public Utente Utente { get; set; }
    public Saves()
    {
        var numeroZeri = 0;
        if (numeroZeri > 12)
        {
            VediPianoAccumulo.IsEnabled = false;
        }
        if (numeroZeri > 12)
        {
            VediMomentiDifficili.IsEnabled = false;
        }
        ora = DateTime.Now;
        unAnnoFa= ora.AddYears(-1);
        InitializeComponent();
        Utente = App.UtenteInUso;
        BindingContext = this;
        List<string> list = new List<string>();
        for (int i = 0; i < Utente.Risparmi.Pensione.Length/2; i++)
        {
            ora  = DateTime.Now;
            ora = ora.AddMonths(-i);
            list.Add("Mese di "+ CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(ora.Month) + " " + Utente.Risparmi.Pensione[i].ToString());
        }
        Lista1.ItemsSource = list;
        list = new List<string>();

        for (int i = 0; i < Utente.Risparmi.Pensione.Length / 2; i++)
        {
            ora = DateTime.Now;
            ora = ora.AddMonths(-i);
            list.Add("Mese di " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(ora.Month) + " " + Utente.Risparmi.PianoAccumulo[i].ToString());
        }
        Lista2.ItemsSource = list; 
        list = new List<string>();

        for (int i = 0; i < Utente.Risparmi.Pensione.Length / 2; i++)
        {
            ora = DateTime.Now;
            ora = ora.AddMonths(-i);
            list.Add("Mese di " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(ora.Month) + " " + Utente.Risparmi.MomentiDifficili[i].ToString());
        }
        Lista3.ItemsSource = list;

    }
    private void Indietro(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }
    private void ApriPensioneClicked(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new PaginaPensione());
    }

    private void ApriMomDifficiliClicked(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new PaginaMomDifficili());
    }

    private void ApriPianoAccumuloClicked(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new PaginaAccumulo());
    }
}