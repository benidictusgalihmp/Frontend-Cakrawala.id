using System;
using System.Collections.Generic;
using System.Text;

namespace Cakrawala.Models
{
    public class Voucher
    {
        public int voucherId { get; set; }
        public float discount { get; set; }

        public Voucher(int voucherId, float discount) { 
            this.voucherId = voucherId;
            this.discount = discount;   
        }

        public Voucher(Voucher v)
        {
            this.voucherId=v.voucherId;
            this.discount=v.discount;
        }
    }
}
