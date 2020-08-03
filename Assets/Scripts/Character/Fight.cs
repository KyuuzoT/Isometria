using Assets.Scripts.AdditionalSystems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : MonoBehaviour
{
    [SerializeField] private AnimationClip _attackAnimation;
    [SerializeField] private AnimationClip _deathAnimation;
    [SerializeField] private float _damage;
    [SerializeField] private float _impactTime;
    [SerializeField] private float _attackRange = 0;

    internal GameObject Opponent;

    private Animation _animationComponent;
    private bool _impactFlag = false;
    private bool _deathFlag = false;

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
    }

    private bool IsDead()
    {
        return transform.GetComponent<HealthSystem>().GetCurrentHealth <= 0;
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
            Vector3 lookDirection = new Vector3(0, Opponent.transform.position.y, 0);
            transform.LookAt(lookDirection);
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
                Opponent.GetComponent<SkeletonAI>().GetHit(_damage);
                _impactFlag = true;
                _animationComponent.Stop(_attackAnimation.name);
            }
        }
    }

    private bool IsInRange()
    {
        //Debug.Log($"Opponent: {Opponent.transform.position}\nPlayer: {transform.position}");
        return Vector3.Distance(Opponent.transform.position, transform.position) <= _attackRange;
    }
}
