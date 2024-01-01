using System.Collections.ObjectModel;

namespace CounterApp
{
    /// <summary>
    /// Classe che serve per la visualizzazione del prodotto
    /// all'interno del grafico e contiene una lista osservabile
    /// link del video:https://www.youtube.com/watch?v=2WDZfXpMZsE
    /// </summary>
    internal class VediProdotto
    {
        public ObservableCollection<Data> Prodotti { get; set; }
        public VediProdotto(string[] nomi, double[] spese)
        {
            Prodotti = new ObservableCollection<Data>();
            for (int i = 0; i < nomi.Length; i++)
                Prodotti.Add(new Data() { NomeSpesa = nomi[i], Spesa = spese[i] });
        }
        public VediProdotto()
        {
            Prodotti = new ObservableCollection<Data>();  
        }
    }
}
