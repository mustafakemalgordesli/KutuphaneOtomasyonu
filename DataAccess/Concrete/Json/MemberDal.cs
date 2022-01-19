using DataAccess.Abstract;
using DataAccess.Concrete.Json;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class MemberDal : JsonEntityRepositoryBase<Member>, IMemberDal
    {
        private MemberDal() 
        {
            _path = "./../../../../DataAccess/Data/members.json";
        }
        private static MemberDal instance = null;
        public static MemberDal Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MemberDal();
                }
                return instance;
            }
        }


        public void Delete(int id)
        {
            List<Member> _data = new List<Member>();
            _data = GetAll();
            _data.RemoveAll(x => x.Id == id);

            string json = JsonSerializer.Serialize(_data);
            File.WriteAllText(_path, json);
        }

        public void Update(Member member)
        {
            List<Member> _data = GetAll();
            foreach (Member item in _data)
            {
                if (item.Id == member.Id)
                {
                    item.FirstName = member.FirstName;
                    item.LastName = member.LastName;
                    item.PhoneNumber = member.PhoneNumber;
                    item.AdressDetails = member.AdressDetails;
                    item.Email = member.Email;
                }
            }
            string json = JsonSerializer.Serialize(_data);
            File.WriteAllText(_path, json);
        }
    }
}
