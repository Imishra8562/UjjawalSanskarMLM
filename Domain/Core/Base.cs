using System;

namespace Domain
{
    public class Base
    {
        public Base()
        {
        }

        public DateTime? Created_On { get; set; }
        public int Created_By { get; set; }
        public string Created_IP { get; set; }
        public DateTime? Modified_On { get; set; }
        public int? Modified_By { get; set; }
        public string Modified_IP { get; set; }
        public bool Is_Active { get; set; }

    }
}
