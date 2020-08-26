using Assets.Scripts.AdditionalSystems;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SkeletonAI : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _agroRadius;
    [SerializeField] private float _approachDistance = 1;
    [SerializeField] private float _damage = 1;
    [SerializeField] [Range(5, 15)] private float _attackSpeed = 1;
    [SerializeField] [Range(0, 1)] private float _aggressiveness;
    [SerializeField] private int _stunTimeSeconds = 3;
    [SerializeField] private CharacterController _controller;
    [SerializeField] private Renderer _renderComponent;
    [SerializeField] private AudioClip attackSound;

    [SerializeField] private AnimationClip _idleAnimation;
    [SerializeField] private AnimationClip _idleCombatAnimation;
    [SerializeField] private AnimationClip _runAnimation;
    [SerializeField] private AnimationClip _hitAnimation;
    [SerializeField] private AnimationClip _getHitAnimation;
    [SerializeField] private AnimationClip _deathAnimation;

    [SerializeField] private EnemyHealthBar _script;
    [SerializeField] private int _experience = 100;


    private Animation _animationComponent;
    private GameObject _player;
    private HealthSystem _healthSystem;
    private States _currentAnimationState;
    private States _tempState;
    private bool _combatIdleFlag;
    private bool _deathFlag = false;
    private bool _isHit = false;
    private float _stunTimer;
    int i = 0;
    private Vector3 _soundPosition;

    void Start()
    {
        _animationComponent = transform.GetComponent<Animation>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _healthSystem = gameObject.GetComponent<HealthSystem>();
        _script.enabled = false;
        _soundPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(_currentAnimationState != States.Stun)
        {
            if (IsInAgroRadius())
            {
                AggressiveBehaviour(_aggressiveness);
            }
        }

        if (IsDead())
        {
            _currentAnimationState = States.Death;
        }
        StatesHandler();
    }

    private void AggressiveBehaviour(float aggressivenes)
    {
        if(aggressivenes >= 0.5f)
        {
            _currentAnimationState = States.Move;
            if (IsInAttackRange())
            {
                _currentAnimationState = States.Fight;
            }
        }
        else if (aggressivenes >= 0.1f)
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
                if (IsInAttackRange())
                {
                    AttackPlayer();
                }
                else
                {
                    AggressiveBehaviour(_aggressiveness);
                }
                break;
            case States.Death:
                if(!_deathFlag)
                {
                    _animationComponent.CrossFade(_deathAnimation.name);
                    _deathFlag = true;
                    _player.GetComponent<LevelSystem>().AddExperience(_experience);
                }
                break;
            case States.Stun:
                GetStunned();
                break;
            default:
                _animationComponent.CrossFade(_idleAnimation.name);
                break;
        }
    }

    public void Stun(int seconds)
    {
        _tempState = _currentAnimationState;
        _currentAnimationState = States.Stun;
        _stunTimeSeconds = seconds;
        _stunTimer = _stunTimeSeconds;
    }

    private void GetStunned()
    {
        _animationComponent.CrossFade(_idleCombatAnimation.name);
        if (_stunTimer > 0)
        {
            _stunTimer -= Time.deltaTime;
            Debug.Log(_stunTimer);
        }
        else
        {
            _currentAnimationState = _tempState;
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
            transform.LookAt(_player.transform.position);
            _animationComponent.CrossFade(_hitAnimation.name);
            _player.GetComponent<HealthSystem>().ChangeCurrentHP(-_damage);
            Debug.Log($"{i++} hit\nPlayer's HP:{_player.GetComponent<HealthSystem>().GetCurrentHealth}");
            PlayAttackSound();
            yield return new WaitForSeconds(2 + (1 - 1 / _attackSpeed));
        }
    }

    private async void PlayAttackSound()
    {
        Debug.Log("Playing!");
        await Task.Run(() => PlaySound(attackSound));
    }

    void PlaySound(AudioClip sound)
    {
        AudioSource.PlayClipAtPoint(sound, _soundPosition);
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
        ChangeOutlineThickness(0.08f);
    }

    private void OnMouseDown()
    {
        _player.transform.GetComponent<Fight>().Opponent = gameObject;
        ChangeOutlineThickness(0.1f);
        if (!_deathFlag)
        {
            _script.enabled = true;
        }
    }

    private void OnMouseExit()
    {
        ChangeOutlineThickness(0.002f);
        if(_deathFlag)
        {
            _script.enabled = false;
        }
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
