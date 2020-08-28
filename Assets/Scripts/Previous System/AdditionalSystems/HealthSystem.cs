using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Obsolete("This system is deprecated, use HealthSystem instead.", true)]
public class HealthSystemDeprecated : MonoBehaviour
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

    public float MaximalHealth
    {
        get
        {
            return _maxHP;
        }
        set
        {
            _maxHP = value;
        }
    }

    void Awake()
    {
        if (_maxHP < 1)
        {
            _maxHP = 1;
        }
    }

    void Update()
    {
        if (_deathFlag)
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
            _currentHP = 0;
            Debug.Log($"{deathMessage}");
        }
        _deathFlag = false;
    }

    private void DeathHandler()
    {
        if (_currentHP <= 0)
        {
            _currentHP = 0;
            Invoke("DestroyObject", _secondsBeforeDisappear);
        }
        _deathFlag = false;
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }

    public void ChangeCurrentHP(float ammount)
    {
        if (_currentHP + ammount >= _maxHP)
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
            _deathFlag = true;
        }
    }
}
