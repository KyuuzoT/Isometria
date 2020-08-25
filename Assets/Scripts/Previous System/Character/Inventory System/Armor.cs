using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Armor : Item
{
    public Armor(Texture2D texture, int width, int height)
    {
        Image = texture;
        Width = width;
        Height = height;
    }

    public override void PerformAction()
    {
        throw new NotImplementedException();
    }
}
