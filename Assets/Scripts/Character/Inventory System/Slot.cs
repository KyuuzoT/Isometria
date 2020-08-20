using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public Item InventoryItem;
    public bool SlotOccupied;
    public Rect SlotPosition;

    public static Texture2D ItemInSlot { get; set; }

    public Slot()
    {

    }

    public Slot(Rect position)
    {

    }

    void DrawSlot(int frameX, int frameY)
    {
        GUI.DrawTexture(new Rect(frameX + SlotPosition.x, frameY + SlotPosition.y, SlotPosition.width, SlotPosition.height), InventoryItem.Image);
    }
}
