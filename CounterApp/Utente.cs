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
        public List<Spesa> Spesa { get; set; }
        public Risparmio Risparmio { get; set; }

        public Utente(string email, string password)
        {
            Email = email;
            Password = password;
            Spesa = new List<Spesa>();
            Risparmio = new Risparmio();
        }
        public Utente ()
        {
            Spesa = new List<Spesa>();
            Risparmio = new Risparmio();

        }
    }
}
