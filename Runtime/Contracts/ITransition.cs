using System;

namespace TNRD.StateManagement.Contracts
{
    public interface ITransition : IState
    {
        Enum TransitionId { get; }
    }
}
