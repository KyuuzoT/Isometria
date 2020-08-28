using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    public int MinimalDamage;
    public int MaximalDamage;

    public int ReturnDamage()
    {
        return Random.Range(MinimalDamage, MaximalDamage);
    }
}
