using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IBorrowDal : IEntityRepository<Borrow>
    {
        void Update(Borrow borrow);
        void Delete(int id);
    }
}
