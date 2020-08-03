using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float _maxHP = 100;
    [SerializeField] private float _currentHP = 100;
    [SerializeField] private float _secondsBeforeDisappear = 360;

    private bool _deathFlag = false;

    public float GetCurrentHealth
    {
        get
        {
            return _currentHP;
        }
    }

    void Awake()
    {
        if(_maxHP < 1)
        {
            _maxHP = 1;
        }
    }

    void Update()
    {
        if(!_deathFlag)
        {
            if (gameObject.tag == "Player")
            {
                DeathHandler("YOU'RE DEAD!");
            }
            else
            {
                DeathHandler();
            }
        }
    }

    private void DeathHandler(string deathMessage)
    {
        if (_currentHP <= 0)
        {
            _deathFlag = true;
            _currentHP = 0;
            Debug.Log($"{deathMessage}");
        }
    }

    private void DeathHandler()
    {
        if(_currentHP <= 0)
        {
            _deathFlag = true;
            _currentHP = 0;
            Invoke("DestroyObject", _secondsBeforeDisappear);
        }
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }

    public void ChangeCurrentHP(float ammount)
    {
        if(_currentHP + ammount >= _maxHP)
        {
            _currentHP = _maxHP;
        }
        else
        {
            _currentHP += ammount;
        }

        if (_currentHP < 0)
        {
            _currentHP = 0;
        }
    }
}
