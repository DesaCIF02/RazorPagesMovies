using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovies.Models;

namespace RazorPagesMovies.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly Data.RazorPagesMoviesContext _context;

        public IndexModel(Data.RazorPagesMoviesContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie;
        public SelectList Genres;

        public string sMovieGenre { get; set; }
        public decimal nMoviePrice { get; set; }
        public DateTime dReleaseDate { get; set; }
        public async Task OnGetAsync(string sMovieGenre, string sMovieTitle, decimal nMoviePrice, DateTime dMovieReleaseDate)
        {
            IQueryable<string> sGenreQuery = from dtMovie in _context.Movie
                                             orderby dtMovie.Genre
                                             select dtMovie.Genre;
            IQueryable<decimal> nPriceQuery = from dtMovie in _context.Movie
                                              orderby dtMovie.Price
                                              select dtMovie.Price;
            IQueryable<DateTime> dReleaseDateQuery = from dtMovie in _context.Movie
                                                     orderby dtMovie.ReleaseDate
                                                     select dtMovie.ReleaseDate;

            var movies = from dtMovie in _context.Movie
                         select dtMovie;
            if (!String.IsNullOrEmpty(sMovieTitle))
            {
                movies = movies.Where(movie => movie.Title.Contains(sMovieTitle));
            }
            if (!String.IsNullOrEmpty(sMovieGenre))
            {
                movies = movies.Where(movie => movie.Genre == sMovieGenre);
            }
            if (nMoviePrice > 0)
            {
                movies = movies.Where(movie => movie.Price <= nMoviePrice);
            }
            if (dMovieReleaseDate.Year > 1900)
            {
                movies = movies.Where(movie => movie.ReleaseDate <= dMovieReleaseDate);
            }
            Genres = new SelectList(await sGenreQuery.Distinct().ToListAsync());
            //Movie = await _context.Movie.ToListAsync();
            Movie = await movies.ToListAsync();
        }
    }
}
