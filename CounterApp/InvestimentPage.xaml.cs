namespace CounterApp;

public partial class InvestimentPage : ContentPage
{
    public InvestimentPage()
	{
		InitializeComponent();
        Utente u = App.UtenteInUso;
        if (App.UtenteInUso.Investimenti is null || App.UtenteInUso.Investimenti.Count == 0)
        {
            App.UtenteInUso.Investimenti = new List<Investimento>();
            App.UtenteInUso.Investimenti.Add(new Investimento());
            App.UtenteInUso.Investimenti.Add(new Investimento());
            ValoreInInput.Text = "1";
        }
        else
        {
            ValoreInInput.Text = App.UtenteInUso.Investimenti[0].ValoreBase.ToString();
            ValoreInInput2.Text = App.UtenteInUso.Investimenti[1].ValoreBase.ToString();
        }
            
        
        LoadBitMarketValue();
        LoadSPMarketValue();
	}
    private async void LoadBitMarketValue()
    {
        FredApiService alphaVantageService = new FredApiService();
        string currentValue = await alphaVantageService.GetBitcoinMarketValueAsync();
        var currentValueC = currentValue.ToCharArray();
        for(int i = 0; i<currentValueC.Length;i++)
            if (currentValueC[i] == '.')
                currentValueC[i] = ',';
        string currentValueN = new string(currentValueC);
        double valoreDiUnBitCoin = Convert.ToDouble(currentValueN);
        double parteDiBitcoinSuCuiInvesto = Convert.ToDouble(ValoreInInput.Text) / valoreDiUnBitCoin;
        double valoreAttuale = valoreDiUnBitCoin*parteDiBitcoinSuCuiInvesto;
        variationLabel.Text = $"Numero di BitCoin {Math.Round(parteDiBitcoinSuCuiInvesto,8)}";
        NuovoValore.Text = $"Nuovo valore:  {Math.Round(valoreAttuale, 4)} $";
        Investimento investimento = new Investimento() { NomeMercato = "BitCoin", NumeroQuote = parteDiBitcoinSuCuiInvesto, NuovoValore = valoreAttuale, ValoreBase = Convert.ToDouble(ValoreInInput.Text) };
        App.UtenteInUso.Investimenti[0] = investimento;
        App.SalvaDatoUtente(App.UtenteInUso);
    }

    private async void LoadSPMarketValue()
    {
        FredApiService fredApiService = new FredApiService();
        string currentValue = await fredApiService.GetSendP500MarketValueAsync();
        var currentValueC = currentValue.ToCharArray();
        for (int i = 0; i < currentValueC.Length; i++)
            if (currentValueC[i] == '.')
                currentValueC[i] = ',';
        string currentValueN = new string(currentValueC);
        double valoreDiUnBitCoin = Convert.ToDouble(currentValueN);
        double parteDiBitcoinSuCuiInvesto = Convert.ToDouble(ValoreInInput2.Text) / valoreDiUnBitCoin;
        double valoreAttuale = valoreDiUnBitCoin * parteDiBitcoinSuCuiInvesto;
        variationLabel2.Text = $"Numero di Quote acquistate: {Math.Round(parteDiBitcoinSuCuiInvesto, 8)}";
        NuovoValore2.Text = $"Nuovo valore:  {Math.Round(valoreAttuale, 4)} $";
        Investimento investimento = new Investimento() { NomeMercato = "S&P500", NumeroQuote = parteDiBitcoinSuCuiInvesto, NuovoValore = valoreAttuale, ValoreBase = Convert.ToDouble(ValoreInInput2.Text) };
        App.UtenteInUso.Investimenti[1] = investimento;
        App.SalvaDatoUtente(App.UtenteInUso);
    }
    private void CambiamentoSoldi(object sender, TextChangedEventArgs e)
    {
        if(ValoreInInput.Text is not null && ValoreInInput.Text != "")
            LoadBitMarketValue();
        App.SalvaDatoUtente(App.UtenteInUso);
    }

    private void CambiamentoSoldi2(object sender, TextChangedEventArgs e)
    {
        if (ValoreInInput2.Text is not null && ValoreInInput.Text != "")
            LoadSPMarketValue();
        App.SalvaDatoUtente(App.UtenteInUso);
    }
}