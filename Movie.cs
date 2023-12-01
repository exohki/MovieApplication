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
    public class Movie
    {
        
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Genres { get; set; }

        public void Display()
        {
            Console.WriteLine($"Movie ID: {MovieId}, Title: {Title}, Genres: {Genres}");
        }
    }
}