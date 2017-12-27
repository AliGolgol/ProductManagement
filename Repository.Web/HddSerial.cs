using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Web
{
    public class HddSerial
    {

        public string Hdd()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
            int i = 0;
            string hdd = "";
            foreach (ManagementObject wmi_HD in searcher.Get())
            {

                // get the hardware serial no.
                if (wmi_HD["SerialNumber"] == null)
                    hdd = "None";
                else
                    hdd = wmi_HD["SerialNumber"].ToString();

                ++i;
            }
            return hdd;
        }
    }
}
