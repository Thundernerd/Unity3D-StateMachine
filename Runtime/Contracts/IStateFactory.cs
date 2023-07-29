using System;
using BrunoMikoski.ScriptableObjectCollections;

namespace TNRD.StateManagement.Contracts
{
    public interface IStateFactory
    {
        void Initialize(IStateMachine stateMachine);
        IState Instantiate(ScriptableObjectCollectionItem stateId, Type stateType);
    }
}
