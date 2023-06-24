using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{

    public class Library
    {
        private string connectionString;
        private SqlConnection connection;

        public Library(string connectionString)
        {
            this.connectionString = connectionString;
            connection = new SqlConnection(connectionString);
        }

        public void AddBook(Book book)
        {
            using (var command = new SqlCommand("INSERT INTO Books (BookId ,Title, Author, Genre, IsBorrowed) VALUES (@BookId ,@Title, @Author, @Genre, @IsBorrowed)", connection))
            {
                command.Parameters.AddWithValue("@BookId", book.BookId);
                command.Parameters.AddWithValue("@Title", book.Title);
                command.Parameters.AddWithValue("@Author", book.Author);
                command.Parameters.AddWithValue("@Genre", book.Genre);
                command.Parameters.AddWithValue("@IsBorrowed", book.IsBorrowed);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                Console.WriteLine("Book added successfully");
            }
        }
        
        

    }
}
