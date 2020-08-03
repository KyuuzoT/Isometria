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
    }
}
