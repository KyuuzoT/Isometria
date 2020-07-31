using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float _maxHP = 100;
    [SerializeField] private float _currentHP = 100;
    [SerializeField] private float _secondsBeforeDisappear = 360;
    [SerializeField] private AnimationClip _deathAnimation;

    private Animation _animationComponent;

    void Awake()
    {
        if(_maxHP < 1)
        {
            _maxHP = 1;
        }
    }

    private void Start()
    {
        _animationComponent = gameObject.GetComponent<Animation>();
    }

    void Update()
    {
        DeathHandler();
    }

    private void DeathHandler()
    {
        if(_currentHP <= 0)
        {
            _currentHP = 0;
            _animationComponent.CrossFade(_deathAnimation.name);
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
    }
}
