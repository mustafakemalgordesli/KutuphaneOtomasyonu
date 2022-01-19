using DataAccess.Abstract;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace DataAccess.Concrete.Json
{
    public class BookDal : JsonEntityRepositoryBase<Book>, IBookDal
    {
        private BookDal()
        {
            _path = "./../../../../DataAccess/Data/books.json";
        }
        private static BookDal instance = null;
        public static BookDal Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BookDal();
                }
                return instance;
            }
        }

        public void Delete(int id)
        {
            List<Book> _data = new List<Book>();
            _data = GetAll();
            _data.RemoveAll(x => x.Id == id);

            string json = JsonSerializer.Serialize(_data);
            File.WriteAllText(_path, json);
        }

        public void Update(Book book)
        {
            List<Book> _data = GetAll();
            foreach (Book item in _data)
            {
                if (item.Id == book.Id)
                {
                    item.BarcodeNo = book.BarcodeNo;
                    item.AuthorName = book.AuthorName;
                    item.BookName = book.BookName;
                    item.CategoryName = book.CategoryName;
                    item.ISBN = item.ISBN;
                }
            }
            string json = JsonSerializer.Serialize(_data);
            File.WriteAllText(_path, json);
        }
    }
}
