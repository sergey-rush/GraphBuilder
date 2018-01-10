using System;
using System.Collections.Generic;
using System.Windows.Media;
using GraphBuilder.Core;

namespace GraphBuilder.Shell.Models
{
    public class AppColor
    {
        /// <summary>
        /// AppColor
        /// </summary>
        /// <param name="shapeColor">ShapeColor</param>
        /// <param name="borderColor">BorderColor</param>
        public AppColor(Color shapeColor, Color borderColor)
        {
            ShapeColor = shapeColor;
            BorderColor = borderColor;
        }
        public Color ShapeColor { get; set; }
        public Color BorderColor { get; set; }
    }
    
    public class AppColorList : List<AppColor>
    {
        public AppColorList()
        {
            Add(new AppColor(ColorExt.ToColorFromHex("#5BF5C2"), ColorExt.ToColorFromHex("#2D7A61")));// light-green
            Add(new AppColor(ColorExt.ToColorFromHex("#F55B5B"), ColorExt.ToColorFromHex("#7A2D2D")));// red-dark
            Add(new AppColor(ColorExt.ToColorFromHex("#C25BF5"), ColorExt.ToColorFromHex("#612D7A")));// violet-blue
            Add(new AppColor(ColorExt.ToColorFromHex("#C2F55B"), ColorExt.ToColorFromHex("#617A2D")));// yell-green
            Add(new AppColor(ColorExt.ToColorFromHex("#5B5BF5"), ColorExt.ToColorFromHex("#2D2D7A")));// dark-blue
            Add(new AppColor(ColorExt.ToColorFromHex("#5BC2F5"), ColorExt.ToColorFromHex("#2D617A")));// light-blue
            Add(new AppColor(ColorExt.ToColorFromHex("#5BF55B"), ColorExt.ToColorFromHex("#2D7A2D")));// green-light
        }
    }
}