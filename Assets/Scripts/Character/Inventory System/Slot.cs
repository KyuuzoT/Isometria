using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot
{
    [SerializeField] private Item InventoryItem;
    [SerializeField] private Rect SlotPosition;

    public static Texture2D ItemInSlot { get; set; }
    internal bool SlotOccupied;

    public Slot()
    {
    }

    public Slot(Rect position)
    {
        this.SlotPosition = position;
    }

    internal void DrawSlot(float frameX, float frameY)
    {
        if (InventoryItem != null)
        {
            GUI.DrawTexture(new Rect(frameX + SlotPosition.x, frameY + SlotPosition.y, SlotPosition.width, SlotPosition.height), InventoryItem.Image);
        }
    }
}
