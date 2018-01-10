using System;
using System.Globalization;
using System.Windows.Media;

namespace GraphBuilder.Core
{
    public static class ColorExt
    {
        /// <summary>
        /// Usage: Color color = 0xFFDFD991.ToColor1();
        /// </summary>
        public static Color ToColor1(this uint argb)
        {
            return Color.FromArgb((byte) ((argb & 0xff000000) >> 0x18), (byte) ((argb & 0xff0000) >> 0x10),
                (byte) ((argb & 0xff00) >> 0x08), (byte) (argb & 0xff));
        }

        /// <summary>
        /// Usage: Color color = 0xFFDFD991.ToColor2();
        /// </summary>
        public static Color ToColor2(this uint argb)
        {
            return Color.FromArgb((byte)((argb & -16777216) >> 0x18),
                (byte)((argb & 0xff0000) >> 0x10),
                (byte)((argb & 0xff00) >> 8),
                (byte)(argb & 0xff));
        }

        /// <summary>
        /// Usage: Color c1 = ColorExt.ToColorFromHex("#00B2D6E8");
        /// Accepts 3 standard formats for colors: 
        /// 1. [ALPHA][RED][GREEN][BLUE] (alpha and all colors)
        /// 2. [RED][GREEN][BLUE] (no alpha channel, automatically set to 255)
        /// 3. Short hand [R][G][B] (takes the single digit hex number and adds it to the same number * 16, alpha set to 255);
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static Color ToColorFromHex(string hex)
        {
            if (string.IsNullOrEmpty(hex))
            {
                throw new ArgumentNullException("hex");
            }
            
            while (hex.StartsWith("#"))
            {
                hex = hex.Substring(1);
            }

            int num = 0;
            if (!Int32.TryParse(hex, NumberStyles.HexNumber, null, out num))
            {
                throw new ArgumentException("GraphBuilder.Core.ColorExt: Color Hex format is not recognized");
            }

            int[] pieces = new int[4];
            if (hex.Length > 7)
            {
                pieces[0] = ((num >> 24) & 0x000000ff);
                pieces[1] = ((num >> 16) & 0x000000ff);
                pieces[2] = ((num >> 8) & 0x000000ff);
                pieces[3] = (num & 0x000000ff);
            }
            else if (hex.Length > 5)
            {
                pieces[0] = 255;
                pieces[1] = ((num >> 16) & 0x000000ff);
                pieces[2] = ((num >> 8) & 0x000000ff);
                pieces[3] = (num & 0x000000ff);
            }
            else if (hex.Length == 3)
            {
                pieces[0] = 255;
                pieces[1] = ((num >> 8) & 0x0000000f);
                pieces[1] += pieces[1] * 16;
                pieces[2] = ((num >> 4) & 0x000000f);
                pieces[2] += pieces[2] * 16;
                pieces[3] = (num & 0x000000f);
                pieces[3] += pieces[3] * 16;
            }
            return Color.FromArgb((byte)pieces[0], (byte)pieces[1], (byte)pieces[2], (byte)pieces[3]);
        }
    }
}