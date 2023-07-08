namespace TNRD.StateManagement.Contracts
{
    public interface IStateMachineController
    {
        IStateMachine StateMachine { get; }
    }
}
