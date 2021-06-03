using System;
using System.Collections.Generic;
using System.Text;

namespace Libri
{
    abstract class Libro
    {
        public string Titolo { get; set; }
        public string Autore { get; set; } 
        public string ISBN { get; set; }


        public Libro(string titolo, string autore, string isbn)
        {
            Titolo = titolo;
            Autore = autore;
            ISBN = isbn;
        }
    }
}
