using System;

namespace TNRD.StateManagement.Contracts
{
    public interface ITransitionFactory
    {
        void Initialize(IStateMachine stateMachine);
        ITransition Instantiate(Enum transitionId, Type transitionType);
    }
}
