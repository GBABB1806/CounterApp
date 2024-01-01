using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CounterApp
{
    public class Spesa
    {
        public string Nome {  get; set; }
        public double Valore { get; set; }
        public Spesa(string n,  double valore)
        {
            Nome = n;
            Valore = valore;
        }
    }
}
