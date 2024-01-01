using Syncfusion.Maui.Charts;

namespace CounterApp
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            VediProdotto listaDati = new VediProdotto();
            listaDati.Prodotti.Add(new Data());
            listaDati.Prodotti.Add(new Data() { NomeSpesa = "Risparmi", Spesa = 0});
            listaDati.Prodotti[0].NomeSpesa = "Spese";
            for (int i = 0; i < App.UtenteInUso.Spesa.Count; i++)
            {
                listaDati.Prodotti[0].Spesa += App.UtenteInUso.Spesa[i].Valore;
            }
            for(int i = 0; i < App.UtenteInUso.Risparmio.Pensione.Length; i++)
            {
                listaDati.Prodotti[1].Spesa += App.UtenteInUso.Risparmio.Pensione[i];
            }
            for(int i = 0; i < App.UtenteInUso.Risparmio.PianoAccumulo.Length; i++)
            {
                listaDati.Prodotti[1].Spesa += App.UtenteInUso.Risparmio.PianoAccumulo[i];
            }
            for(int i = 0; i < App.UtenteInUso.Risparmio.MomentiDifficili.Length; i++)
            {
                listaDati.Prodotti[1].Spesa += App.UtenteInUso.Risparmio.MomentiDifficili[i];
            }
            GraficoTorta.ItemsSource = listaDati.Prodotti;
            BindingContext = listaDati;
            AggiornaSoldi();
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
            foreach (var spesa in App.UtenteInUso.Spesa)
            {
                denaroTotale -= spesa.Valore;
            }
            foreach(var risparmio in App.UtenteInUso.Risparmio.Pensione)
            {
                denaroTotale += risparmio;
            }
            foreach (var risparmio in App.UtenteInUso.Risparmio.PianoAccumulo)
            {
                denaroTotale += risparmio;
            }
            foreach (var risparmio in App.UtenteInUso.Risparmio.MomentiDifficili)
            {
                denaroTotale += risparmio;
            }
            Soldi.Text = Convert.ToDouble(denaroTotale).ToString() + " €";
        }
    }

}
