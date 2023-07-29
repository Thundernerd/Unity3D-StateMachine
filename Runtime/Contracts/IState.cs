using BrunoMikoski.ScriptableObjectCollections;

namespace TNRD.StateManagement.Contracts
{
    public interface IState
    {
        ScriptableObjectCollectionItem StateId { get; }
        void OnEnter();
        void OnExit();
    }
}
