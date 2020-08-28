using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int _maximalHP;
    [SerializeField] private int _currentHP;
    

    private bool _deathFlag = false;

    internal int GetCurrentHP
    {
        get
        {
            return _currentHP;
        }
    }

    internal int MaximalHP
    {
        get
        {
            return _maximalHP;
        }
        set
        {
            _maximalHP = value;
        }
    }

    void Awake()
    {
        if(_maximalHP <= 1)
        {
            _maximalHP = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!_deathFlag)
        {
            DeathHandler();
        }
    }

    private void DeathHandler()
    {
        _deathFlag = true;
        if(_currentHP <= 0)
        {
            _currentHP = 0;
            if (gameObject.tag.Equals("Player"))
            {
                Debug.Log("YOU'RE DEAD!");
            }
            else
            {
                transform.GetComponent<CorpseSystem>().InvokeDestroying();
            }
        }
    }
}
