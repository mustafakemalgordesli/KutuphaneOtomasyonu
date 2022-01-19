using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entities
{
    public class Member : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string AdressDetails { get; set; }
    }
}

