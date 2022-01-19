using Core.Utilities.Results;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Abstract
{
    public interface IBorrowService
    {
        IResult Add(Borrow borrow);
        IResult Update(Borrow borrow);
        IDataResult<List<Borrow>> GetAll();
        IDataResult<Borrow> GetById(int id);
        IDataResult<Borrow> GetByBookId(int id);
        IDataResult<List<Borrow>> GetAllNotReturnedBorrows();
    }
}
