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
    public class BorrowManager : IBorrowService
    {
        private readonly IBorrowDal _borrowDal;

        public BorrowManager(IBorrowDal borrowDal)
        {
            _borrowDal = borrowDal;
        }

        public IResult Add(Borrow borrow)
        {
            try
            {
                List<Borrow> borrows = GetAll().Data;
                do
                {
                    int createdId = IdCreater.CreateId();
                    foreach (var _borrow in borrows)
                    {
                        if (_borrow.Id == createdId)
                            continue;
                    }
                    borrow.Id = createdId;
                } while (false);
                _borrowDal.Add(borrow);
                return new SuccessResult(Messages.BorrowAdded);
            }
            catch (Exception)
            {
                return new ErrorResult(Messages.BorrowNotAdded);
            }
        }

        public IDataResult<List<Borrow>> GetAll()
        {
            try
            {
                return new SuccessDataResult<List<Borrow>>(_borrowDal.GetAll());
            }
            catch (Exception)
            {
                return new ErrorDataResult<List<Borrow>>();
            }
        }

        public IDataResult<Borrow> GetById(int id)
        {
            List<Borrow> borrows = GetAll().Data;
            foreach (var borrow in borrows)
            {
                if (borrow.Id == id)
                    return new SuccessDataResult<Borrow>(borrow);
            }

            return new ErrorDataResult<Borrow>(Messages.BorrowNotFound);
        }

        public IResult Update(Borrow borrow)
        {
            List<Borrow> borrows = GetAll().Data;
            foreach (var _borrow in borrows)
            {
                if (_borrow.Id == borrow.Id)
                {
                    try
                    {
                        _borrowDal.Update(borrow);
                        return new SuccessResult(Messages.BorrowUpdated);
                    }
                    catch (Exception)
                    {
                        return new ErrorResult(Messages.BorrowNotUpdated);
                    }
                }
            }
            return new ErrorResult(Messages.BorrowNotFound);
        }

        public IDataResult<Borrow> GetByBookId(int id)
        {
            List<Borrow> borrows = GetAll().Data;

            foreach (var borrow in borrows)
            {
                if(borrow.BookId == id && borrow.Status == true)
                {
                    return new SuccessDataResult<Borrow>(borrow);
                }
            }
            return new ErrorDataResult<Borrow>();
        }

        public IDataResult<List<Borrow>> GetAllNotReturnedBorrows()
        {
            List<Borrow> borrows = GetAll().Data;

            List<Borrow> _notReturnedBorrow = new List<Borrow>();
            
            foreach (var borrow in borrows)
            {
                if (borrow.Status == true)
                {
                    _notReturnedBorrow.Add(borrow);
                }
            }

            return new SuccessDataResult<List<Borrow>>(_notReturnedBorrow);
        }
    }
}
