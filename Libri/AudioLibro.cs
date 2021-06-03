using System;
using System.Collections.Generic;
using System.Text;

namespace Libri
{
    class AudioLibro: Libro
    {
        public int Durata { get; set; }

        public AudioLibro(string titolo,string autore,string isbn,int durata)
            :base(titolo,autore,isbn)
        {
            Durata = durata;
        }
    }
}
