using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public UpdateAuthorModel Model;
        public int AuthorId { get; set; }
        private readonly BookStoreDbContext _context;
        public UpdateAuthorCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if(author is null)
                throw new InvalidOperationException("Güncellenecek yazar bulunamadı");

            if(_context.Authors.Any(x => x.FirstName.ToLower() == Model.FirstName.ToLower() && x.LastName.ToLower() == Model.LastName.ToLower() && x.Id != AuthorId ))
                throw new InvalidOperationException("Güncellemek istediğiniz yazar ismi zaten mevcut");

            author.FirstName = string.IsNullOrEmpty(Model.FirstName.Trim()) ? author.FirstName : Model.FirstName;
            author.LastName = string.IsNullOrEmpty(Model.LastName.Trim()) ? author.LastName : Model.LastName;
            author.BirthDate = Model.BirthDate == default ? author.BirthDate : Model.BirthDate;
            _context.SaveChanges();
            
        }
    }

    public class UpdateAuthorModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}