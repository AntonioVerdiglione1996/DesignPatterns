using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Patterns
{
    public interface IState
    {
        void OnStateEnter();    // Initializes the state
        IState Update();        // Updates the state
        void OnStateExit();     // De-initializes the state
    }

    public class Enemy 
    {
        private float sightRadius;  

        private IState currentState;

        // IStates
        StateIdle idle;        
        StateChase chase;
        StateAttack attack;
        public Enemy()
        {
            sightRadius = 100f;

            //Init states
            idle = new StateIdle(this);
            chase = new StateChase(this);
            attack = new StateAttack(this);

            //Linking states NB: questo è brutto
            idle.Chase = chase;
            chase.Idle = idle;
            chase.Attack = attack;
            attack.Idle = idle;
            attack.Chase = chase;

            //Turn on the FSM and set currentstate to idle 
            idle.OnStateEnter();
            currentState = idle;
        }
        public void Update()
        {
            // now the FSM works alone and enemy use is brain alone
            currentState = currentState.Update();
        }

        #region FSM
        //Private and Nested, because they control the owner class
        private class StateIdle : IState
        {
            public StateChase Chase { get; set; }

            private Enemy owner;

            private float time;

            private Random random;

            public StateIdle(Enemy owner)
            {
                this.owner = owner;
                random = new Random();
            }

            public void OnStateEnter()
            {
                //Activate idle animation

                //Enemy is thinking...
                time = (float)random.NextDouble() * 2f;     //Init time to a value from 0f to 2f
            }

            public IState Update()
            {
                time -= 0.02f; // deltatime
                if (time < 0f)
                {
                    //Check if the distance < sightRadius
                    //Then pass to another state
                    this.OnStateExit();
                    Chase.OnStateEnter();
                    return Chase;   //pass to another state


                    this.OnStateEnter();
                }

                return this;    //return the current state
            }

            public void OnStateExit()
            {

            }

        }
        private class StateChase : IState
        {
            public StateIdle Idle { get; set; }
            public StateAttack Attack { get; set; }

            private Enemy owner;

            public StateChase(Enemy owner)
            {
                this.owner = owner;
            }


            public void OnStateEnter()
            {
                //Activate chase(movement) animation
            }

            public void OnStateExit()
            {
            }

            public IState Update()
            {
                return null;
            }
        }
        private class StateAttack : IState
        {
            public StateChase Chase { get; set; }
            public StateIdle Idle { get; set; }

            private Enemy owner;

            public StateAttack(Enemy owner)
            {
                this.owner = owner;
            }

            public void OnStateEnter()
            {
            }

            public void OnStateExit()
            {
            }

            public IState Update()
            {
                return null;
            }
        }
        #endregion
    }
   
}
