using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _attackRange;
    internal GameObject Enemy;
    private Animation _playerAnimation;

    // Use this for initialization
    void Start()
    {
        _playerAnimation = gameObject.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsInRange())
        {
            if (Input.GetMouseButton(0) && Enemy.GetComponent<Mob>().IsMobHighlited)
            {
                MakeHit();
            }
        }
    }

    private bool IsInRange()
    {
        if(!Enemy.Equals(null))
        {
            return Vector3.Distance(transform.position, Enemy.transform.position) <= _attackRange;
        }

        return false;
    }

    void MakeHit()
    {
        if (!Enemy.Equals(null))
        {
            LookAtEnemy();
            _playerAnimation.CrossFade(AnimationSystem.ReturnAnimationClip(AnimationStates.Attack).name);
        }
    }

    private void LookAtEnemy()
    {
        gameObject.transform.LookAt(Enemy.transform.position);
    }

    public void GetHit()
    {
    }
}
