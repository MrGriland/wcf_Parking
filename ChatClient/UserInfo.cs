using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient
{
    public class UserInfo
    {
        public int UserInfo_ID { get; set; }
        public string UserInfo_Login { get; set; }
        public string UserInfo_Name { get; set; }
        public string UserInfo_Surname { get; set; }
        public string UserInfo_IsAdmin { get; set; }
        public int UserInfo_Count { get; set; }
    }
}
