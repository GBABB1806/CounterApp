using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Maui.Controls;

namespace CounterApp
{
    public class FredApiResponse
    {
        public FredObservation[] Observations { get; set; }
    }

    public class FredObservation
    {
        public string Value { get; set; }
        // Altri campi necessari dal JSON di risposta
    }

}
