using System;

namespace TNRD.StateManagement.Contracts
{
    public interface IState
    {
        Enum StateId { get; }
        void OnEnter();
        void OnExit();
    }
}
