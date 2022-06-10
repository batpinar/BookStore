using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public int AuthorId { get; set; }
        public GetAuthorDetailQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public AuthorDetailViewModel Handle()
        {
            var author = _context.Authors.Where(x => x.Id == AuthorId).SingleOrDefault();
            if(author is null)
                throw new InvalidOperationException("Yazar Bulununamadı");
            AuthorDetailViewModel avm = _mapper.Map<AuthorDetailViewModel>(author);
            return avm;
        }
    }

    public class AuthorDetailViewModel
    {
        public string FullName { get; set; }
        public string BirthDate { get; set; }
    }
}