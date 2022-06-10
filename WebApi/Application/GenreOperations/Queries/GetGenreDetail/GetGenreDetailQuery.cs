using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperetions.Queries.GetGenreDetail
{
    public class GetGenreDetailQuery
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public int GenreId {get; set;}
        public GetGenreDetailQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public GenresDetailsViewModel Handle()
        {
            var genre = _context.Genres.SingleOrDefault(genre =>genre.IsActive && genre.Id == GenreId);
            if (genre is null)
                throw new InvalidOperationException("Kitap türü bulunamadı");
            var obj = _mapper.Map<GenresDetailsViewModel>(genre);
            return obj;
        }
    }

    public class GenresDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}