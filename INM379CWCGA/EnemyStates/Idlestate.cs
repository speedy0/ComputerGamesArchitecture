using System;
using Microsoft.Xna.Framework;

namespace INM379CWCGA.EnemyStates
{
    class Idlestate : State
    {
        #region Properties
        private double CurrTimer = 0.0;
        private const double DirChange = 0.5;
        #endregion

        #region Constructor
        public Idlestate()
        {
            Name = "idle";
        }
        #endregion

        public override void Enter(object owner)
        {
            Mage mg = owner as Mage;
            CurrTimer = 0.0;
        }

        public override void Execute(object owner, GameTime dt)
        {
            Mage mg = owner as Mage;
            if (mg == null) return;

            if (CurrTimer >= DirChange)
            {
                CurrTimer = 0.0;
                mg.IdleAction(dt);
            }
            else
            {
                CurrTimer += dt.ElapsedGameTime.TotalSeconds;
            }
        }

        public override void Exit(object owner)
        {
            Mage mg = owner as Mage;
            CurrTimer = 0.0;
        }
    }
}
