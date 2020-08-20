using Assets.Scripts.AdditionalSystems.GUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBar : MonoBehaviour
{
    [SerializeField] private Texture2D _actionBarImage;
    [SerializeField] private Rect _actionBarPosition;

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
        DrawActionBar();
    }

    private void DrawActionBar()
    {
        GUI.DrawTexture(new Rect(Screen.width * _actionBarPosition.x, Screen.height * _actionBarPosition.y, Screen.width * _actionBarPosition.width, Screen.height * _actionBarPosition.height), _actionBarImage);
    }
}
