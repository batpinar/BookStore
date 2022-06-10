using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        private readonly BookStoreDbContext _context;
        public int AuthorId { get; set; }
        public DeleteAuthorCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);
            var book   = _context.Books.SingleOrDefault(x => x.AuthorId == AuthorId);
            if(author is null)
                throw new InvalidOperationException("Silinecek yazar bulunamadı");
            else if(book is not null)
                throw new InvalidOperationException("Yazara ait kitap bulunmaktadır.");
            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}