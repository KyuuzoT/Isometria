using System;
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

    void Start()
    {
        _animationComponent = transform.GetComponent<Animation>();
        _player = GameObject.FindGameObjectWithTag("Player");
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
            Vector3 lookDirection = new Vector3(0, _player.transform.position.y, 0);
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
        Debug.Log($"Distance to enemy: {Vector3.Distance(_player.transform.position, transform.position)}");
        if(Vector3.Distance(_player.transform.position, transform.position) < _agroRadius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void GetHit(int damage)
    {
        //TODO: health reduction
    }

    private void OnMouseOver()
    {
        _player.transform.GetComponent<Fight>().Opponent = gameObject;
    }
}
