using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace INM379CWCGA.EnemyStates
{
    class Chasestate : State
    {
        public Chasestate()
        {
            Name = "Chase";
        }

        public override void Enter(object owner)
        {
            Mage mg = owner as Mage;
        }

        public override void Execute(object owner, GameTime dt)
        {
            Mage mg = owner as Mage;
            if (mg == null) return;
            //if (mg.Target == null)
            //{
                //SelectTarget(mg);
            //}

            //if (mg.Target != null)
            //{
             //   Vector3 targetPosition = mg.Target.Position;
             //   mg.Direction = targetPosition - mg.Position;
             //   mg.Direction.Normalize();
            //}
        }

        public override void Exit(object owner)
        {
            Mage mg = owner as Mage;
        }
    }
}
