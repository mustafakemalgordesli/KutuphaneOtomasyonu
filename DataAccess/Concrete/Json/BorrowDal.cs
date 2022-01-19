using DataAccess.Abstract;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace DataAccess.Concrete.Json
{
    public class BorrowDal : JsonEntityRepositoryBase<Borrow> ,IBorrowDal
    {
        private BorrowDal()
        {
            _path = "./../../../../DataAccess/Data/borrows.json";
        }
        private static BorrowDal instance = null;
        public static BorrowDal Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BorrowDal();
                }
                return instance;
            }
        }

        public void Delete(int id)
        {
            List<Borrow> _data = new List<Borrow>();
            _data = GetAll();
            _data.RemoveAll(x => x.Id == id);

            string json = JsonSerializer.Serialize(_data);
            File.WriteAllText(_path, json);
        }

        public void Update(Borrow borrow)
        {
            List<Borrow> _data = GetAll();
            foreach (Borrow item in _data)
            {
                if (item.Id == borrow.Id)
                {
                    item.BorrowDate = borrow.BorrowDate;
                    item.ReturnDate = borrow.ReturnDate;
                }
            }
            string json = JsonSerializer.Serialize(_data);
            File.WriteAllText(_path, json);
        }
    }
}
