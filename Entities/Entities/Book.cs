using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entities
{
    public class Book : IEntity
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string AuthorName { get; set; }
        public string BookName { get; set; }
        public string BarcodeNo { get; set; }
        public string ISBN { get; set; }
    }
}
