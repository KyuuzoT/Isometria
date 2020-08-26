using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSystem : MonoBehaviour
{
    public static AnimationClip Animation;

    [SerializeField] private static List<AnimationClip> _animations;

    void Start()
    {
        if(_animations.Count == 0)
        {
            //TODO: exception
            //throw new System.Exception
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Equals("attack")

    }

    internal static AnimationClip ReturnAnimationClip(AnimationStates state)
    {
        switch (state)
        {
            case AnimationStates.Attack:
                Animation = _animations.Find(s => s.name.Contains("attack"));
                break;
            case AnimationStates.Die:
                Animation = _animations.Find(s => s.name.Contains("die"));
                break;
            case AnimationStates.GetHit:
                Animation = _animations.Find(s => s.name.Equals("gethit"));
                break;
            case AnimationStates.Idle:
                Animation = _animations.Find(s => s.name.Equals("idle"));
                break;
            case AnimationStates.Run:
                Animation = _animations.Find(s => s.name.Equals("run"));
                break;
            case AnimationStates.BattleIdle:
                Animation = _animations.Find(s => s.name.Equals("waitingforbattle"));
                break;
            default:
                Animation = _animations.Find(s => s.name.Equals("idle"));
                break;
        }

        return Animation;
    }
}
