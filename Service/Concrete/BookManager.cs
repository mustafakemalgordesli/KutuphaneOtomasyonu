using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Entities;
using Service.Abstract;
using Service.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Concrete
{
    public class BookManager : IBookService
    {
        private readonly IBookDal _bookDal;

        public BookManager(IBookDal bookDal)
        {
            _bookDal = bookDal;
        }

        public IResult Add(Book book)
        {
            try
            {
                List<Book> books = GetAll().Data;
                do
                {
                    int createdId = IdCreater.CreateId();
                    foreach (var _book in books)
                    {
                        if (book.Id == createdId)
                            continue;
                    }
                    book.Id = createdId;
                } while (false);
                _bookDal.Add(book);
                return new SuccessResult(Messages.BookAdded);
            }
            catch (Exception)
            {
                return new ErrorResult(Messages.BookNotAdded);
            }
        }

        public IResult Delete(int id)
        {
            List<Book> books = GetAll().Data;
            foreach (var book in books)
            {
                if (book.Id == id)
                {
                    try
                    {
                        _bookDal.Delete(id);
                        return new SuccessResult("Kitap bilgileri silindi.");
                    }
                    catch (Exception)
                    {
                        return new ErrorResult(Messages.BookNotDeleted);
                    }
                }
            }
            return new ErrorResult(Messages.BookNotFound);
        }

        public IDataResult<List<Book>> GetAll()
        {
            try
            {
                return new SuccessDataResult<List<Book>>(_bookDal.GetAll());
            }
            catch (Exception)
            {
                return new ErrorDataResult<List<Book>>();
            }
        }

        public IDataResult<Book> GetByBarcodeNo(string BarcodeNo)
        {
            List<Book> books = GetAll().Data;
            foreach (var book in books)
            {
                if (book.BarcodeNo == BarcodeNo)
                    return new SuccessDataResult<Book>(book);
            }

            return new ErrorDataResult<Book>(Messages.BookNotFound);
        }

        public IDataResult<Book> GetById(int id)
        {
            List<Book> books = GetAll().Data;
            foreach (var book in books)
            {
                if (book.Id == id)
                    return new SuccessDataResult<Book>(book);
            }

            return new ErrorDataResult<Book>(Messages.BookNotFound);
        }

        public IDataResult<Book> GetByISBN(string ISBN)
        {
            List<Book> books = GetAll().Data;
            foreach (var book in books)
            {
                if (book.ISBN == ISBN)
                    return new SuccessDataResult<Book>(book);
            }

            return new ErrorDataResult<Book>(Messages.BookNotFound);
        }

        public IResult Update(Book book)
        {
            List<Book> books = GetAll().Data;
            foreach (var _book in books)
            {
                if (_book.Id == book.Id)
                {
                    try
                    {
                        _bookDal.Update(book);
                        return new SuccessResult(Messages.UserUpdated);
                    }
                    catch (Exception)
                    {
                        return new ErrorResult(Messages.UserNotUpdated);
                    }
                }
            }
            return new ErrorResult(Messages.UserNotFound);
        }
    }
}
