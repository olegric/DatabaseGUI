using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sweetsWindowsForms.Additional
{
    class ImageByte
    {
        static public byte[] ImageToByte(System.Drawing.Image image)
        {
            System.IO.MemoryStream m = new System.IO.MemoryStream();

            image.Save(m, image.RawFormat);
            byte[] imageBytes = m.GetBuffer();

            return imageBytes;
        }
        static public System.Drawing.Image ByteToImage(byte[] imageBytes)
        {
            System.IO.MemoryStream m = new System.IO.MemoryStream(imageBytes);

            System.Drawing.Image image = System.Drawing.Image.FromStream(m);

            return image;
        }
    }
}
