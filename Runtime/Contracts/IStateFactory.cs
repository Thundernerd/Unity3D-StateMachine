using System;

namespace TNRD.StateManagement.Contracts
{
    public interface IStateFactory
    {
        void Initialize(IStateMachine stateMachine);
        IState Instantiate(Enum stateId, Type stateType);
    }
}
