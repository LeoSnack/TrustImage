using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrustImage
{
    public partial class Form1 : Form
    {
        selectPhoto selectPhoto = new selectPhoto();
        uniqPhoto uniqPhoto = new uniqPhoto();

        public Form1()
        {
            InitializeComponent();
        }

        private void uniqPic(string path)
        {
            uniqPhoto.CropImage(path, 15);
            uniqPhoto.RotatePhoto(path,-0.02f);
            uniqPhoto.AlphaImage();
            uniqPhoto.SetWatermark(@"C:\Users\user\Desktop\1.jpg", @"C:\Users\user\Desktop\wt.png");
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            string root = @"C:\Users\user\Desktop\main";
            string[] subDir = Directory.GetDirectories(root);

            foreach(string dirPath in subDir)
            {
                string[] filePath = Directory.GetFiles(dirPath);
                int countTrueFile = 0;

                for (int i = 0; i < filePath.Count(); i++)
                {   
                    if(countTrueFile == 30)
                    {
                        break;
                        //File.Delete(@filePath[i]);
                    }
                    else
                    {
                        bool res = await selectPhoto.MakeAnalysisRequest(@filePath[i]);

                        if (!res)
                        {
                            File.Delete(@filePath[i]);
                        }
                        else
                        {
                            countTrueFile++;
                            string newPath = dirPath + "/" + countTrueFile + ".jpg";
                            File.Move(filePath[i], newPath);

                        }
                    }
                }
            }


            //cropImage
            //CropImage(imagePath, 30);
        }
    }
}
