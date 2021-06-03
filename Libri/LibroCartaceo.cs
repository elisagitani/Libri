using System;
using System.Collections.Generic;
using System.Text;

namespace Libri
{
    class LibroCartaceo:Libro
    {
        public int NumeroPagine { get; set; }
        public int Quantita { get; set; }

        public LibroCartaceo(string titolo,string autore, string isbn, int numeroPagine, int quantita)
            :base(titolo,autore,isbn)
        {
            NumeroPagine = numeroPagine;
            Quantita = quantita;
        }

    }
}
