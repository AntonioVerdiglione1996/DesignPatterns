using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Patterns
{
    public abstract class State
    {
        protected StateMachine machine;

        public virtual void Enter()
        {

        }
        public virtual void Exit()
        {

        }
        public virtual void Update()
        {

        }
        public void AssignStateMachine(StateMachine stateMachine)
        {
            this.machine = stateMachine;
        }
    }
    public class StateMachine
    {
        //save all the states in this dictionary
        private Dictionary<int, State> states;
        //current state 
        private State currentState;

        //NB: qui avviene un accoppiamento fortissimo!!! <- Molto Brutto!!
        // probabilmente nel codice dove lo userai avrai un interfaccia o una classe madre
        // che potrai settare come owner : in unity ad esempio puoi usare magari Gameobject---
        private Enemy enemy;
        public Enemy Actor { get { return enemy; } }
        //------------------------------------------------------------------------------------

        public StateMachine(Enemy owner)
        {
            this.states = new Dictionary<int, State>();
            enemy = owner;
        }
        public void RegisterState(int id, State state)
        {
            states.Add(id, state);
            state.AssignStateMachine(this);
        }
        public void Switch(int id)
        {
            if (currentState != null)
            {
                currentState.Exit();
            }
            currentState = states[id];
            currentState.Enter();
        }
        public void Run()
        {
            if (currentState != null)
            {
                currentState.Update();
            }
        }
        //questo enumerato è facoltativo...ma è sempre meglio incapsulare i tuoi index in un enumerato!
        //E' più leggibile!
        public enum States
        {
            Patroling,
            Attack
        }
        //Application of this FSM
        public class Enemy
        {
            //this is his private fsm u can instatiate a lot of fsm with this pattern
            private StateMachine brain;
            public Enemy()
            {
                //gli passi l'owner
                brain = new StateMachine(this);
                //register states in the dictionary
                brain.RegisterState((int)States.Patroling, new PatrolState());
                brain.RegisterState((int)States.Attack, new AttackState());

                brain.Switch((int)States.Patroling);
            }

            public void Update()
            {
                brain.Run();
            }
        }
        class PatrolState : State
        {
            public override void Enter()
            {
                //withdraw sword
                Console.WriteLine("Entering Patroling");
            }
            public override void Update()
            {
                Console.WriteLine("Patroling");
                // checks

                // is player near enough ?
                //float distance = (B - A).Length;
                //if (distance <= lineOfSight)
                //{
                machine.Switch((int)States.Attack);
                //    return;
                //}
            }
            public override void Exit()
            {
                Console.WriteLine("ExitState");
            }
        }
        class AttackState : State
        {
            public override void Enter()
            {
                Console.WriteLine("Entering Attack State");
            }
            public override void Update()
            {
                // checks
                //float distance = (B - A).Length;
                //if (distance > farDistance)
                //{
                machine.Switch((int)States.Patroling);
                //    return;
                //}
            }
            public override void Exit()
            {
                Console.WriteLine("ExitState");
            }
        }
    }
}
