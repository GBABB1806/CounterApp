#if ANDROID
using Android.Preferences;
#endif
using Syncfusion.Maui.Charts;
using Microsoft.Maui.Controls;
using System.Text.Json;

namespace CounterApp
{
    public partial class MainPage : ContentPage
    {
        public DateTime Ora {  get; set; }
        public MainPage()
        {
            Utente utente = App.UtenteInUso;
            Ora = DateTime.Now;
            InitializeComponent();
            VediProdotto listaDati = new VediProdotto();
            listaDati.Prodotti.Add(new Data());
            listaDati.Prodotti.Add(new Data() { NomeSpesa = "Risparmi", Spesa = 0});
            listaDati.Prodotti.Add(new Data() { NomeSpesa = "Investimenti", Spesa = 20});
            listaDati.Prodotti[0].NomeSpesa = "Spese";
            for (int i = 0; i < App.UtenteInUso.Spese.Count; i++)
                listaDati.Prodotti[0].Spesa += App.UtenteInUso.Spese[i].Valore;                        
            listaDati.Prodotti[1].Spesa += App.UtenteInUso.Risparmi.Pensione[Ora.Month-1];            
            listaDati.Prodotti[1].Spesa += App.UtenteInUso.Risparmi.PianoAccumulo[Ora.Month-1];
            listaDati.Prodotti[1].Spesa += App.UtenteInUso.Risparmi.MomentiDifficili[Ora.Month-1];           
            GraficoTorta.ItemsSource = listaDati.Prodotti;
            BindingContext = listaDati;
            AggiornaSoldi();
            App.SalvaDatoUtente(App.UtenteInUso);
        }
        private void Spese_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Expenses());
        }
        private void Login(object sender, EventArgs e)
        {
            App.Current.MainPage = new LoginPage();
        }
        private void Saves_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Saves());
        }
        private void SpesaPiùGrande_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AggiungiElemento(new Expenses(), -1));        
        }
        public void AggiornaSoldi()
        {
            var denaroTotale = 0.0;
            foreach (var spesa in App.UtenteInUso.Spese)
            {
                denaroTotale -= spesa.Valore;
            }
            denaroTotale += App.UtenteInUso.Risparmi.Pensione[Ora.Month-1];   
            denaroTotale += App.UtenteInUso.Risparmi.MomentiDifficili[Ora.Month-1];   
            denaroTotale += App.UtenteInUso.Risparmi.PianoAccumulo[Ora.Month-1];   
           
            Soldi.Text = Convert.ToDouble(denaroTotale).ToString() + " €";
            if(Convert.ToDouble(denaroTotale)>0)
                Soldi.TextColor = new Color(0, 255, 0);
            else 
                Soldi.TextColor = new Color(255, 0, 0);
            
        }

        private void RisparmioPiùGrande_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AggiungiElemento( new Saves(), false));
        }

        private void InvestimentClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new InvestimentPage());
        }
    }

}
