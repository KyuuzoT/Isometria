
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Item : ScriptableObject
{
    [SerializeField] private Texture2D _image;
    [SerializeField] private int _x;
    [SerializeField] private int _y;
    [SerializeField] private int _width;
    [SerializeField] private int _height;

    public Texture2D Image
    {
        get
        {
            return _image;
        }
        set
        {
            _image = value;
        }
    }

    public int X
    {
        get
        {
            return _x;
        }
        set
        {
            _x = value;
        }
    }

    public int Y
    {
        get
        {
            return _y;
        }
        set
        {
            _y = value;
        }
    }

    public int Width
    {
        get
        {
            return _width;
        }
        set
        {
            _width = value;
        }
    }

    public int Height
    {
        get
        {
            return _height;
        }
        set
        {
            _height = value;
        }
    }

    public abstract void PerformAction();
}
