using System;
using System.Collections.Generic;
using BrunoMikoski.ScriptableObjectCollections;

namespace TNRD.StateManagement.Contracts
{
    public interface IStateMachine
    {
        IState CurrentState { get; }
        
        void Initialize();
        void Transition(ScriptableObjectCollectionItem transitionId);
        void OnTransitionFinished(ScriptableObjectCollectionItem transitionId);

        IEnumerable<PossibleTransition> GetPossibleTransitions();
    }
}
