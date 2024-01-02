using System.Diagnostics;
using Microsoft.Data.Sqlite;
using System.Text.RegularExpressions;

namespace CounterApp
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        public LoginPage(string input)
        {
            
            InitializeComponent();
            if(App.UtenteInUso.Password!= null || App.UtenteInUso.Password!= null)
            {
                App.Current.MainPage = new MainPage();
            }
            InputEmail.Text = input;
        }

        private async void ControlloEHome(object sender, EventArgs e)
        {
            if(IsValidEmail(InputEmail.Text))
            {
                string mainDir = FileSystem.Current.AppDataDirectory;
                string fileName = "DatabaseMaui.db";
                string bundleFileName = "DatabaseMaui.db";
                string targetFile = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, fileName);
                if (!File.Exists(targetFile))
                {
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
                        string[] emails = new string[500];
                        string[] passwords = new string[500];
                        var commandUtVerifica = connection.CreateCommand();
                        var command = connection.CreateCommand();
                        var commandScrivi = connection.CreateCommand();
                        commandUtVerifica.CommandText = @"
                        SELECT name
                        FROM sqlite_master
                        WHERE type='table' AND name='Utente'";
                        var commandCreaTabella = connection.CreateCommand();
                        commandCreaTabella.CommandText = @"
                            CREATE TABLE IF NOT EXISTS 'Utente' (
                                'Pk_IdUtente' INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                'Email' TEXT,
                                'Password' TEXT,
                                'FK_IdUserSaves' INTEGER,
                                'FK_IdExpenses' INTEGER,
                                FOREIGN KEY('FK_IdUserSaves') REFERENCES 'UserSaves'('PK_IdUserSaves'),
                                FOREIGN KEY('FK_IdExpenses') REFERENCES 'UserExpenses'('PK_IdExpenses')
                            );

                            CREATE TABLE IF NOT EXISTS 'UserExpenses' (
                                'PK_IdExpenses' INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                'Macchina' INTEGER,
                                'Casa' INTEGER,
                                'Lavoro' INTEGER
                            );

                            CREATE TABLE IF NOT EXISTS 'UserSaves' (
                                'PK_IdUserSaves' INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                'FK_IdPensione' INTEGER,
                                'FK_IdPianoAccumulo' INTEGER,
                                'FK_IdMomentiDifficili' INTEGER,
                                FOREIGN KEY('FK_IdPensione') REFERENCES 'Pensione'('PK_PensioneId')
                            );

                            CREATE TABLE IF NOT EXISTS 'Pensione' (
                                'PK_PensioneId' INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                'Gennaio' INTEGER,
                                'Febbraio' INTEGER,
                                'Marzo' INTEGER,
                                'Aprile' INTEGER,
                                'Maggio' INTEGER,
                                'Giugno' INTEGER,
                                'Luglio' INTEGER,
                                'Agosto' INTEGER,
                                'Settembre' INTEGER,
                                'Ottobre' INTEGER,
                                'Novembre' INTEGER,
                                'Dicembre' INTEGER
                            );
                        ";
                        commandCreaTabella.ExecuteNonQuery();
                        command.CommandText = @"SELECT email, password FROM Utente";
                        commandScrivi.CommandText = @"INSERT INTO Utente(email, password) VALUES ($email, $password);";
                        App.UtenteInUso.Email = InputEmail.Text;
                        App.UtenteInUso.Password = InputPassword.Text;
                        using (var reader = commandUtVerifica.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (!reader.HasRows)
                                {

                                }
                            }
                            if (reader.HasRows)
                            {
                                var checkIfExistsCommand = connection.CreateCommand();
                                checkIfExistsCommand.CommandText =
                                @"
                            SELECT COUNT(*)
                            FROM Utente
                            WHERE Email = $email AND Password = $password
                            ";
                                checkIfExistsCommand.Parameters.AddWithValue("$email", InputEmail.Text);
                                checkIfExistsCommand.Parameters.AddWithValue("$password", InputPassword.Text);
                                int countE = Convert.ToInt32(checkIfExistsCommand.ExecuteScalar());


                                // Credenziali Valide
                                if (countE > 0)
                                {
                                    Debug.WriteLine("Il valore è già presente nella tabella.");
                                    var controllaPassword = connection.CreateCommand();
                                    controllaPassword.CommandText = @"
                                SELECT PK_IdUtente, email, password, FK_IdUserSaves
                                FROM Utente";
                                    using (var leggiUtenti = controllaPassword.ExecuteReader())
                                    {
                                        while (leggiUtenti.Read())
                                        {
                                            Debug.WriteLine("Email: " + leggiUtenti.GetString(0) + "\n Password: " + leggiUtenti.GetString(1));
                                        }
                                    }
                                    Application.Current.MainPage = new MainPage();
                                }
                                //Credenziali non valide
                                else
                                {
                                    commandScrivi.Parameters.AddWithValue("$email", InputEmail.Text);
                                    commandScrivi.Parameters.AddWithValue("$password", InputPassword.Text);
                                    commandScrivi.ExecuteNonQuery();
                                    await Navigation.PushModalAsync(new AggiungiElemento(new Saves(), true));
                                }
                            }
                            //Se non ho righe le creo col comando
                            else
                            {

                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    await DisplayAlert(ex.Message, "Errore2", "OK");
                }
            }
            else
            {
                await DisplayAlert("Email non valida", "", "CHIUDI");
            }
            //Recupero il path della cartella in cui si trova l'applicazione

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

        private void MostraPassword(object sender, CheckedChangedEventArgs e)
        {
            if(VediPwd.IsChecked == true)
                InputPassword.IsPassword = false;
            else 
                InputPassword.IsPassword = true;
        }
        public static bool IsValidEmail(string email)
        {
            // Utilizza un'espressione regolare per verificare la validità dell'indirizzo email
            string pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }
    }
}