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
        //COUNT TOTAL BOOKS 
        public void TotalBooksCount()
        {
            int totalBooks = 0;
            using (var command = new SqlCommand("select count(*) from Books", connection))
            {
                connection.Open();
                totalBooks = (int)command.ExecuteScalar();
                connection.Close();
            }

            Console.WriteLine($"Total Books available in the Library : {totalBooks}");
        }

        //DISPLAY TOTAL BOOKS IN LIBRARAY
        public int GetTotalBooksList()
        {
            TotalBooksCount();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Books";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("Books in the Library:");

                        while (reader.Read())
                        {
                            int bookId = (int)reader["BookId"];
                            string title = (string)reader["Title"];
                            string author = (string)reader["Author"];
                            string genre = (string)reader["Genre"];
                            bool isBorrowed = (bool)reader["IsBorrowed"];

                            Console.WriteLine("BookId:  " + bookId + ", Title:  " + title + ", Author:      " + author + ", Genre:  " + genre + ", IsBorrowed:  " + isBorrowed);
                        }
                    }
                }
            }

            return 0;
        }

        //COUNT AVAILABLE BOOKS EXCEPT BORROWED BOOKS
        public void GetAvailableBooksCount()
        {
            int availableBooks = 0;
            using (var command = new SqlCommand("select count(*) from Books where IsBorrowed !=1 ", connection))

            {
                connection.Open();
                availableBooks = (int)command.ExecuteScalar();
                connection.Close();
            }

            Console.WriteLine($"Available books count : {availableBooks}");

        }
        // DISPLAY AVAILABLE BOOKS IN LIBRARY
        public int GetAvailableBooksList()
        {
            GetAvailableBooksCount();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Books where IsBorrowed !=1";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("Books in the Library:");

                        while (reader.Read())
                        {
                            int bookId = (int)reader["BookId"];
                            string title = (string)reader["Title"];
                            string author = (string)reader["Author"];
                            string genre = (string)reader["Genre"];
                            bool isBorrowed = (bool)reader["IsBorrowed"];

                            Console.WriteLine("BookId: " + bookId + ", Title: " + title + ", Author: " + author + ", Genre: " + genre + ", IsBorrowed: " + isBorrowed);
                        }
                    }
                }
            }
            return 0;
        }

        //COUNT BORROWED BOOKS
        public void GetBorrowedBooksCount()
        {
            int borrowedBooks = 0;
            using (var command = new SqlCommand("select count(*) from Books where IsBorrowed =1 ", connection))

            {
                connection.Open();
                borrowedBooks = (int)command.ExecuteScalar();
                connection.Close();
            }

            Console.WriteLine($"Borrowed  books  : {borrowedBooks}");

        }

        //DISPLAY BORROWED BOOKS LIST
        public void GetBorrowedBooksList()
        {
            GetBorrowedBooksCount();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Books where IsBorrowed =1";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("Books in the Library:");

                        while (reader.Read())
                        {
                            int bookId = (int)reader["BookId"];
                            string title = (string)reader["Title"];
                            string author = (string)reader["Author"];
                            string genre = (string)reader["Genre"];
                            bool isBorrowed = (bool)reader["IsBorrowed"];

                            Console.WriteLine("BookId: " + bookId + ", Title: " + title + ", Author: " + author + ", Genre: " + genre + ", IsBorrowed: " + isBorrowed);
                        }
                    }
                }
            }
        }

        // DISPLAY BOOKS BY AUTHOR
        public SqlDataReader GetBooksByAuthor(string author)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string get_Book_By_Author = "select * from Books where Author =@Author";
            SqlCommand command = new SqlCommand(get_Book_By_Author, connection);
            command.Parameters.AddWithValue("@Author", author);

            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }

        //DISPLAY BOOKS BY GENRE
        public SqlDataReader GetBooksByGenre(string genre)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string get_Book_By_Genre = "select * from Books where Genre =@Genre";
            SqlCommand command = new SqlCommand(get_Book_By_Genre, connection);
            command.Parameters.AddWithValue("@Genre", genre);

            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

            return reader;

        }
        //Borrow book
        public void BorrowBook(int bookId)
        {
            using (var command = new SqlCommand("update Books set IsBorrowed = 1 where BookId=@BookId", connection))
            {
                command.Parameters.AddWithValue("@BookId", bookId);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void ReturnBook(int bookId)
        {
            using (var command = new SqlCommand("update Books set IsBorrowed = 0 where BookId=@BookId", connection))
            {
                command.Parameters.AddWithValue("@BookId", bookId);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public Book GetBookDetails(int bookId)
        {
            Book book = null;

            using (var command = new SqlCommand("SELECT * FROM Books WHERE BookId = @BookId", connection))
            {
                command.Parameters.AddWithValue("@BookId", bookId);
                connection.Open();

                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string title = (string)reader["Title"];
                    string author = (string)reader["Author"];
                    string genre = (string)reader["Genre"];
                    bool isAvailable = (bool)reader["IsBorrowed"];

                    book = new Book(bookId, title, author, genre, isAvailable);
                }

                connection.Close();
            }

            return book;
        }
    }
}
