using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;

namespace CounterApp
{
    public class FredApiService
    {
        private const string ApiKey = "130639ed91c195cf66af9b9750a73971";
        private const string ApiUrl = "https://api.stlouisfed.org/fred/";

        public async Task<string> GetBitcoinMarketValueAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Costruisci l'URL della richiesta API
                    string apiUrl = $"https://api.stlouisfed.org/fred/series/observations?series_id=CBBTCUSD&api_key=130639ed91c195cf66af9b9750a73971&file_type=json";

                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        // Leggi la risposta JSON
                        string responseData = await response.Content.ReadAsStringAsync();

                        // Deserializza la risposta JSON per ottenere il valore del mercato dell'oro
                        var fredApiResponse = JsonConvert.DeserializeObject<FredApiResponse>(responseData);
                        // Verifica la presenza di dati validi nella risposta
                        if (fredApiResponse != null && fredApiResponse.Observations != null && fredApiResponse.Observations.Any())
                        {
                            string goldMarketValue = fredApiResponse.Observations.Last().Value;
                            return goldMarketValue;
                        }
                        else
                        {
                            Console.WriteLine("La risposta non contiene dati validi.");
                            return "0"; // O un valore di fallback appropriato
                        }
                    }
                    else
                    {
                        // Gestisci gli errori di risposta HTTP
                            Console.WriteLine($"Errore nella richiesta API: {response.StatusCode}");
                        return "0"; // Valore di fallback
                    }
                }
            }
            catch (Exception ex)
            {
                // Gestisci eccezioni
                Console.WriteLine($"Errore durante la richiesta API: {ex}");
                return "0"; // Valore di fallback
            }
            //catch (HttpRequestException ex)
            //{
            //    Console.WriteLine($"Errore di connessione: {ex.Message}");
            //}

        }
        public async Task<string> GetSendP500MarketValueAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Costruisci l'URL della richiesta API
                    string apiUrl = $"https://api.stlouisfed.org/fred/series/observations?series_id=SP500&api_key=130639ed91c195cf66af9b9750a73971&file_type=json";

                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        // Leggi la risposta JSON
                        string responseData = await response.Content.ReadAsStringAsync();

                        // Deserializza la risposta JSON per ottenere il valore del mercato dell'oro
                        var fredApiResponse = JsonConvert.DeserializeObject<FredApiResponse>(responseData);
                        // Verifica la presenza di dati validi nella risposta
                        if (fredApiResponse != null && fredApiResponse.Observations != null && fredApiResponse.Observations.Any())
                        {
                            string goldMarketValue = fredApiResponse.Observations.Last().Value;
                            return goldMarketValue;
                        }
                        else
                        {
                            Console.WriteLine("La risposta non contiene dati validi.");
                            return "0"; // O un valore di fallback appropriato
                        }
                    }
                    else
                    {
                        // Gestisci gli errori di risposta HTTP
                        Console.WriteLine($"Errore nella richiesta API: {response.StatusCode}");
                        return "0"; // Valore di fallback
                    }
                }
            }
            catch (Exception ex)
            {
                // Gestisci eccezioni
                Console.WriteLine($"Errore durante la richiesta API: {ex}");
                return "0"; // Valore di fallback
            }
        }
    }
}
