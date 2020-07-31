using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : MonoBehaviour
{
    [SerializeField] private AnimationClip _attackAnimation;
    [SerializeField] private float _damage;

    internal GameObject Opponent;
    private Animation _animationComponent;
    int i = 0;

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
                Debug.Log($"{i++} hit!");
                Opponent.GetComponent<SkeletonAI>().GetHit(_damage);
            }
        }

        if (!_animationComponent.IsPlaying(_attackAnimation.name))
        {
            ClickToMove.CurrentState = ClickToMove.State.Idle;
        }
    }
}
