using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CounterApp
{
    public class Utente
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Spesa> Spese { get; set; }
        public Risparmio Risparmi { get; set; }

        public Utente(string email, string password)
        {
            Email = email;
            Password = password;
            Spese = new List<Spesa>();
            Risparmi = new Risparmio();
        }
        public Utente ()
        {
            Spese = new List<Spesa>();
            Risparmi = new Risparmio();

        }
    }
}
