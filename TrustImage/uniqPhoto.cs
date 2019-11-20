using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace TrustImage
{
    class uniqPhoto
    {
        public void RotatePhoto(string path, float angel)
        {
            Bitmap b1 = new Bitmap(path);
            Bitmap img = RotateImage(b1, angel);
            b1.Dispose();
            System.IO.File.Delete(path);
            img.Save(path, ImageFormat.Jpeg);
            img.Dispose();
        }

        private Bitmap RotateImage(Bitmap b, float angle)
        {

            var sinVal = Abs(Sin(angle));
            var cosVal = Abs(Cos(angle));
            int newImgWidth = Convert.ToInt32(sinVal * b.Height + cosVal * b.Width);
            int newImgHeight = Convert.ToInt32(sinVal * b.Width + cosVal * b.Height);

            Bitmap returnBitmap = new Bitmap(newImgWidth, newImgHeight);
            returnBitmap.SetResolution(b.HorizontalResolution, b.VerticalResolution);
            Graphics g = Graphics.FromImage(returnBitmap);
            g.Clear(Color.White);
            g.TranslateTransform((float)b.Width / 2, (float)b.Height / 2);
            g.RotateTransform(angle);
            g.TranslateTransform(-(float)b.Width / 2, -(float)b.Height / 2);
            g.DrawImage(b, new Point(0, 0));
            return returnBitmap;
        }

        public void CropImage(string imagePath, int randomPix)
        {
            Bitmap Image = new Bitmap(imagePath);
            int x = Image.Height - (Image.Height - randomPix);
            int y = Image.Width - (Image.Width - randomPix);
            int height = Image.Height - randomPix;
            int width = Image.Width - randomPix;
            Image.Dispose();

            Bitmap croppedImage;

            using (var originalImage = new Bitmap(imagePath))
            {
                Rectangle crop = new Rectangle(x, y, width, height);
                croppedImage = originalImage.Clone(crop, originalImage.PixelFormat);
            }
            croppedImage.Save(imagePath, ImageFormat.Jpeg);
            croppedImage.Dispose();
        }

        public void AlphaImage()
        {
            var image = Image.FromFile(@"C:\Users\user\Desktop\wt.png");
            var bitmap = new Bitmap(image.Width, image.Height);
            var canvas = Graphics.FromImage(bitmap);
            var brush = new SolidBrush(Color.Transparent);
            canvas.FillRectangle(brush, 0, 0, image.Width, image.Height);
            ColorMatrix cm = new ColorMatrix();
            cm.Matrix33 = 0.001f;
            ImageAttributes ia = new ImageAttributes();
            ia.SetColorMatrix(cm);
            canvas.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, ia);
            canvas.Save();
            bitmap.Save(@"C:\Users\user\Desktop\wt-1.png");
        }

        public void SetWatermark(string imagePath, string watermarkImagePath)
        {
            using (Image image = Image.FromFile(imagePath))
            using (Image watermarkImage = Image.FromFile(watermarkImagePath)) 

            using (Graphics imageGraphics = Graphics.FromImage(image))
            using (Brush watermarkBrush = new TextureBrush(watermarkImage))
            {
                int x = (image.Width - watermarkImage.Width) / 2;
                int y = (image.Height - watermarkImage.Height) / 2;
                imageGraphics.FillRectangle(watermarkBrush, new Rectangle(new Point(x, y), watermarkImage.Size));
                image.Save(@"C:\Users\user\Desktop\2.jpg");
            }
        }
    }
}
