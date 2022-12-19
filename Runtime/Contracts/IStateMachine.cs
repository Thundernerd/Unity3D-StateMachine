using System;

namespace TNRD.StateManagement.Contracts
{
    public interface IStateMachine
    {
        void Initialize();
        void Transition(Enum transitionId);
        void OnTransitionFinished(Enum transitionId);
    }
}
