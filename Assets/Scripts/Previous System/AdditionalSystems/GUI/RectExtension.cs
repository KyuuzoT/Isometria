using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.AdditionalSystems.GUI
{
    public static class RectExtension
    {
        public static Rect AdjustToScreen(this Rect rect)
        {
            rect.AdjustToScreenHeight();
            rect.AdjustToScreenWidth();
            return rect;
        }

        public static Rect AdjustToScreenWidth(this Rect rect)
        {
            rect.x = (Screen.width - rect.width) / 2;
            return rect;
        }

        public static Rect AdjustToScreenHeight(this Rect rect)
        {
            rect.y = (Screen.height - rect.height) / 2;
            return rect;
        }

        public static void CopyParameters(this Rect source, ref Rect destination)
        {
            source.CopyCoordinates(ref destination);
            source.CopySize(ref destination);
        }

        public static void CopyCoordinates(this Rect source, ref Rect destination)
        {
            source.CopyXCoordinate(ref destination);
            source.CopyYCoordinate(ref destination);
        }

        public static void CopyXCoordinate(this Rect source, ref Rect destination)
        {
            destination.x = source.x;
        }

        public static void CopyYCoordinate(this Rect source, ref Rect destination)
        {
            destination.y = source.y;
        }

        public static void CopySize(this Rect source, ref Rect destination)
        {
            source.CopyWidth(ref destination);
            source.CopyHeight(ref destination);
        }

        public static void CopyWidth(this Rect source, ref Rect destination)
        {
            destination.width = source.width;
        }

        public static void CopyHeight(this Rect source, ref Rect destination)
        {
            destination.height = source.height;
        }
    }
}
