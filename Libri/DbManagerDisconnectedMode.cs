using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Libri
{
    class DbManagerDisconnectedMode
    {
        const string connectionString = @"Data Source= (localdb)\mssqllocaldb;" +
                                         "Initial Catalog = Libreria;" +
                                         "Integrated Security=true;";
        public static void GetAllBooks()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "select Titolo, Autore from dbo.LibriCartacei";


                SqlDataAdapter adapter = new SqlDataAdapter();
                DataSet dataSet = new DataSet();
                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "LibriCartacei");

                bool audioLibro = false;

                foreach (DataRow row in dataSet.Tables["LibriCartacei"].Rows)
                {
                    var titolo = row["Titolo"];
                    var autore = row["Autore"];
                    Console.WriteLine($"Titolo: {titolo} \tAutore: {autore} \tAudiolibro: {audioLibro}");
                    Console.WriteLine("--------------------------------------------------------------------------------");
                }


                SqlCommand command1 = new SqlCommand();
                command1.Connection = connection;
                command1.CommandType = System.Data.CommandType.Text;
                command1.CommandText = "select Titolo, Autore from dbo.AudioLibri";
                DataSet dataSet1 = new DataSet();
                adapter.SelectCommand = command1;
                adapter.Fill(dataSet1, "AudioLibri");

                audioLibro = true;

                foreach (DataRow row in dataSet1.Tables["AudioLibri"].Rows)
                {
                    var titolo = row["Titolo"];
                    var autore = row["Autore"];
                    Console.WriteLine($"Titolo: {titolo} \tAutore: {autore} \tAudiolibro: {audioLibro}");
                    Console.WriteLine("--------------------------------------------------------------------------------");
                }

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
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "AudioLibri");

                bool esiste = dataSet.Tables["AudioLibri"].Columns.Contains(isbn);

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
                command.CommandText = "select * from dbo.AudioLibri where ISBN=@ISBN";
                command.Parameters.AddWithValue("@Titolo", titolo);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "AudioLibri");

                bool esiste = dataSet.Tables["AudioLibri"].Columns.Contains(titolo);

                connection.Close();

                return esiste;

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

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "AudioLibri");

                Console.WriteLine("Lista degli audiolibri:");
                Console.WriteLine();
                foreach (DataRow row in dataSet.Tables["AudioLibri"].Rows)
                {
                    var titolo = row["Titolo"];
                    var autore = row["Autore"];
                    var isbn = row["ISBN"];
                    var durata = row["Durata"];


                    Console.WriteLine($"Titolo: {titolo} \t Autore: {autore} \t ISBN: {isbn} \tDurata: {durata}");
                    Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------");
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
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "LibriCartacei");

                bool esiste = dataSet.Tables["LibriCartacei"].Columns.Contains(isbn);

                connection.Close();

                return esiste;

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

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "LibriCartacei");

                Console.WriteLine("Lista dei libri cartacei:");
                Console.WriteLine();
                foreach (DataRow row in dataSet.Tables["LibriCartacei"].Rows)
                {
                    var titolo = row["Titolo"];
                    var autore = row["Autore"];
                    var isbn = row["ISBN"];
                    var numeroPagine = row["NumeroPagine"];
                    var quantita = row["Quantita"];

                    Console.WriteLine($"Titolo: {titolo} \t Autore: {autore} \t ISBN: {isbn} \t Numero Pagine: {numeroPagine} \tQuantità: {quantita}");
                    Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------");
                }

                connection.Close();

            }
        }

        public static void GetBookByTitle(string title)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "select * from dbo.AudioLibri where Titolo=@Titolo";
                command.Parameters.AddWithValue("@Titolo", title);

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                DataSet dataset = new DataSet();
                adapter.Fill(dataset, "AudioLibri");

                DataTable dt = dataset.Tables["AudioLibri"];


                foreach (DataRow row in dt.Rows)
                {
                    if ((string)row["Titolo"] == title)
                    {
                        var titolo = row["Titolo"];
                        var autore = row["Autore"];
                        var isbn = row["ISBN"];
                        var durata = row["Durata"];

                        Console.WriteLine($"Audiolibro: Titolo: {titolo} \tAutore: {autore} \tISBN: {isbn} \tDurata in minuti: {durata}");
                        Console.WriteLine("------------------------------------------------------------------------------------------------------------------");
                    }
                   
                }

               
                SqlCommand command1 = new SqlCommand();
                command1.Connection = connection;
                command1.CommandType = System.Data.CommandType.Text;
                command1.CommandText = "select * from dbo.LibriCartacei where Titolo=@Titolo";
                command1.Parameters.AddWithValue("@Titolo", title);
                SqlDataAdapter adapter1 = new SqlDataAdapter();
                adapter1.SelectCommand = command1;
                DataSet dataset1 = new DataSet();
                adapter1.Fill(dataset1, "LibriCartacei");

                DataTable dt1 = dataset1.Tables["LibriCartacei"];

                foreach (DataRow row in dt1.Rows)
                {
                    if ((string)row["Titolo"] == title)
                    {
                        var titolo = row["Titolo"];
                        var autore = row["Autore"];
                        var isbn = row["ISBN"];
                        var numeroPagine = row["NumeroPagine"];
                        var quantita=row["Quantita"];

                        Console.WriteLine($"Libro Cartaceo: Titolo: {titolo} \tAutore: {autore} \tISBN: {isbn} \tNumero Pagine: {numeroPagine} \tQuantità: {quantita}");
                        Console.WriteLine("------------------------------------------------------------------------------------------------------------------");
                    }

                }


                connection.Close();
            }
        }
        public static void InsertNewAudioBook(AudioLibro audiolibro)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand selectCommand = new SqlCommand();
                selectCommand.Connection = connection;
                selectCommand.CommandType = System.Data.CommandType.Text;
                selectCommand.CommandText = "select * from dbo. AudioLibri";
                

                SqlCommand insertCommand = new SqlCommand();
                insertCommand.Connection = connection;
                insertCommand.CommandType = System.Data.CommandType.Text;
                insertCommand.CommandText = "INSERT INTO dbo.AudioLibri VALUES (@Titolo, @Autore, @ISBN,@Durata)";
                insertCommand.Parameters.Add("@Titolo", System.Data.SqlDbType.NVarChar, 30, "Titolo");
                insertCommand.Parameters.Add("@Autore", System.Data.SqlDbType.NVarChar, 20, "Autore");
                insertCommand.Parameters.Add("@ISBN", System.Data.SqlDbType.NVarChar, 30, "ISBN");
                insertCommand.Parameters.Add("@Durata", System.Data.SqlDbType.Int, 30, "Durata");

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = selectCommand;
                adapter.InsertCommand = insertCommand;
                DataSet dataset = new DataSet();
                adapter.Fill(dataset, "AudioLibri");

                DataRow dr= dataset.Tables["AudioLibri"].NewRow();

                dr["Titolo"] = audiolibro.Titolo;               
                dr["Autore"] = audiolibro.Autore;               
                dr["ISBN"] = audiolibro.ISBN;
                dr["Durata"] = audiolibro.Durata;

                dataset.Tables["AudioLibri"].Rows.Add(dr);
               
                adapter.Update(dataset, "AudioLibri");

                connection.Close();
            }
        }

        public static bool PaperBookExistByTitle(string titolo)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "select * from dbo.LibriCartacei where ISBN=@ISBN";
                command.Parameters.AddWithValue("@Titolo", titolo);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "LibriCartacei");

                bool esiste = dataSet.Tables["LibriCartacei"].Columns.Contains(titolo);

                connection.Close();

                return esiste;

            }
        }

        public static void UpdateDurate(string title, int durata)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand selectCommand = new SqlCommand();
                selectCommand.Connection = connection;
                selectCommand.CommandType = System.Data.CommandType.Text;
                selectCommand.CommandText = "select * from dbo.AudioLibri";

                SqlCommand updateCommand = new SqlCommand();
                updateCommand.Connection = connection;
                updateCommand.CommandType = System.Data.CommandType.Text;
                updateCommand.CommandText = "update dbo.AudioLibri set Durata=@Durata where Titolo=@Titolo";
                updateCommand.Parameters.Add("@Durata", SqlDbType.Int, 30, "Durata");
                updateCommand.Parameters.Add("@Titolo", SqlDbType.NVarChar, 30, "Titolo");

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = selectCommand;
                adapter.UpdateCommand = updateCommand;
                DataSet dataset = new DataSet();
                adapter.Fill(dataset, "AudioLibri");


                DataTable dt = dataset.Tables["AudioLibro"];

                int count = 0;


                foreach (DataRow dr in dataset.Tables["AudioLibri"].Rows)
                {
                    if (Convert.ToString(dr["Titolo"]) == title)
                    {
                        break;
                    }

                    count++;
                }

                dt.Rows[count]["Durata"] = durata;

                adapter.Update(dataset, "AudioLibri");

                connection.Close();
            }
        }

        public static void UpdateQuantity(string title, int quantita)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand selectCommand = new SqlCommand();
                selectCommand.Connection = connection;
                selectCommand.CommandType = System.Data.CommandType.Text;
                selectCommand.CommandText = "select * from dbo.LibriCartacei";

                SqlCommand updateCommand = new SqlCommand();
                updateCommand.Connection = connection;
                updateCommand.CommandType = System.Data.CommandType.Text;
                updateCommand.CommandText = "update dbo.LibriCartacei set Quantita=@Quantita where Titolo=@Titolo";
                updateCommand.Parameters.Add("@Quantita", SqlDbType.Int, 30, "Quantita");
                updateCommand.Parameters.Add("@Titolo", SqlDbType.NVarChar, 30, "Titolo");

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = selectCommand;
                adapter.UpdateCommand = updateCommand;
                DataSet dataset = new DataSet();
                adapter.Fill(dataset, "LibriCartacei");


                DataTable dt = dataset.Tables["LibriCartacei"];

                int count = 0;


                foreach (DataRow dr in dataset.Tables["LibriCartacei"].Rows)
                {
                    if (Convert.ToString(dr["Titolo"]) == title)
                    {
                        break;
                    }

                    count++;
                }

                dt.Rows[count]["Quantita"] = quantita;

                adapter.Update(dataset, "LibriCartacei");

                connection.Close();
            }
        }

        internal static void InsertNewPaperBook(LibroCartaceo libro)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand selectCommand = new SqlCommand();
                selectCommand.Connection = connection;
                selectCommand.CommandType = System.Data.CommandType.Text;
                selectCommand.CommandText = "select * from dbo. LibriCartacei";


                SqlCommand insertCommand = new SqlCommand();
                insertCommand.Connection = connection;
                insertCommand.CommandType = System.Data.CommandType.Text;
                insertCommand.CommandText = "INSERT INTO dbo.LibriCartacei VALUES (@Titolo, @Autore, @ISBN, @NumeroPagine,@Quantita)";
                insertCommand.Parameters.Add("@Titolo", System.Data.SqlDbType.NVarChar, 30, "Titolo");
                insertCommand.Parameters.Add("@Autore", System.Data.SqlDbType.NVarChar, 20, "Autore");
                insertCommand.Parameters.Add("@ISBN", System.Data.SqlDbType.NVarChar, 30, "ISBN");
                insertCommand.Parameters.Add("@NumeroPagine", System.Data.SqlDbType.Int, 30, "NumeroPagine");
                insertCommand.Parameters.Add("@Quantita", System.Data.SqlDbType.Int, 30, "Quantita");

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = selectCommand;
                adapter.InsertCommand = insertCommand;
                DataSet dataset = new DataSet();
                adapter.Fill(dataset, "LibriCartacei");

                DataRow dr = dataset.Tables["LibriCartacei"].NewRow();

                dr["Titolo"] = libro.Titolo;
                dr["Autore"] = libro.Autore;
                dr["ISBN"] = libro.ISBN;
                dr["NumeroPagine"] = libro.NumeroPagine;
                dr["Quantita"] = libro.Quantita;

                dataset.Tables["LibriCartacei"].Rows.Add(dr);

                adapter.Update(dataset, "LibriCartacei");

                connection.Close();
            }
        }


    }
       
}
