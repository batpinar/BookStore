using AutoMapper;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;

namespace WebApi.Common
{
    public class MapppingProfile : Profile
    {
        public MapppingProfile()
        {
            CreateMap<CreateBookModel, Book>().ForMember(dest => dest.GenreId, opt=> opt.MapFrom(src=> ((GenreEnum)src.GenerId)));
            CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt=> opt.MapFrom(src=> ((GenreEnum)src.GenreId).ToString()));
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt=> opt.MapFrom(src=> ((GenreEnum)src.GenreId).ToString()));
        }

    }
}