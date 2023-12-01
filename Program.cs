using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CsvHelper;
using CsvHelper.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace MovieApplication
{
       class Program
    {
        static void Main(string[] args)
        {
            using (var db = new MovieDbContext())
            {
                db.Database.Migrate();

                bool exit = false;
                while (!exit)
                {
                    Console.WriteLine("Choose an option:");
                    Console.WriteLine("1. List Movies");
                    Console.WriteLine("2. Add Movie");
                    Console.WriteLine("3. Update Movie");
                    Console.WriteLine("4. Delete Movie");
                    Console.WriteLine("5. Search Movies");
                    Console.WriteLine("6. Exit");

                    string option = Console.ReadLine();

                    switch (option)
                    {
                        case "1":
                            ListMovies(db);
                            break;

                        case "2":
                            AddMovie(db);
                            break;

                        case "3":
                            UpdateMovie(db);
                            break;

                        case "4":
                            DeleteMovie(db);
                            break;

                        case "5":
                            SearchMovies(db);
                            break;

                        case "6":
                            exit = true;
                            break;

                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
            }
        }

        static void ListMovies(MovieDbContext db)
        {
            var movies = db.Movies.ToList();
            foreach (var movie in movies)
            {
                movie.Display();
            }
        }

        static void AddMovie(MovieDbContext db)
        {
            Console.WriteLine("Enter the title of the movie:");
            string title = Console.ReadLine();

            if (db.Movies.Any(m => m.Title.ToUpper() == title.ToUpper()))
            {
                Console.WriteLine("Movie with the same title already exists.");
                return;
            }

            Console.WriteLine("Enter the genres of the movie (comma-separated):");
            string genres = Console.ReadLine();

            var movie = new Movie
            {
                Title = title,
                Genres = genres
            };

            db.Movies.Add(movie);
            db.SaveChanges();

            Console.WriteLine("Movie added successfully.");
        }

        static void UpdateMovie(MovieDbContext db)
        {
            Console.WriteLine("Enter the title of the movie to update:");
            string title = Console.ReadLine();

            var movie = db.Movies.FirstOrDefault(m => m.Title.ToUpper() == title.ToUpper());

            if (movie == null)
            {
                Console.WriteLine("Movie not found.");
                return;
            }

            Console.WriteLine($"Enter new title for {title}:");
            string newTitle = Console.ReadLine();

            Console.WriteLine($"Enter new genres for {title} (comma-separated):");
            string newGenres = Console.ReadLine();

            movie.Title = newTitle;
            movie.Genres = newGenres;

            db.SaveChanges();

            Console.WriteLine("Movie updated successfully.");
        }

        static void DeleteMovie(MovieDbContext db)
        {
            Console.WriteLine("Enter the title of the movie to delete:");
            string title = Console.ReadLine();

            var movie = db.Movies.FirstOrDefault(m => m.Title.ToUpper() == title.ToUpper());

            if (movie == null)
            {
                Console.WriteLine("Movie not found.");
                return;
            }

            db.Movies.Remove(movie);
            db.SaveChanges();

            Console.WriteLine("Movie deleted successfully.");
        }

        static void SearchMovies(MovieDbContext db)
        {
            Console.WriteLine("Enter search term:");
            string searchTerm = Console.ReadLine();

            var movieResults = db.Movies.Where(m => m.Title.Contains(searchTerm)).ToList();

            Console.WriteLine("Search Results:");
            foreach (var movie in movieResults)
            {
                movie.Display();
            }
        }
    }
}