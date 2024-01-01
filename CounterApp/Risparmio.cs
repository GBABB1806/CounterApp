using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CounterApp
{
    public class Risparmio
    {
        public double[] Pensione {get; set;}
        public double[] PianoAccumulo { get; set;}
        public double[] MomentiDifficili { get; set;}
        public Risparmio()
        {
            Pensione = new double[12];
            PianoAccumulo = new double[12];
            MomentiDifficili = new double[12];
        }
        
    }
}
