using System;
using System.Collections.Generic;
using System.Text;

namespace Cakrawala.Models
{
    public class Membership
    {
        public string name { get; set; }
        public int minExp { get; set; }

        public Membership(string name, int minExp) { 
            this.name = name;
            this.minExp = minExp;
        }

        public Membership(Membership membership) { 
            this.name=membership.name;
            this.minExp=membership.minExp;
        }
    }
}
