using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.GetAuthors
{
    public class GetAuthorQuery
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetAuthorQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<AuthorViewModel> Handle()
        {
            var autherList = _context.Authors.OrderBy(x => x.Id).ToList<Author>();
            List<AuthorViewModel> avm = _mapper.Map<List<AuthorViewModel>>(autherList);
            return avm;
        }
    }

    public class AuthorViewModel
    {
        public string FullName { get; set; }
        public string BirthDate { get; set; }
    }
}