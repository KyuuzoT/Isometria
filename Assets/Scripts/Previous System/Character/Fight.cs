using Assets.Scripts.AdditionalSystems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : MonoBehaviour
{
    public bool SpecialAttackFlag { get; set; }

    [SerializeField] private AnimationClip _attackAnimation;
    [SerializeField] private AnimationClip _deathAnimation;
    [SerializeField] private float _damage;
    [SerializeField] private float _impactTime;
    [SerializeField] private float _attackRange = 0;
    [SerializeField] private float _combatEscapeTime;
    [SerializeField] private int _stunTimeSeconds;
    [SerializeField] private ParticleSystem _stunEffect;
    private float _countDown;

    internal GameObject Opponent;

    private Animation _animationComponent;
    private bool _impactFlag = false;
    private bool _deathFlag = false;
    private bool _stunAttackFlag;

    public float Damage { get; set; }

    void Start()
    {
        _attackRange += transform.GetComponent<ClickToMove>().GetApproachDistance;
        _animationComponent = gameObject.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!_deathFlag)
        {
            if (Input.GetKeyUp(KeyCode.Alpha1) && IsInRange())
            {
                GetComponent<SpecialAttack>().StunAttackFlag = true;
                Opponent.GetComponent<SkeletonAI>().Stun(_stunTimeSeconds);
                LookAtEnemy();
                PlayHitAnimation();
            }

            AttackTarget();
        }
    }

    private void AttackTarget()
    {
        if (Input.GetKey(KeyCode.Space) && IsInRange())
        {
            LookAtEnemy();
            PlayHitAnimation();
        }

        Impact();

        if (IsDead())
        {
            PlayDeathAnimation();
        }
        else if (!_animationComponent.IsPlaying(_attackAnimation.name))
        {
            ClickToMove.CurrentState = States.Idle;
            _impactFlag = false;
        }
    }

    private bool IsDead()
    {
        return transform.GetComponent<HealthSystemDeprecated>().GetCurrentHealth <= 0;
    }

    private void PlayDeathAnimation()
    {
        ClickToMove.CurrentState = States.Death;
        Debug.Log("Death animation");
        _animationComponent.Play(_deathAnimation.name);
        transform.GetComponent<ClickToMove>().enabled = false;
        _deathFlag = true;
    }

    private void PlayHitAnimation()
    {
        ClickToMove.CurrentState = States.Fight;
        _animationComponent.CrossFade(_attackAnimation.name);
    }

    private void LookAtEnemy()
    {
        if (!Opponent.Equals(null))
        {
            transform.LookAt(Opponent.transform.position);
        }
    }

    private void Impact()
    {
        if (Opponent != null && _animationComponent.IsPlaying(_attackAnimation.name) && !_impactFlag)
        {
            float AnimationTime = _animationComponent[_attackAnimation.name].time;
            float AnimationImpact = _animationComponent[_attackAnimation.name].length * _impactTime;
            if (AnimationTime > AnimationImpact)
            {
                _countDown = _combatEscapeTime;
                Opponent.GetComponent<SkeletonAI>().GetHit(_damage);
                CancelInvoke("CombatEscapeCountDown");
                InvokeRepeating("CombatEscapeCountDown", time: 0, repeatRate: 1);
                _impactFlag = true;
                _animationComponent.Stop(_attackAnimation.name);
            }
        }
    }

    private void CombatEscapeCountDown()
    {
        _countDown--;
        if(_countDown == 0)
        {
            CancelInvoke("CombatEscapeCountDown");
        }
    }

    private bool IsInRange()
    {
        //Debug.Log($"Opponent: {Opponent.transform.position}\nPlayer: {transform.position}");
        if(Opponent != null)
        {
            return Vector3.Distance(Opponent.transform.position, transform.position) <= _attackRange;
        }
        else
        {
            return false;
        }
    }
}
