using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Helper
{
    public static class IdCreater
    {
        public static int CreateId()
        {
            int number = Convert.ToInt32(String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000));

            return number;
        }
    }
}
