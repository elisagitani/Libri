using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace Libri
{
    static class DbManagerConnectedMode
    {
        const string connectionString = @"Data Source= (localdb)\mssqllocaldb;" +
                                          "Initial Catalog = Libreria;" +
                                          "Integrated Security=true;";

        public static void GetAllBooks()
        {
            using(SqlConnection connection=new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "select Titolo, Autore from dbo.LibriCartacei";
                SqlDataReader reader = command.ExecuteReader();

                bool audioLibro = false;
                while (reader.Read())
                {
                    var titolo = reader["Titolo"];
                    var autore = reader["Autore"];  

                    Console.WriteLine($"Titolo: {titolo} \tAutore: {autore} \tAudiolibro: {audioLibro}");
                    Console.WriteLine("--------------------------------------------------------------------------------");
                }

                reader.Close();

                SqlCommand command1 = new SqlCommand();
                command1.Connection = connection;
                command1.CommandType = System.Data.CommandType.Text;
                command1.CommandText = "select Titolo, Autore from dbo.AudioLibri";
                SqlDataReader reader1 = command1.ExecuteReader();
                
                audioLibro = true;
                while (reader1.Read())
                {
                    var titolo = reader1["Titolo"];
                    var autore = reader1["Autore"];

                    Console.WriteLine($"Titolo: {titolo} \tAutore: {autore} \tAudiolibro: {audioLibro}");
                    Console.WriteLine("--------------------------------------------------------------------------------");
                }


                connection.Close();
            }
        }
        public static void GetAllPaperBooks()
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand();

                command.Connection = connection;

                command.CommandType = System.Data.CommandType.Text;

                command.CommandText = "select * from dbo.LibriCartacei";

                SqlDataReader reader = command.ExecuteReader();

                Console.WriteLine("Lista dei libri cartacei:");
                Console.WriteLine();
                while (reader.Read())
                {
                    var titolo = reader["Titolo"];
                    var autore = reader["Autore"];
                    var isbn = reader["ISBN"];
                    var numeroPagine = reader["NumeroPagine"];
                    var quantita = reader["Quantita"];

                    Console.WriteLine($"Titolo: {titolo} \t Autore: {autore} \t ISBN: {isbn} \t Numero Pagine: {numeroPagine} \tQuantità: {quantita}");
                    Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------");
                }

                connection.Close();

            }

         
        }
        public static void GetAllAudioBooks()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand();

                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;

                command.CommandText = "select * from dbo.AudioLibri";

                SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine();
                Console.WriteLine("Lista degli audiolibri:");
                Console.WriteLine();
                while (reader.Read())
                {
                    var titolo = reader["Titolo"];
                    var autore = reader["Autore"];
                    var isbn = reader["ISBN"];
                    var durata = reader["Durata"];


                    Console.WriteLine($"Titolo: {titolo} \t Autore: {autore} \t ISBN: {isbn} \t Durata in minuti: {durata}");
                    Console.WriteLine("------------------------------------------------------------------------------------------------------------------");
                }

                connection.Close();
            }

        }
        internal static bool PaperBookExistByIsbn(string isbn)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "select * from dbo.LibriCartacei where ISBN=@ISBN";
                command.Parameters.AddWithValue("@ISBN", isbn);
                SqlDataReader reader = command.ExecuteReader();

                bool esiste = false;

                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        esiste = true;
                    }
                }

                connection.Close();

                return esiste;

            }
        }
        internal static void InsertNewPaperBook(LibroCartaceo libro)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "insert into dbo.LibriCartacei values (@Titolo,@Autore,@ISBN,@NumeroPagine,@Quantita)";
                command.Parameters.AddWithValue("@Titolo", libro.Titolo);
                command.Parameters.AddWithValue("@Autore", libro.Autore);
                command.Parameters.AddWithValue("@ISBN", libro.ISBN);
                command.Parameters.AddWithValue("@NumeroPagine", libro.NumeroPagine);
                command.Parameters.AddWithValue("@Quantita", libro.Quantita);

                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        public static bool PaperBookExistByTitle(string titolo)
        {
            using(SqlConnection connection=new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "select * from dbo.LibriCartacei where Titolo=@Titolo";
                command.Parameters.AddWithValue("@Titolo", titolo);
                SqlDataReader reader = command.ExecuteReader();

                bool esiste=false;

                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        esiste = true;
                    }
                }
                
                connection.Close();
                return esiste;
                
            }
        }
        public static bool AudioBookExistByTitle(string titolo)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "select * from dbo.AudioLibri where Titolo=@Titolo";
                command.Parameters.AddWithValue("@Titolo", titolo);
                SqlDataReader reader = command.ExecuteReader();

                bool esiste = false;

                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        esiste = true;
                    }
                }

                connection.Close();
                return esiste;

            }
        }
        public static void UpdateQuantity(string title, int quantita)
        {
            using(SqlConnection connection=new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "update dbo.LibriCartacei set Quantita=@Quantita where Titolo=@Titolo";
                command.Parameters.AddWithValue("@Quantita", quantita);
                command.Parameters.AddWithValue("@Titolo", title);

                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        public static void UpdateDurate(string title,int durata)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "update dbo.AudioLibri set Durata=@Durata where Titolo=@Titolo";
                command.Parameters.AddWithValue("@Durata", durata);
                command.Parameters.AddWithValue("@Titolo", title);

                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        public static bool AudioBookExistByIsbn(string isbn)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "select * from dbo.AudioLibri where ISBN=@ISBN";
                command.Parameters.AddWithValue("@ISBN", isbn);
                SqlDataReader reader = command.ExecuteReader();

                bool esiste = false;

                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        esiste = true;
                    }
                }

                connection.Close();
                
                return esiste;

            }
        }
        public static void InsertNewAudioBook(AudioLibro audiolibro)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "insert into dbo.Audiolibri values (@Titolo,@Autore,@ISBN,@Durata)";
                command.Parameters.AddWithValue("@Titolo", audiolibro.Titolo);
                command.Parameters.AddWithValue("@Autore", audiolibro.Autore);
                command.Parameters.AddWithValue("@ISBN", audiolibro.ISBN);
                command.Parameters.AddWithValue("@Durata", audiolibro.Durata);

                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        public static void GetBookByTitle(string title)
        {
            using(SqlConnection connection=new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "select * from dbo.AudioLibri where Titolo=@Titolo";
                command.Parameters.AddWithValue("@Titolo", title);
                SqlDataReader reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    var titolo = reader["Titolo"];
                    var autore = reader["Autore"];
                    var isbn = reader["ISBN"];
                    var durata = reader["Durata"];

                    Console.WriteLine($"Audiolibro: Titolo: {titolo} \t Autore: {autore} \t ISBN: {isbn} \t Durata in minuti: {durata}");
                    Console.WriteLine("------------------------------------------------------------------------------------------------------------------");
                }

                reader.Close();

                SqlCommand command1 = new SqlCommand();
                command1.Connection = connection;
                command1.CommandType = System.Data.CommandType.Text;
                command1.CommandText = "select * from dbo.LibriCartacei where Titolo=@Titolo";
                command1.Parameters.AddWithValue("@Titolo", title);
                SqlDataReader reader1 = command1.ExecuteReader();
                Console.WriteLine("Libro cartaceo: ");
                Console.WriteLine();
                while (reader1.Read())
                {
                    var titolo = reader1["Titolo"];
                    var autore = reader1["Autore"];
                    var isbn = reader1["ISBN"];
                    var numeroPagine = reader1["NumeroPagine"];
                    var quantita = reader1["Quantita"];

                    Console.WriteLine($"Libro cartaceo: Titolo: {titolo} \t Autore: {autore} \t ISBN: {isbn} \t Numero Pagine: {numeroPagine} \tQuantità: {quantita}");
                    Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------");
                }

                reader1.Close();

                connection.Close();
            }
        }
    }
}
