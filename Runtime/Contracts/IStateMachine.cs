using System;
using System.Collections.Generic;

namespace TNRD.StateManagement.Contracts
{
    public interface IStateMachine
    {
        IState CurrentState { get; }
        
        void Initialize();
        void Transition(Enum transitionId);
        void OnTransitionFinished(Enum transitionId);

        IEnumerable<PossibleTransition> GetPossibleTransitions();
    }
}
