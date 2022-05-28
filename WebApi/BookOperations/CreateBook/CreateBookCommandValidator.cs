using System;
using FluentValidation;

namespace WebApi.BookOperations.CreateBook
{
   public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
   {
       public CreateBookCommandValidator()
       {
           RuleFor(command => command.Model.GenerId).GreaterThan(0);
           RuleFor(command => command.Model.PageCount).GreaterThan(100);
           RuleFor(command => command.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
           RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);
       }

   }
}