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
    public class MemberManager : IMemberService
    {
        private readonly IMemberDal _memberDal;
        public MemberManager(IMemberDal memberDal)
        {
            _memberDal = memberDal;
        }

        public IResult Add(Member member)
        {
            try
            {
                List<Member> members = GetAll().Data;
                do
                {
                    int createdId = IdCreater.CreateId();
                    foreach (var _member in members)
                    {
                        if (_member.Email == member.Email)
                            return new ErrorResult(Messages.EmailError);

                        if (_member.Id == createdId)
                            continue;
                    }
                    member.Id = createdId;
                } while (false);
                _memberDal.Add(member);
                return new SuccessResult(Messages.UserAdded);
            }
            catch (Exception)
            {
                return new ErrorResult(Messages.UserNotAdded);
            }
        }

        public IResult Delete(int id)
        {
            List<Member> members = GetAll().Data;
            foreach (var _member in members)
            {
                if (_member.Id == id)
                {
                    try
                    {
                        _memberDal.Delete(id);
                        return new SuccessResult("Kullanıcı bilgileri silindi.");
                    }
                    catch (Exception)
                    {
                        return new ErrorResult(Messages.UserNotDeleted);
                    }
                }
            }
            return new ErrorResult(Messages.UserNotFound);
        }

        public IDataResult<List<Member>> GetAll()
        {
            try
            {
                return new SuccessDataResult<List<Member>>(_memberDal.GetAll());
            }
            catch (Exception)
            {
                return new ErrorDataResult<List<Member>>();
            }
        }

        public IDataResult<Member> GetById(int id)
        {
            List<Member> members = GetAll().Data;
            foreach (var member in members)
            {
                if (member.Id == id)
                    return new SuccessDataResult<Member>(member);
            }

            return new ErrorDataResult<Member>(Messages.UserNotFound);
        }

        public IResult Update(Member member)
        {
            List<Member> members = GetAll().Data;
            foreach (var _member in members)
            {
                if (_member.Id == member.Id)
                {
                    try
                    {
                        _memberDal.Update(member);
                        return new SuccessResult(Messages.BookUpdated);
                    }
                    catch (Exception)
                    {
                        return new ErrorResult(Messages.BookNotUpdated);
                    }
                }
            }
            return new ErrorResult(Messages.BookNotFound);
        }

        public IDataResult<Member> GetByEmail(string email)
        {
            List<Member> members = GetAll().Data;
            foreach (var member in members)
            {
                if (member.Email == email)
                    return new SuccessDataResult<Member>(member);
            }

            return new ErrorDataResult<Member>(Messages.UserNotFound);
        }
    }
}
