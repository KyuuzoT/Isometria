using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.AI
{
    public enum Animations
    {
        Idle,
        CombatIdle,
        Run,
        GetHit,
        Death
    }

    public class AnimationController
    {
        public Animations CurrentAnimation { get; set; }
    }
}
