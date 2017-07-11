using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MainSoftLib.DeviceInformation
{
    public class DeviceInformation
    {
        public static string GetCPUId()
        {
            string cpuInfo = String.Empty;
            string temp = String.Empty;
            ManagementClass mc = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {
                if (cpuInfo == String.Empty) // only return cpuInfo from first CPU 
                {
                    cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
                }
            }
            return cpuInfo;
        }

        public static string GetMotherBoardID()
        {
            ManagementObjectCollection mbCol = new ManagementClass("Win32_BaseBoard").GetInstances();
            //Enumerating the list 
            ManagementObjectCollection.ManagementObjectEnumerator mbEnum = mbCol.GetEnumerator();
            //Move the cursor to the first element of the list (and most probably the only one) 
            mbEnum.MoveNext();
            //Getting the serial number of that specific motherboard 
            return ((ManagementObject)(mbEnum.Current)).Properties["SerialNumber"].Value.ToString();
        }

        public static string GetMacAddress()
        {
            string macs = "";

            // get network interfaces physical addresses 
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface ni in interfaces)
            {
                PhysicalAddress pa = ni.GetPhysicalAddress();
                macs += pa.ToString();
            }
            return macs;
        }

        public static string GetVolumeSerial()
        {
            string strDriveLetter = "None";

            try
            {
                string drive = "C";
                ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"" + drive + ":\"");
                disk.Get();
                strDriveLetter = disk["VolumeSerialNumber"].ToString();
            }
            catch (Exception ex)
            {

            }
            return strDriveLetter;
        }

        public static string Cute(string Text, int Length)
        {
            try
            {
                if (Text.Length > Length)
                {
                    string Start = Text.Substring(0, Length / 2);
                    string End = Text.Substring(Text.Length - (Length / 2), Length / 2);

                    return Start + End;
                }
                else
                {
                    return Text;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string IntToLetters(int value)
        {
            string result = string.Empty;
            while (--value >= 0)
            {
                result = (char)('A' + value % 26) + result;
                value /= 26;
            }
            return result;
        }

        public static string GetUniqueID()
        {
            try
            {
                string PreCode = null;
                string Disco = null;
                string Bios = null;
                string Board = null;
                int Count = 0;

                try
                {
                    //ManagementObjectCollection.ManagementObjectEnumerator EnumDisco = null;
                    ManagementObjectCollection.ManagementObjectEnumerator EnumBios = null;
                    ManagementObjectCollection.ManagementObjectEnumerator EnumBoard = null;

                    while (Count < 3)
                    {
                        try
                        {
                            //EnumDisco = new ManagementClass("Win32_PhysicalMedia").GetInstances().GetEnumerator();
                            EnumBios = new ManagementClass("Win32_BIOS").GetInstances().GetEnumerator();
                            EnumBoard = new ManagementClass("Win32_BaseBoard").GetInstances().GetEnumerator();
                            break;
                        }
                        catch (Exception ex)
                        {

                        }

                        Count++;
                    }

                    // Serial Disco Duro   
                    Disco = "W-DCW1C2S2042681";

                    try
                    {
                        //Obtener serial de la Bios                       

                        if (EnumBios != null && EnumBios.MoveNext())
                        {
                            Bios = ((ManagementObject)(EnumBios.Current)).Properties["SerialNumber"].Value.ToString().Trim();
                        }
                    }
                    catch (Exception ex)
                    {

                    }

                    try
                    {
                        //Obtener serial de la Board

                        if (EnumBoard != null && EnumBoard.MoveNext())
                        {
                            Board = ((ManagementObject)(EnumBoard.Current)).Properties["SerialNumber"].Value.ToString().Trim();
                        }
                    }
                    catch (Exception ex)
                    {

                    }

                    // Generar Pre Code con con los diferentes Componentes

                    if (Disco != null && Bios != null && Board != null)
                    {
                        PreCode = Disco.Substring(Disco.Length - 7, 6) + Bios.Substring(Bios.Length - 6, 5) + Board.Substring(Board.Length - 6, 5);
                    }
                }
                catch (Exception ex)
                {

                }

                return PreCode;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string GetUniqueID(bool IncludeMacAddress, bool IncludeVolumeSerial, int Length = 0)
        {
            try
            {
                string ID;
                string UniqueID;
                string CPUId = GetCPUId();
                string MotherBoardID = GetMotherBoardID();
                string MacAddress = GetMacAddress();
                string VolumeSerial = GetVolumeSerial();

                ID = CPUId + MotherBoardID;

                if (IncludeMacAddress)
                    ID += MacAddress;

                if (IncludeVolumeSerial)
                    ID += VolumeSerial;

                // generate hash 
                HMACSHA1 hmac = new HMACSHA1();
                hmac.Key = Encoding.ASCII.GetBytes(MotherBoardID);
                hmac.ComputeHash(Encoding.ASCII.GetBytes(ID));

                // convert hash to hex string 
                UniqueID = "";

                for (int i = 0; i < hmac.Hash.Length; i++)
                {
                    UniqueID += hmac.Hash[i].ToString("X2");
                }

                //ID = ID.NumToLetter();
                UniqueID = Cute(UniqueID, Length);

                return UniqueID;
            }
            catch (Exception)
            {
                return "ERROR";
            }
        }
    }
}
