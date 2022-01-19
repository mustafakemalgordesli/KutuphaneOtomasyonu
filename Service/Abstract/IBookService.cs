using Core.Utilities.Results;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Abstract
{
    public interface IBookService
    {
        IResult Add(Book book);
        IResult Update(Book book);
        IResult Delete(int id);
        IDataResult<List<Book>> GetAll();
        IDataResult<Book> GetById(int id);
        IDataResult<Book> GetByISBN(string ISBN);
        IDataResult<Book> GetByBarcodeNo(string BarcodeNo);
    }
}
