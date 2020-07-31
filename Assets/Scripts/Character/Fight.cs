using Assets.Scripts.AdditionalSystems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : MonoBehaviour
{
    [SerializeField] private AnimationClip _attackAnimation;
    [SerializeField] private float _damage;
    [SerializeField] private float _impactTime;
    [SerializeField] private float _attackRange = 0;

    internal GameObject Opponent;

    private Animation _animationComponent;
    private bool _impactFlag = false;

    void Start()
    {
        _attackRange += transform.GetComponent<ClickToMove>().GetApproachDistance;
        _animationComponent = gameObject.GetComponent<Animation>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && IsInRange())
        {
            if (!Opponent.Equals(null))
            {
                Vector3 lookDirection = new Vector3(0, Opponent.transform.position.y, 0);
                transform.LookAt(lookDirection);
            }

            ClickToMove.CurrentState = States.Fight;
            _animationComponent.CrossFade(_attackAnimation.name);
        }

        Impact();

        if (!_animationComponent.IsPlaying(_attackAnimation.name))
        {
            ClickToMove.CurrentState = States.Idle;
            _impactFlag = false;
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
        return Vector3.Distance(Opponent.transform.position, transform.position) <= _attackRange;
    }
}
