using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : MonoBehaviour
{
    [SerializeField] private AnimationClip _attackAnimation;
    [SerializeField] private float _damage;
    [SerializeField] private float _impactTime;

    internal GameObject Opponent;
    private Animation _animationComponent;
    private bool _impactFlag = false;

    void Start()
    {
        _animationComponent = gameObject.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            ClickToMove.CurrentState = ClickToMove.State.Fight;
            _animationComponent.Play(_attackAnimation.name);

            if (!Opponent.Equals(null))
            {
                Vector3 lookDirection = new Vector3(0, Opponent.transform.position.y, 0);
                transform.LookAt(lookDirection);
            }
        }

        if (!_animationComponent.IsPlaying(_attackAnimation.name))
        {
            ClickToMove.CurrentState = ClickToMove.State.Idle;
            _impactFlag = false;
        }

        Impact();
    }

    private void Impact()
    {
        //Debug.Log($"Opponent: {Opponent}\nAnimation: {_animationComponent.IsPlaying(_attackAnimation.name)}\nFlag: {_impactFlag}");
        if(Opponent != null && _animationComponent.IsPlaying(_attackAnimation.name) && !_impactFlag)
        {
            float AnimationTime = _animationComponent[_attackAnimation.name].time;
            float AnimationImpact = _animationComponent[_attackAnimation.name].length * _impactTime;
            Debug.Log($"Animation Time: {AnimationTime}\nAnimation Impact: {AnimationImpact}");
            if (AnimationTime > AnimationImpact)
            {
                Opponent.GetComponent<SkeletonAI>().GetHit(_damage);
                _impactFlag = true;
            }
        }
    }
}
