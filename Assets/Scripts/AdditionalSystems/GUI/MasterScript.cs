using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterScript : MonoBehaviour
{
    private void Awake()
    {
        gameObject.GetComponent<Inventory>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.I))
        {
            gameObject.GetComponent<Inventory>().enabled = !gameObject.GetComponent<Inventory>().enabled;
        }
    }
}
