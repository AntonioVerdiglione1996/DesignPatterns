using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Patterns
{
    class FSM_Delegates_Simple
    {
        //your states in this pattern are only methods with the same signature of the delegate:

        //1) create your delegate with the same signature of your states/Methods
        private delegate void StateMachine_State(); //declaretion on the fly of this type

        //2) save a current state
        private StateMachine_State currentState;

        //3) inside your initializer (Ctor or Awake or Start) save your currentState to the IdleState
        public FSM_Delegates_Simple()
        {
            currentState = Idle;
            //you can see current state as reference type that point a method with the same signature
            //Current State is type StateMachine_State that is the type we declared in point 1),
            //that point to a method with that signature (Signature: Return Type and Parameters)
        }
        //6) inside your main Loop(updates or while) just run your currentState
        void Update()
        {
            currentState();
        }
        //4) this is a state of your stateMachine becouse have the same signature of the declared type 1)
        void Idle()
        {
            //here go the implementation of this method:


            //now checks: 
            //if your condition is verified than switch your state in another state
            //if(distance < 0.5f)
            //{
            currentState = Attack;
            //}
        }

        //5)another state
        void Attack()
        {
            //if(t< 0)
            //{
            currentState = Idle;
            //}
        }

    }
}
