using MainSoftLib.Image;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainSoftLib
{
    public partial class FrmImageCompare : Form
    {
        public FrmImageCompare()
        {
            InitializeComponent();
        }

        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            //DirectoryInfo dir = new DirectoryInfo(@"C:\DataCaptcha");
            //Bitmap image1 = new Bitmap(dir.FullName + "\\00169\\7244f574-9ad0-41c1-9dc2-689eedf0795a.bmp");
            //Bitmap image2 = new Bitmap(dir.FullName + "\\00169\\bc7d57cb-a657-4fc1-befa-cd3e5b65355c.bmp");

            //float Similarity = MethodImagenFilter.GetSimilarity(image1, image2);

            

           string Result = await GetResquest();
        }


        public static Task<string> GetResquest()
        {
            Task<string> task = Task.Run(() =>
            {
                string Resp = null;

                try
                {
                    DirectoryInfo dir = new DirectoryInfo(@"C:\DataCaptcha");

                    Bitmap image1 = new Bitmap(dir.FullName + "\\55499\\0f645d1d-7f21-45df-a2e8-adaf5e00159a.jpg");

                    Parallel.ForEach(dir.GetDirectories(), (currentDir, stateDir) =>
                    {
                        Parallel.ForEach(currentDir.GetFiles(), (currentFile, stateFile) =>
                        {
                            Bitmap image2 = new Bitmap(currentFile.FullName);

                            float Similarity = MethodImagenFilter.GetSimilarity(image1, image2);

                            if (Similarity > 0.95f)
                            {
                                Resp = currentFile.FullName;

                                stateDir.Break();
                                stateFile.Break();
                            }
                            else if (Similarity < 0.50f)
                            {
                                stateFile.Break();
                            }
                        });
                    });
                }
                catch (Exception ex)
                {
                    Resp = null;
                }

                return Resp;
            });
            return task;
        }

       
    }
}
