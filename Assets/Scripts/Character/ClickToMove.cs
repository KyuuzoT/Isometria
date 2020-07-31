using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToMove : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _approachDistance = 3;
    [SerializeField] private CharacterController _controller;
    [SerializeField] private AnimationClip _idleAnimation;
    [SerializeField] private AnimationClip _runAnimation;
    private Vector3 position;
    private Animation _animationComponent;

    public enum State
    {
        Idle,
        Move,
        Fight
    }

    internal static State CurrentState = State.Idle;

    private void Start()
    {
        position = transform.position;
        _animationComponent = transform.GetComponent<Animation>();
    }

    void FixedUpdate()
    {
        if (!CurrentState.Equals(State.Fight))
        {
            if (Input.GetMouseButton(0))
            {
                locateClickPosition();
            }

            moveToClickPosition();
        }
        else
        {

        }
    }

    private void locateClickPosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000f) && hit.collider.tag != "Player")
        {
            position = hit.point;
        }
    }

    private void moveToClickPosition()
    {
        if (Vector3.Distance(transform.position, position) > _approachDistance)
        {
            Quaternion newRotation;
            ConstrainXZRotation(out newRotation);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * _rotationSpeed);
            _controller.SimpleMove(transform.forward * _moveSpeed);

            _animationComponent.CrossFade(_runAnimation.name);
        }
        else
        {
            _animationComponent.CrossFade(_idleAnimation.name);
        }
    }

    public void ConstrainXZRotation(out Quaternion rotation)
    {
        rotation = Quaternion.LookRotation(position - transform.position, Vector3.forward);
        rotation.x = 0f;
        rotation.z = 0f;
    }
}
