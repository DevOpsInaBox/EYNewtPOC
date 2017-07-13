using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Automation_Portal
{
    public class portalBean
    {
        public string Res_grp_name { get; set; }
        public string Vnet_name { get; set; }
        public string Subnet_name { get; set; }
        public string Sec_grp_name { get; set; }
        public string Nic_name { get; set; }
        public string Vm_name { get; set; }
        public string Publicipname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Confpassword { get; set; }
        public string Os { get; set; }
        public string Location { get; set; }
        public string Diskname { get; set; }

        override
        public string ToString()
        {
            return "Res_grp_name:"+ Res_grp_name+ " Vnet_name:"+ Vnet_name+ " Subnet_name:"+ Subnet_name+ " Sec_grp_name:"+ Sec_grp_name
                + "\nNic_name:"+ Nic_name+ " Vm_name:"+ Vm_name+" Os:"+Os+" Location:"+ Location;
        }
    }
}