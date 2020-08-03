using Assets.Scripts.AdditionalSystems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAI : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _agroRadius;
    [SerializeField] private float _approachDistance = 1;
    [SerializeField] private float _damage = 1;
    [SerializeField] [Range(5,15)] private float _attackSpeed = 1;
    [SerializeField] [Range(0,1)] private float _aggressiveness;
    [SerializeField] private CharacterController _controller;
    [SerializeField] private Renderer _renderComponent;

    [SerializeField] private AnimationClip _idleAnimation;
    [SerializeField] private AnimationClip _idleCombatAnimation;
    [SerializeField] private AnimationClip _runAnimation;
    [SerializeField] private AnimationClip _hitAnimation;
    [SerializeField] private AnimationClip _getHitAnimation;
    [SerializeField] private AnimationClip _deathAnimation;

    private Animation _animationComponent;
    private GameObject _player;
    private HealthSystem _healthSystem;
    private States _currentAnimationState;
    private bool _combatIdleFlag;
    private bool _deathFlag = false;
    private bool _isHit = false;
    int i = 0;

    void Start()
    {
        _animationComponent = transform.GetComponent<Animation>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _healthSystem = gameObject.GetComponent<HealthSystem>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsInAgroRadius() && _aggressiveness >= 0.5f)
        {
            _currentAnimationState = States.Move;
            if (IsInAttackRange())
            {
                _currentAnimationState = States.Fight;
            }
        }
        else if (IsInAgroRadius() && _aggressiveness >= 0.1)
        {
            _currentAnimationState = States.Idle;
            _combatIdleFlag = true;
            if (IsInAttackRange() && _isHit)
            {
                _currentAnimationState = States.Fight;
            }
        }
        else
        {
            _currentAnimationState = States.Idle;
            _combatIdleFlag = false;
        }

        if (IsDead())
        {
            _currentAnimationState = States.Death;
        }
        StatesHandler();
    }

    private bool IsInAttackRange()
    {
        return Vector3.Distance(_player.transform.position, transform.position) <= _approachDistance;
    }

    private void StatesHandler()
    {
        switch (_currentAnimationState)
        {
            case States.Idle:
                if(_combatIdleFlag)
                {
                    _animationComponent.CrossFade(_idleCombatAnimation.name);
                }
                else
                {
                    _animationComponent.CrossFade(_idleAnimation.name);
                }
                break;
            case States.Move:
                MoveTowardsPlayer();
                break;
            case States.Fight:
                AttackPlayer();
                break;
            case States.Death:
                if(!_deathFlag)
                {
                    _animationComponent.CrossFade(_deathAnimation.name);
                    _deathFlag = true;
                }
                break;
            default:
                _animationComponent.CrossFade(_idleAnimation.name);
                break;
        }
    }

    private void AttackPlayer()
    {
        if (!_animationComponent.IsPlaying(_hitAnimation.name))
        {
            StartCoroutine("HitPlayer");
        }
    }

    private IEnumerator HitPlayer()
    {
        if(_player.GetComponent<HealthSystem>().GetCurrentHealth > 0)
        {
            _animationComponent.CrossFade(_hitAnimation.name);
            _player.GetComponent<HealthSystem>().ChangeCurrentHP(-_damage);
            Debug.Log($"{i++} hit\nPlayer's HP:{_player.GetComponent<HealthSystem>().GetCurrentHealth}");
            yield return new WaitForSeconds(20 + (1 - 1 / _attackSpeed));
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
        return Vector3.Distance(_player.transform.position, transform.position) < _agroRadius;
    }

    public void GetHit(float Damage)
    {
        if(!_deathFlag)
        {
            _isHit = true;
            _animationComponent.CrossFade(_getHitAnimation.name);
            _healthSystem.ChangeCurrentHP(-Damage);
            Debug.Log(_healthSystem.GetCurrentHealth);
        }
    }

    private bool IsDead()
    {
        return gameObject.GetComponent<HealthSystem>().GetCurrentHealth <= 0;
    }

    private void OnMouseOver()
    {
        _player.transform.GetComponent<Fight>().Opponent = gameObject;
        ChangeOutlineThickness(0.1f);
    }

    private void OnMouseExit()
    {
        ChangeOutlineThickness(0.002f);
    }

    private void ChangeOutlineThickness(float thickness)
    {
        Material[] mats = _renderComponent.materials;

        foreach (Material item in mats)
        {
            item.SetFloat("_Outline", thickness);
        }
    }
}
