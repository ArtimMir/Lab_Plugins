using System;
using System.Drawing;
using PluginInterface;

namespace TestPlugin
{
    [Version(2, 1)]
    
    public class ReverseTransform : IPlugin
    {
        string ver;
        public string Name
        {
            get
            {
                return "Повышение контрастности";
            }
        }

        public string Author
        {
            get
            {
                return "Разработчик 1";
            }
        }
        public string Version
        {

            get
            {
                
                Type type = typeof(ReverseTransform);
                object[] versionAttributes = type.GetCustomAttributes(false);
                foreach (VersionAttribute attribute in versionAttributes)
                {
                    ver = "(" + attribute.Major.ToString() + "," + attribute.Minor.ToString() + ")";
                }
                return ver;
            }
        }

        public void Transform(Bitmap bitmap)
        {
            double contrast = 50;
            if (contrast < -100) contrast = -100;
            if (contrast > 100) contrast = 100;
            contrast = (100.0 + contrast) / 100.0;
            contrast *= contrast;
            Color c;
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    c = bitmap.GetPixel(i, j);
                    double pR = c.R / 255.0;
                    pR -= 0.5;
                    pR *= contrast;
                    pR += 0.5;
                    pR *= 255;
                    if (pR < 0) pR = 0;
                    if (pR > 255) pR = 255;

                    double pG = c.G / 255.0;
                    pG -= 0.5;
                    pG *= contrast;
                    pG += 0.5;
                    pG *= 255;
                    if (pG < 0) pG = 0;
                    if (pG > 255) pG = 255;

                    double pB = c.B / 255.0;
                    pB -= 0.5;
                    pB *= contrast;
                    pB += 0.5;
                    pB *= 255;
                    if (pB < 0) pB = 0;
                    if (pB > 255) pB = 255;

                    bitmap.SetPixel(i, j, Color.FromArgb((byte)pR, (byte)pG, (byte)pB));
                }
            }
            
        }
    }

}
