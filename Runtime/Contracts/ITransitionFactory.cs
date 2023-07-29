using System;
using BrunoMikoski.ScriptableObjectCollections;

namespace TNRD.StateManagement.Contracts
{
    public interface ITransitionFactory
    {
        void Initialize(IStateMachine stateMachine);
        ITransition Instantiate(ScriptableObjectCollectionItem transitionId, Type transitionType);
    }
}
