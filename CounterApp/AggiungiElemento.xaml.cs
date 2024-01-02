using Microsoft.Data.Sqlite;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Transactions;

namespace CounterApp;

public partial class AggiungiElemento : ContentPage
{
    public DateTime Ora {  get; set; }
    private string _tabellaAggiungere;
    private string _valoreInserito;
    private string _nomeCampo;
    private int _parametrocontrollo;
    public AggiungiElemento()
    {
        InitializeComponent();
        App.SalvaDatoUtente(App.UtenteInUso);
    }
    private Expenses ExpensesPage; // Aggiungi campo per riferimento
    public AggiungiElemento(Expenses expenses)
    {
        InitializeComponent();
        App.SalvaDatoUtente(App.UtenteInUso);
        sceltaTipo.IsEnabled = false;
        sceltaTipo.SelectedIndex = 0;
        NomeSpesa.IsVisible = true;
        Valore.IsVisible = true;
        ExpensesPage = expenses;
    }
    public AggiungiElemento(Expenses expenses, int a)
    {
        InitializeComponent();
        App.SalvaDatoUtente(App.UtenteInUso);
        sceltaTipo.IsEnabled = false;
        sceltaTipo.SelectedIndex = 0;
        NomeSpesa.IsVisible = true;
        Valore.IsVisible = true;
        ExpensesPage = expenses;
        _parametrocontrollo = a;
    }

    public AggiungiElemento(Saves saves, bool n)
    {
        InitializeComponent();
        sceltaTipo.IsEnabled = false;
        sceltaTipo.SelectedIndex = 1;
        PickerSaves.IsVisible = true;
        if (n == true)
        {
            PickerSaves.IsEnabled = false;
            PickerSaves.SelectedIndex = 0;
        }
        //Valore.IsVisible = true;
    }

    private void SceltaTipo(object sender, EventArgs e)
    {
        switch (sceltaTipo.SelectedIndex)
        {
            case 0:
                PickerSaves.IsVisible = false;
                NomeSpesa.IsVisible = true;
                _tabellaAggiungere = "Expenses";
                break;
            case 1:
                PickerSaves.IsVisible = true;
                NomeSpesa.IsVisible= false;
                _tabellaAggiungere = "Saves";
                break;
            case 2:
                _tabellaAggiungere = "Investimenti";
                break;
        }

    }



    public void CambiaSpesa(object sender, EventArgs e)
    {
        if(NomeSpesa.Text.Length >=4)
        {
            _nomeCampo = NomeSpesa.Text;
            Valore.IsVisible = true;
            Valore.TextChanged += Valore_TextChanged;
        }
    }

    public void Valore_TextChanged(object sender, TextChangedEventArgs e)
    {
        _valoreInserito = Valore.Text;
        
        Conferma.IsVisible = true;
    }

    public void CambiaRisparmio(object sender, EventArgs e)
    {
        Entry inserireRisparmio = new Entry();
        inserireRisparmio.HorizontalOptions = LayoutOptions.Center;
        inserireRisparmio.WidthRequest = 150;
        var sceltaRisparmio = (Picker)sender;
        switch (sceltaRisparmio.SelectedIndex)
        {
            case 0:
                inserireRisparmio.Placeholder = "Inserisci un cambiamento nella pensione";
                _nomeCampo = "Pensione";
                break;
            case 1:
                inserireRisparmio.Placeholder = "Immetti i soldi accumulati questo mese";
                _nomeCampo = "PianoAccumulo";
                break;
            case 2:
                inserireRisparmio.Placeholder = "Immetti i soldi dei momenti difficili per questo mese";
                _nomeCampo = "MomentiDifficili";
                break;
        }
        GrigliaAggiunta.SetRow(inserireRisparmio, 2);
        GrigliaAggiunta.SetColumnSpan(inserireRisparmio, 2);
        GrigliaAggiunta.Add(inserireRisparmio);
        inserireRisparmio.TextChanged += InserisciValore;
        
    }

    private void InserisciValore(object sender, TextChangedEventArgs e)
    {
        Entry entry = (Entry)sender;
        entry.Keyboard = Keyboard.Numeric;
        _valoreInserito = entry.Text;
        Conferma.IsVisible = true;
    }
    private void Indietro(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
        App.SalvaDatoUtente(App.UtenteInUso);
    }
    private void Invia(object sender, EventArgs e)
    {
        double valoreInserito;
        string nome = NomeSpesa.Text;
        bool check = double.TryParse(_valoreInserito, out valoreInserito);

        if (check)
        {
            // ... Tua logica per l'invio della spesa

            // Chiudi la pagina AggiungiElemento
            Navigation.PopModalAsync();
            if (sceltaTipo.SelectedIndex == 0)
            {
                // Aggiorna la griglia delle spese nella pagina "Expenses"
                ExpensesPage.AggiornaGrigliaSpese(new Spesa(_nomeCampo, valoreInserito));
                if (_parametrocontrollo == -1)
                {
                    App.Current.MainPage = new MainPage();
                }
            }
            else if( sceltaTipo.SelectedIndex == 1)
            {
                if(_nomeCampo == "Pensione")
                    for (int i = 0; i < App.UtenteInUso.Risparmi.Pensione.Length; i++)
                        App.UtenteInUso.Risparmi.Pensione[i] = valoreInserito;
                if (_nomeCampo == "PianoAccumulo")
                    App.UtenteInUso.Risparmi.PianoAccumulo[Ora.Month - 1] += valoreInserito;
                if (_nomeCampo == "MomentiDifficili")
                    App.UtenteInUso.Risparmi.MomentiDifficili[Ora.Month - 1] += valoreInserito;
                App.SalvaDatoUtente(App.UtenteInUso);
                App.Current.MainPage = new MainPage();
            }
        }
        else
        {
            DisplayAlert("Il valore inserito non è un numero", "", "CHIUDI");
        }
    }





    private async void AggiungiDati(object sender, EventArgs e)
    {
        //Recupero il path della cartella in cui si trova l'applicazione
        string nomeFK = "FK_Id" + _tabellaAggiungere;
        string fileName = "DatabaseMaui.db";
        string bundleFileName = "DatabaseMaui.db";
        //Genero il path completo con anche il nome del file al nuovo file
        string targetFile = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, fileName);
        if (!File.Exists(targetFile))
        {
            //Se il file non esiste nel file system della app, lo copia dal bundle

            try
            {
                using FileStream outputStream = System.IO.File.OpenWrite(targetFile);
                using Stream fs = await FileSystem.Current.OpenAppPackageFileAsync(bundleFileName);
                using BinaryWriter writer = new BinaryWriter(outputStream);
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    var bytesRead = 0;
                    int bufferSize = 1024;
                    var buffer = new byte[bufferSize];
                    using (fs)
                    {
                        do
                        {
                            buffer = reader.ReadBytes(bufferSize);
                            bytesRead = buffer.Count();
                            writer.Write(buffer);
                        }

                        while (bytesRead > 0);

                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Errore1", ex.Message, "OK");
            }
        }

        try
        {
            string connectionTarget = $"Data Source={targetFile}";
            using (var connection = new SqliteConnection(connectionTarget))
            {
                connection.Open();
                var commandUtVerifica = connection.CreateCommand();
                commandUtVerifica.CommandText = @"
                SELECT name
                FROM sqlite_master
                WHERE type='table' AND name='utente'";
                var command = connection.CreateCommand();
                command.CommandText = @"
                SELECT $campoFK
                FROM Utente
                WHERE email = $email
                ";
                command.Parameters.AddWithValue("$campoFK", nomeFK);
                command.Parameters.AddWithValue("$email", "gabriele");
                using(var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        //Da scrivere
                    }
                    else
                    {
                        long ultimoId;
                        var commandNuovo = connection.CreateCommand();
                        commandNuovo.CommandText = @"
                        INSERT INTO User"+ _tabellaAggiungere + " (" + _nomeCampo + @")
                        Values
                        ($valore);
                        SELECT last_insert_rowid()
                        ";
                        commandNuovo.Parameters.AddWithValue("$tabella","User" + _tabellaAggiungere);
                        commandNuovo.Parameters.AddWithValue("$NomeCampo", _nomeCampo);
                        commandNuovo.Parameters.AddWithValue("$valore", _valoreInserito);
                        commandNuovo.ExecuteNonQuery();
                        ultimoId = (long)commandNuovo.ExecuteScalar();
                        var commandInserisciId = connection.CreateCommand();
                        commandInserisciId.CommandText = @"
                        UPDATE Utente
                        SET $nomeFK = $ultimoId
                        WHERE PK_IdUtente = $ultimoId;
                        ";
                        commandInserisciId.Parameters.AddWithValue("$nomeFK", nomeFK);
                        commandInserisciId.Parameters.AddWithValue("$ultimoId", ultimoId);
                    }
                }   
            }
            App.SalvaDatoUtente(App.UtenteInUso);
        }
        catch (Exception ex)
        {
            await DisplayAlert(ex.Message, "Errore2", "OK");
        }

    }

    public async Task MoveFile(string sourceFile, string targetFileName)
    {
        // Read the source file
        using Stream fileStream = await FileSystem.Current.OpenAppPackageFileAsync(sourceFile);
        using StreamReader reader = new StreamReader(fileStream);
        string content = await reader.ReadToEndAsync();
        // Write the file content to the app data directory
        string targetFile = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, targetFileName);
        using FileStream outputStream = System.IO.File.OpenWrite(targetFile);
        using StreamWriter streamWriter = new StreamWriter(outputStream);
        await streamWriter.WriteAsync(content);
    }


}