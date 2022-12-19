namespace TNRD.StateManagement.Contracts
{
    public interface IUpdateProvider
    {
        void Register(IUpdateReceiver updateReceiver);
        void Deregister(IUpdateReceiver updateReceiver);
    }
}
