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
    private Vector2 _currentSelection;
    private Vector2 _nextSelection;

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
        DetectGUIAction();
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
                    Debug.Log($"Slot[{x},{y}] is occupied!");
                    return;
                }
            }
        }

        item.X = x;
        item.Y = y;
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

    void DetectGUIAction()
    {
        if(Input.mousePosition.x >= _inventoryRect.x && Input.mousePosition.x < _inventoryRect.x + _inventoryRect.width)
        {
            if (Screen.height - Input.mousePosition.y >= _inventoryRect.y && Screen.height - Input.mousePosition.y < _inventoryRect.y + _inventoryRect.height)
            {
                DetectMouseOnInventory();
                ClickToMove._inventoryOpened = true;
                return;
            }
        }

        ClickToMove._inventoryOpened = false;
    }

    void DetectMouseOnInventory()
    {
        Item tempItem = null;
        for (int i = 0; i < _slotRows; i++)
        {
            for (int j = 0; j < _slotCols; j++)
            {
                Rect slot = new Rect(_inventoryRect.x + slots[i,j].SlotPosition.x, _inventoryRect.y + slots[i, j].SlotPosition.y, _slotWidth, _slotHeight);
                if(slot.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y)))
                {
                    if (Event.current.isMouse)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            _currentSelection.x = i;
                            _currentSelection.y = j;
                            if(slots[i,j].InventoryItem != null)
                            {
                                tempItem = slots[i, j].InventoryItem;
                                slots[i, j].InventoryItem = null;
                            }
                        }
                        else if (Input.GetMouseButtonUp(0))
                        {
                            _nextSelection.x = i;
                            _nextSelection.y = j;

                            if(_nextSelection != _currentSelection && tempItem != null)
                            {
                                DragItem(tempItem);
                            }
                        }
                    }
                    return;
                }
            }
        }
    }

    private void DragItem(Item tempItem)
    {
        AddInventoryItem((int)_nextSelection.x, (int)_nextSelection.y, tempItem);
    }
}
