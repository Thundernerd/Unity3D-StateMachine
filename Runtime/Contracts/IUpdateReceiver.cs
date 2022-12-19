namespace TNRD.StateManagement.Contracts
{
    public interface IUpdateReceiver
    {
        void Update();
        void FixedUpdate();
        void LateUpdate();
    }
}
