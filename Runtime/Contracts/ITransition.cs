using BrunoMikoski.ScriptableObjectCollections;

namespace TNRD.StateManagement.Contracts
{
    public interface ITransition : IState
    {
        ScriptableObjectCollectionItem TransitionId { get; }
    }
}
