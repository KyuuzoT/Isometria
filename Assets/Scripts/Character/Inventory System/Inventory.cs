using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Texture2D _inventoryImage;
    [SerializeField] private Rect _inventoryRect;
    [SerializeField][Range(0,1)] private float _percentage;

    private float XCoord;
    private float YCoord;
    // Use this for initialization
    void Start()
    {

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
