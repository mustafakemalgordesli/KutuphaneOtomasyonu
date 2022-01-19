using Core.Utilities.Results;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Abstract
{
    public interface IMemberService
    {
        IResult Add(Member member);
        IResult Update(Member member);
        IResult Delete(int id);
        IDataResult<List<Member>> GetAll();
        IDataResult<Member> GetById(int id);
        IDataResult<Member> GetByEmail(string email);
    }
}
