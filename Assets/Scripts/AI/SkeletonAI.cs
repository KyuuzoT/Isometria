﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAI : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _agroRadius;
    [SerializeField] private float _approachDistance = 1;
    [SerializeField] [Range(0,1)] private float _aggressiveness;
    [SerializeField] private CharacterController _controller;

    [SerializeField] private AnimationClip _idleAnimation;
    [SerializeField] private AnimationClip _idleCombatAnimation;
    [SerializeField] private AnimationClip _runAnimation;

    private Animation _animationComponent;
    private GameObject _player;
    private HealthSystem _healthSystem;

    void Start()
    {
        _animationComponent = transform.GetComponent<Animation>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _healthSystem = gameObject.GetComponent<HealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsInAgroRadius() && _aggressiveness >=0.5f)
        {
            MoveTowardsPlayer();
        }
        else if(IsInAgroRadius() && _aggressiveness >= 0.1)
        {
            _animationComponent.CrossFade(_idleCombatAnimation.name);
        }
        else
        {
            _animationComponent.CrossFade(_idleAnimation.name);
        }
    }

    private void MoveTowardsPlayer()
    {
        if (Vector3.Distance(_player.transform.position, transform.position) > _approachDistance)
        {
            transform.LookAt(_player.transform.position);
            _controller.SimpleMove(transform.forward * _speed);
            _animationComponent.CrossFade(_runAnimation.name);
        }
        else
        {
            _animationComponent.CrossFade(_idleCombatAnimation.name);
        }
    }

    private bool IsInAgroRadius()
    {
        //Debug.Log($"Distance to enemy: {Vector3.Distance(_player.transform.position, transform.position)}");
        if(Vector3.Distance(_player.transform.position, transform.position) < _agroRadius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void GetHit(float Damage)
    {
        _healthSystem.ChangeCurrentHP(-Damage);
        Debug.Log(_healthSystem.GetCurrentHealth);
    }

    private void OnMouseOver()
    {
        _player.transform.GetComponent<Fight>().Opponent = gameObject;
    }
}
