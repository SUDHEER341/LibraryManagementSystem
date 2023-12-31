﻿using System.Data.SqlClient;
using System.Net;
using System.Net.NetworkInformation;

namespace LibraryManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=DESKTOP-4FCMKC7;Initial Catalog=LibraryManagementSystem;Integrated Security=True;";
            
            Library objLibrary = new Library(connectionString);

            //Add books BookId

            Book book1 = new Book(1, "Geetanjali ", "RavindraNath Tagore ", "Literature", false);
            Book book2 = new Book(2, "Wings of fire ", "Apj Abdhul kalam ", "Autobiography", false);
            Book book3 = new Book(3, "The white tiger", "Aravindh adiga", "Fiction", false);
            Book book4 = new Book(4, "My experiments with truth ", "Mahatma Gandhi", "Autobiography", true);
            Book book5 = new Book(5, " Gora ", "RavindraNath Tagore", "Novel", true);

            objLibrary.AddBook(book1);
            objLibrary.AddBook(book2);
            objLibrary.AddBook(book3);
            objLibrary.AddBook(book4);

            //display total  count
            objLibrary.TotalBooksCount();

            // display list of books available in library
            objLibrary.GetTotalBooksList();

            Console.WriteLine("====================================================================================================");

            // display available books in library

            objLibrary.GetAvailableBooksList();

            Console.WriteLine("====================================================================================================");
            
            objLibrary.GetBorrowedBooksList();

            Console.WriteLine("====================================================================================================");

            //retrieve books by author

            Console.WriteLine("Books by author  : ");
            SqlDataReader booksByAuthor = objLibrary.GetBooksByAuthor("RavindraNath Tagore");

            while (booksByAuthor.Read())
            {
                Console.WriteLine("Title: " + booksByAuthor["Title"] + ", Author: " + booksByAuthor["Author"] + ", Genre: " + booksByAuthor["Genre"]);
            }
            booksByAuthor.Close();

            Console.WriteLine("====================================================================================================");

            //retrieve books by genre

            Console.WriteLine("Books by Genre  : ");
            SqlDataReader booksByGenre = objLibrary.GetBooksByGenre("Fiction");

            while (booksByGenre.Read())
            {
                Console.WriteLine("Title: " + booksByGenre["Title"] + ", Author: " + booksByGenre["Author"] + ", Genre: " + booksByGenre["Genre"]);

            }
            booksByGenre.Close();
            Console.WriteLine("====================================================================================================");

            //Borrow a book 
            int bookId = 2;
            objLibrary.BorrowBook(bookId);

            Console.WriteLine("====================================================================================================");

            //Return a book
            int bookId2 = 2;
            objLibrary.ReturnBook(bookId2);

            Console.WriteLine("====================================================================================================");

            // Retrieve and display details of a specific book
            int bookId3 = 5;
            Book bookDetails = objLibrary.GetBookDetails(bookId3);
            if (bookDetails != null)
            {
                Console.WriteLine("Book Details:");
                Console.WriteLine("Title: " + bookDetails.Title);
                Console.WriteLine("Author: " + bookDetails.Author);
                Console.WriteLine("Genre: " + bookDetails.Genre);
                Console.WriteLine("Is Borrowed: " + bookDetails.IsBorrowed);
            }
            else
            {
                Console.WriteLine("Book not found!");
            }
        }
    }
}