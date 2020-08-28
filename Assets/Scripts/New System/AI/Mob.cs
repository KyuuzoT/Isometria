using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    internal bool IsMobHighlited;
    private Damage Damage;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetHit(float damage)
    {

    }

    private void MakeHit()
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
