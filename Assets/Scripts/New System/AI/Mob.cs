using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    internal bool IsMobHighlited;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseOver()
    {
        IsMobHighlited = true;
    }

    private void OnMouseExit()
    {
        IsMobHighlited = false;
    }
}
