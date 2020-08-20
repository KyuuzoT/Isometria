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
    [SerializeField] private int _slotRows = 3;
    [SerializeField] private int _slotCols = 9;

    private float XCoord;
    private float YCoord;

    internal List<Item> Items = new List<Item>();
    internal Slot[,] slots;
    

    // Use this for initialization
    void Start()
    {
        slots = new Slot[_slotRows, _slotCols];
        SetSlots();
        AddInventoryItem(1, 1, ItemsList.GetArmor(0));
        AddInventoryItem(2, 2, ItemsList.GetArmor(1));
    }

    private void SetSlots()
    {
        for (int i = 0; i < _slotRows; i++)
        {
            for (int j = 0; j < _slotCols; j++)
            {
                slots[i, j] = new Slot(new Rect(_slotXCoord + _slotWidth * j, _slotYCoord + _slotHeight * i, _slotWidth, _slotHeight));
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
        DrawInventorySlots();
        DrawItems();
    }

    void DrawInventorySlots()
    {
        for (int i = 0; i < _slotRows; i++)
        {
            for (int j = 0; j < _slotCols; j++)
            {
                slots[i, j].DrawSlot(_inventoryRect.x, _inventoryRect.y);
            }
        }
    }

    private void DrawInventory()
    {
        XCoord = Screen.width - _inventoryRect.width;
        YCoord = (1 - _percentage) * Screen.height - _inventoryRect.height;
        _inventoryRect = new Rect(XCoord, YCoord, _inventoryImage.width, _inventoryImage.height);
        GUI.DrawTexture(_inventoryRect, _inventoryImage);
    }

    public void AddInventoryItem(int x, int y, Item item)
    {
        for (int i = 0; i < item.Width; i++)
        {
            for (int j = 0; j < item.Height; j++)
            {
                if(slots[x,y].SlotOccupied)
                {
                    return;
                }
            }
        }

        Items.Add(item);
    }

    void DrawItems()
    {
        foreach (var item in Items)
        {
            GUI.DrawTexture(
                new Rect
                (
                    _slotXCoord + _inventoryRect.x + item.X*_slotWidth, 
                    _slotYCoord + _inventoryRect.y + item.Y*_slotHeight, 
                    item.Width, 
                    item.Height
                ),
                item.Image);
        }
    }
}
