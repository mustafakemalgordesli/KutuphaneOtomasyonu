using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IBookDal : IEntityRepository<Book>
    {
        void Delete(int id);
        void Update(Book book);
    }
}
