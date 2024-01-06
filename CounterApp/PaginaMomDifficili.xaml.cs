namespace CounterApp;

public partial class PaginaMomDifficili : ContentPage
{
	public PaginaMomDifficili()
	{
		InitializeComponent();
		Utente utente = App.UtenteInUso;
		LabelMese.Text = "Monthly " + utente.Risparmi.MomentiDifficili[0].ToString() + "€";
        double totale = 0;
        for (int i = 1; i<=6; i++)
		{
            Label labelPercentuale = this.FindByName<Label>($"Percentuale{i}");
			totale = 0;
            for (int j = 0; j<12; j++)
				totale += utente.Risparmi.MomentiDifficili[i];
			labelPercentuale.Text = Convert.ToString(Convert.ToInt32((utente.Risparmi.MomentiDifficili[i] + utente.Risparmi.MomentiDifficili[i+1])/totale*100)) + "%";
        }
		UnAnno.Text = "YEAR      " + totale.ToString();
		VentAnni.Text = "20 YEARS " + (totale * 20).ToString();
	}
}