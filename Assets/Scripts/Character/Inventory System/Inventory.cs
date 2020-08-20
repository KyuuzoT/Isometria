using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Texture2D _inventoryImage;
    [SerializeField] private Rect _inventoryRect;
    [SerializeField] [Range(0, 1)] private float _percentage;

    [SerializeField] private float _slotWidth;
    [SerializeField] private float _slotHeight;
    [SerializeField] private float _slotXCoord;
    [SerializeField] private float _slotYCoord;
    [SerializeField] private int _slotRows;
    [SerializeField] private int _slotCols;

    private float XCoord;
    private float YCoord;

    internal List<Item> Items = new List<Item>();
    internal Slot[,] slots;
    // Use this for initialization
    void Start()
    {
        slots = new Slot[_slotRows, _slotCols];
        SetSlots();
    }

    private void SetSlots()
    {
        for (int i = 0; i < _slotRows; i++)
        {
            for (int j = 0; j < _slotCols; j++)
            {
                slots[i, j] = new Slot(new Rect(_slotXCoord + _slotWidth * j, _slotYCoord + _slotHeight*i, _slotWidth, _slotHeight));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGUI()
    {
        DrawInventory();
    }

    private void DrawInventory()
    {
        XCoord = Screen.width - _inventoryRect.width;
        YCoord = (1 - _percentage) * Screen.height - _inventoryRect.height;
        _inventoryRect = new Rect(XCoord, YCoord, _inventoryImage.width * 0.75f, _inventoryImage.height * 0.75f);
        GUI.DrawTexture(_inventoryRect, _inventoryImage);
    }
}
