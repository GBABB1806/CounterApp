using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CounterApp 
{
    /// <summary>
    /// Classe che serve per la visualizzazione del prodotto
    /// all'interno del grafico e contiene una lista osservabile
    /// link del video:https://www.youtube.com/watch?v=2WDZfXpMZsE
    /// </summary>
    internal class VediProdotto
    {
        public ObservableCollection<Data> Prodotti {get;set;}
        public VediProdotto(string[] nomi, double[] spese)
        {
            Prodotti = new ObservableCollection<Data>
            {
                new Data() { NomeSpesa =  nomi[0], Spesa = spese[0]},
                new Data() { NomeSpesa =  nomi[1], Spesa = spese[1]},
                new Data() { NomeSpesa =  nomi[2], Spesa = spese[2]},
            };
        }
    }
}
