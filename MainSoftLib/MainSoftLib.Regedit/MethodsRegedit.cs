using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSoftLib.Regedit
{
    public class MethodsRegedit
    {
        RegistryKey Main_RegistryKey = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="m_Registry"></param>
        /// <param name="Rute">Using \\ to Separe</param>
        public MethodsRegedit(RegistryKey m_Registry, string Rute)
        {
            Main_RegistryKey = m_Registry.OpenSubKey(Rute, true);

            if (Main_RegistryKey == null)
            {
                Main_RegistryKey = m_Registry;

                string[] vRute = Rute.Split('\\');

                if (vRute != null && vRute.Length > 0)
                {
                    for (int i = 0; i < vRute.Length; i++)
                    {
                        if (Main_RegistryKey.GetValue(vRute[i], null) == null)
                        {
                            Main_RegistryKey.CreateSubKey(vRute[i]);
                        }

                        Main_RegistryKey = Main_RegistryKey.OpenSubKey(vRute[i], true);
                    }
                }
            }
        }

        public void Create(string Key, string Data)
        {
            try { Main_RegistryKey.SetValue(Key, Data, RegistryValueKind.String); } catch (Exception) { }
        }

        public string Read(string Key)
        {
            try { return Main_RegistryKey.GetValue(Key).ToString(); } catch (Exception) { return null; };
        }
    }
}
