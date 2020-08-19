using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
    public bool StunAttackFlag { get; set; }
    [SerializeField] private ParticleSystem _stunEffect;
    [SerializeField][Range(0,2)] private float _stunEffectHeight;

    private float _timeBeforeStunEffect = 0.5f;
    private GameObject Opponent;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(StunAttackFlag)
        {
            Debug.Log("Stun Attack!");
            Opponent = GetComponent<Fight>().Opponent;
            InvokeStunAttackEffect();
        }
    }

    void InvokeStunAttackEffect()
    {
        Debug.Log("InvokeStunAttackEffect!");
        Invoke("InstantiateStun", _timeBeforeStunEffect);
        StunAttackFlag = false;
    }

    void InstantiateStun()
    {
        Vector3 stunEffectPosition = new Vector3(Opponent.transform.position.x, Opponent.transform.position.y + _stunEffectHeight, Opponent.transform.position.z);
        Instantiate(_stunEffect, stunEffectPosition, Quaternion.identity);
    }
}
