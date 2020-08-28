using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    public int MinimalDamage;
    public int MaximalDamage;

    public float ReturnDamage()
    {
        float resultDamage = Random.Range((float)MinimalDamage, (float)MaximalDamage);
        return resultDamage;
    }
}
