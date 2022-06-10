using AutoMapper;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.GetAuthorDetail;
using WebApi.Application.AuthorOperations.GetAuthors;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.GenreOperetions.Queries.Genres;
using WebApi.Application.GenreOperetions.Queries.GetGenreDetail;
using WebApi.Entities;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace WebApi.Common
{
    public class MapppingProfile : Profile
    {
        public MapppingProfile()
        {
            CreateMap<CreateBookModel, Book>();//.ForMember(dest => dest.GenreId, opt=> opt.MapFrom(src=> src.GenreId));
            CreateMap<Book, BookDetailViewModel>()    .ForMember(dest => dest.Genre, opt=> opt.MapFrom(src=> src.Genre.Name))
                                                      .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.FirstName + " " + src.Author.LastName))
                                                      .ForMember(dest => dest.PublishDate, opt=> opt.MapFrom(src => src.PublishDate.ToString("dd/MM/yyyy")));

            CreateMap<Book, BooksViewModel>()         .ForMember(dest => dest.Genre, opt=> opt.MapFrom(src=> src.Genre.Name))
                                                      .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.FirstName + " " + src.Author.LastName))
                                                      .ForMember(dest => dest.PublishDate, opt=> opt.MapFrom(src => src.PublishDate.ToString("dd/MM/yyyy")));

            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenresDetailsViewModel>();
            CreateMap<CreateAuthorModel, Author>();

            CreateMap<Author, AuthorViewModel>()      .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.Date.ToString("dd/MM/yyyy")))
                                                      .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));

            CreateMap<Author, AuthorDetailViewModel>().ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.Date.ToString("dd/MM/yyyy")))
                                                      .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));
            
        }

    }
}