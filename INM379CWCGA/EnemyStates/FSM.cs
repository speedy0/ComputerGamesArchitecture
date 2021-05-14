using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace INM379CWCGA.EnemyStates
{
    class FSM
    {
        private object Owner;
        private List<State> States;

        private State CurrState;

        public FSM()
            : this(null)
        {
        }

        public FSM(object a)
        {
            Owner = a;
            States = new List<State>();
            CurrState = null;
        }

        public void Initialise(string name)
        {
            CurrState = States.Find(state => state.Name.Equals(name));
            if (CurrState != null)
            {
                CurrState.Enter(Owner);
            }
        }

        public void AddState(State state)
        {
            States.Add(state);
        }

        public void Update(GameTime dt)
        {
            // Null check the current state of the FSM
            if (CurrState == null) return;

            // Check the conditions for each transition of the current state
            foreach (Transition t in CurrState.Transitions)
            {
                // If the condition has evaluated to true
                // then transition to the next state
                if (t.Condition())
                {
                    CurrState.Exit(Owner);
                    CurrState = t.NewState;
                    CurrState.Enter(Owner);
                    break;
                }
            }

            // Execute the current state
            CurrState.Execute(Owner, dt);
        }
    }
}
