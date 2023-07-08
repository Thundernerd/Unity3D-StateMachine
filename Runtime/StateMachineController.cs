using TNRD.StateManagement.Contracts;
using UnityEngine;

namespace TNRD.StateManagement
{
    public abstract class StateMachineController : MonoBehaviour, IStateMachineController
    {
        protected abstract IStateMachine StateMachine { get; }
        
        IStateMachine IStateMachineController.StateMachine => StateMachine;
    }
}
