using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int MinimalDamage;
    public int MaximalDamage;

    public float ReturnDamage()
    {
        float resultDamage = Random.Range((float)MinimalDamage, (float)MaximalDamage);
        return resultDamage;
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
