using OxyPlot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CounterApp
{
    public class ViewModel
    {
        public ObservableCollection<Model> PensionSavings { get; set; }
        public ObservableCollection<Model> EmergencySavings { get; set; }

        public ViewModel() 
        {
            PensionSavings = new ObservableCollection<Model>();
            EmergencySavings = new ObservableCollection<Model>();
            Utente utente = App.UtenteInUso;
            for (int i = 0; i < 12; i++)
            {
                PensionSavings.Add(new Model() { Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i + 1).Substring(0, 3), Savings = App.UtenteInUso.Risparmi.Pensione[i] });
                EmergencySavings.Add(new Model() { Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i + 1).Substring(0, 3), Savings = App.UtenteInUso.Risparmi.MomentiDifficili[i] });
            }
        }
        public ViewModel(Risparmio risparmio)
        {
            PensionSavings = new ObservableCollection<Model>();
            EmergencySavings = new ObservableCollection<Model>();

            for (int i = 0; i < 12; i++)
            {
                PensionSavings.Add(new Model() { Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i + 1).Substring(0,3), Savings = risparmio.Pensione[i] });
                EmergencySavings.Add(new Model() { Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i + 1).Substring(0,3), Savings = risparmio.MomentiDifficili[i] });
            }
        }
    }
}
