using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient
{
    public class OrderInfo
    {
        public int OrderInfo_ID { get; set; }
        public int OrderInfo_Transport { get; set; }
        public string OrderInfo_TransportMark { get; set; }
        public string OrderInfo_TransportModel { get; set; }
        public string OrderInfo_Number { get; set; }
        public int OrderInfo_Creator { get; set; }
        public string OrderInfo_CreationDate { get; set; }
        public string OrderInfo_EndingDate { get; set; }
        public double OrderInfo_Sum { get; set; }
        public bool OrderInfo_IsConfirmed { get; set; }
        public string OrderInfo_Notification { get; set; }
    }
}
