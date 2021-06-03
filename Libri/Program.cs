using System;

namespace Libri
{
    class Program
    {

        // Creare un programma per la gestione di libri da parte del proprietario del sito
        // I libri hanno titolo - autore - codice ISBN -> abstract
        // Gli audiolibri hanno anche la durata in minuti
        // I libri cartacei hanno il numero di pagine e la quantità in magazzino
        // due libri sono uguali se hanno lo stesso ISBN( cartecei e audiolibri NON hanno lo stesso ISBN)
        // Il proprietario può vedere tutti i libri stampando titolo, autore e se è o no audiolibro
        // vedere tutta la lista di libri cartacei
        // vedere tutta la lista di audiolibri
        // Modificare la quantità di libri cartacei in magazzino
        // Modificare la durata in minuti di un audiolibro
        // Se inserisce un titolo gli viene mostrato sia il libro cartaceo che l'audiolibro
        // Se inserisce un nuovo libro cartaceo o audiolibro, 
        // prima di inserirlo verificare che non sia già presente tramite codice ISBN)
        // Gestire il db sia in connected mode che in disconnected mode


        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine();
                Console.WriteLine("-------------------------------Menù-----------------------------");
                Console.WriteLine("1. Visualizza tutti i libri presenti in magazzino");
                Console.WriteLine("2. Visualizza la lista dei libri cartacei");
                Console.WriteLine("3. Visualizza la lista degli audiolibri");
                Console.WriteLine("4. Modifica la quantità dei libri cartacei presenti in magazzino");
                Console.WriteLine("5. Modifica la durata di un audiolibro");
                Console.WriteLine("6. Visualizza libro e audiolibro con lo stesso titolo");
                Console.WriteLine("7. Inserisci un nuovo libro");
                Console.WriteLine("8. Inserisci un nuovo audiolibro");
                Console.WriteLine("0. Esci");
                Console.WriteLine("----------------------------------------------------------------");
                Console.WriteLine();
                int scelta;
                do
                {
                    Console.Write("Fai la tua scelta: ");
                }while(!int.TryParse(Console.ReadLine(),out scelta) && scelta>=0 && scelta<=8);

                Console.WriteLine();
                switch (scelta)
                {
                    case 1:
                        Console.WriteLine("Libri presenti in magazzino: ");
                        Console.WriteLine();
                        //DbManagerConnectedMode.GetAllBooks();
                        DbManagerDisconnectedMode.GetAllBooks();
                        break;
                    case 2:
                        DbManagerConnectedMode.GetAllPaperBooks();
                        /*DbManagerDisconnectedMode.GetAllPaperBooks()*/
                        ;
                        break;
                    case 3:
                        DbManagerConnectedMode.GetAllAudioBooks();
                        //DbManagerDisconnectedMode.GetAllAudioBooks();
                        break;
                    case 4:
                        Console.Write("Inserisci il titolo del libro che desideri modificare: ");
                        string titolo = Console.ReadLine();
                        int quantita;
                        if (DbManagerConnectedMode.PaperBookExistByTitle(titolo))
                        {
                            do
                            {
                                Console.Write("Inserisci la nuova quantità: ");
                            } while (!int.TryParse(Console.ReadLine(), out quantita) && quantita >= 0);
                            DbManagerConnectedMode.UpdateQuantity(titolo, quantita);
                            //DbManagerDisconnectedMode.UpdateQuantity(titolo, quantita);
                            
                            Console.WriteLine();
                            Console.WriteLine("Quantità aggiornata con successo!");
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Il libro selezionato non è presente nella lista di libri cartacei");
                        }
                        break;
                    case 5:
                        Console.Write("Inserisci il titolo del libro che desideri modificare: ");
                        string t = Console.ReadLine();
                        int durata;
                        if (DbManagerConnectedMode.AudioBookExistByTitle(t))
                        {
                            do
                            {
                                Console.Write("Inserisci la nuova durata in minuti: ");
                            } while (!int.TryParse(Console.ReadLine(), out durata) && durata > 0);

                            DbManagerConnectedMode.UpdateDurate(t, durata);
                            //DbManagerDisconnectedMode.UpdateDurate(t, durata);
                            Console.WriteLine();
                            Console.WriteLine("Durata aggiornata con successo!");
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Il libro selezionato non è presente nella lista di audiolibri");
                        }
                        break;
                    case 6:
                        Console.Write("Inserisci il titolo del libro: ");
                        string tit = Console.ReadLine();
                        if(DbManagerConnectedMode.AudioBookExistByTitle(tit) || DbManagerConnectedMode.PaperBookExistByTitle(tit))
                        {
                            Console.WriteLine();
                            DbManagerConnectedMode.GetBookByTitle(tit);
                            //DbManagerDisconnectedMode.GetBookByTitle(tit);
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Libro non presente nella lista");
                        }
                        break;
                    case 7:
                        Console.Write("Inserisci il codice ISBN del libro cartaceo che vuoi aggiungere: ");
                        string isbn = Console.ReadLine();
                        if (!DbManagerConnectedMode.PaperBookExistByIsbn(isbn))
                        {
                            Console.Write("Inserisci il titolo: ");
                            string title = Console.ReadLine();
                            Console.Write("Inserisci l'autore: ");
                            string autore = Console.ReadLine();
                            int numeroPagine;
                            int quantity;
                            do
                            {
                                Console.Write("Inserisci il numero di pagine: ");
                            } while (!int.TryParse(Console.ReadLine(), out numeroPagine) && numeroPagine> 0);

                            do
                            {
                                Console.Write("Inserisci la quantità: ");
                            } while (!int.TryParse(Console.ReadLine(), out quantity) && quantity >= 0);


                            LibroCartaceo libro = new LibroCartaceo(title, autore, isbn, numeroPagine,quantity);
                            //DbManagerConnectedMode.InsertNewPaperBook(libro);
                            DbManagerDisconnectedMode.InsertNewPaperBook(libro);

                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Libro già presente in magazzino");
                        }
                        break;
                    case 8:
                        Console.Write("Inserisci il codice ISBN dell'audiolibro che vuoi aggiungere: ");
                        string isbn1 = Console.ReadLine();
                        if (!DbManagerConnectedMode.AudioBookExistByIsbn(isbn1))
                        {
                            Console.Write("Inserisci il titolo: ");
                            string title = Console.ReadLine();
                            Console.Write("Inserisci l'autore: ");
                            string autore = Console.ReadLine();
                            int minuti;
                            do
                            {
                                Console.Write("Inserisci la durata in minuti: ");
                            } while (!int.TryParse(Console.ReadLine(), out minuti)&& minuti>0);

                            AudioLibro audiolibro = new AudioLibro(title, autore, isbn1, minuti);
                            //DbManagerConnectedMode.InsertNewAudioBook(audiolibro);
                            DbManagerDisconnectedMode.InsertNewAudioBook(audiolibro);    
                            
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Audiolibro già presente in magazzino");
                        }
                        break;
                    case 0:
                        return;

                }

            } while (true);
        }
    }
}
