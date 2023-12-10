using System.Diagnostics;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using System.Text;

namespace CounterApp
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void ControlloEHome(object sender, EventArgs e)
        {

            //Recupero il path della cartella in cui si trova l'applicazione
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
                        byte[] bytes;
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
                    
                    command.CommandText = @"SELECT email, password FROM Utente";
                    commandScrivi.CommandText = @"INSERT INTO Utente(email, password) VALUES ($email, $password);";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Debug.WriteLine(reader.GetString(0));
                            Debug.WriteLine(reader.GetString(1));
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
                                Console.WriteLine("Il valore è già presente nella tabella.");
                                var controllaPassword = connection.CreateCommand();
                                controllaPassword.CommandText = @"
                                SELECT COUNT (*)
                                FROM Utente
                                WHERE Password";
                            }
                            //Credenziali non valide
                            else
                            {
                                commandScrivi.Parameters.AddWithValue("$email", InputEmail.Text);
                                commandScrivi.Parameters.AddWithValue("$password", InputPassword.Text);
                                commandScrivi.ExecuteNonQuery();
                            }
                        }
                        //Se non ho righe le creo col comando
                        else
                        {
                            commandScrivi.Parameters.AddWithValue("$email", InputEmail.Text);
                            commandScrivi.Parameters.AddWithValue("$password", InputPassword.Text);
                            commandScrivi.ExecuteNonQuery();
                        }
                    }        
                }
                App.Current.MainPage = new MainPage();
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
}