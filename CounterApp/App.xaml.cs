using Microsoft.Data.Sqlite;
using System.Diagnostics;
using System.Text.Json;

namespace CounterApp
{
    public partial class App : Application
    {
        
        public static object utenteLock = new object();
        public static Utente UtenteInUso {  get; set; }
        private static List<Utente> userList = new List<Utente>();
        public static object userListLock = new object();
        public static List<Utente> UserList
        {
            get
            {
                lock (userListLock)
                {
                    return new List<Utente>(userList); // Ritorna una copia della lista per evitare modifiche esterne
                }
            }
            set
            {
                lock (userListLock)
                {
                    userList = new List<Utente>(value); // Assegna una nuova lista per evitare riferimenti condivisi
                }
            }
        }
        public App()
        {
            
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(@"Ngo9BigBOggjHTQxAR8/V1NHaF1cWWhIYVVpR2Nbe05xdF9HYlZRQmYuP1ZhSXxQd0djXn5ccHVVRmdcVkU=");
            InitializeComponent();
            UserList = DownloadUserListFromDatabase();
            UtenteInUso = new Utente();
            MainPage = new AppShell();
            if(RecuperaDatoUtente() != null)
                UtenteInUso = RecuperaDatoUtente();
        }
        private List<Utente> DownloadUserListFromDatabase()
        {
            List<Utente> listaDati = new List<Utente>();
            string fileName = "DatabaseMaui.db";
            string targetFile = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, fileName);
            try
            {
                string connectionTarget = $"Data Source={targetFile}";
                using (var connection = new SqliteConnection(connectionTarget))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"SELECT * FROM Utente";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if(reader.HasRows)
                            {
                                listaDati.Add(new Utente(reader.GetString(1), reader.GetString(2)));
                            }
                            else
                            {
                                
                            }
                        }
                        
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return listaDati;
        }
        public static Utente RecuperaDatoUtente()
        {
            string datiSerializzati = Preferences.Get("ChiaveUtente", null);
            try
            {
                if (datiSerializzati != null)
                {
                    var options = new JsonSerializerOptions
                    {
                        IncludeFields = true
                    };
                    return JsonSerializer.Deserialize<Utente>(datiSerializzati, options);
                    //return JsonSerializer.Deserialize<Utente>(datiSerializzati);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Errore durante la deserializzazione: {ex.Message}");
                throw;
            }
            return null; // o un valore predefinito, a seconda del tuo scenario
        }

        public static void SalvaDatoUtente(Utente utente)
        {
            string datiSerializzati = JsonSerializer.Serialize(utente);
            Preferences.Set("ChiaveUtente", datiSerializzati);
        }
    }
}
