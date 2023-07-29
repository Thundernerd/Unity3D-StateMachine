using System;
using BrunoMikoski.ScriptableObjectCollections;

namespace TNRD.StateManagement
{
    public struct PossibleTransition
    {
        public ScriptableObjectCollectionItem TransitionId { get; }
        public ScriptableObjectCollectionItem DestinationStateId { get; }

        public PossibleTransition(ScriptableObjectCollectionItem transitionId, ScriptableObjectCollectionItem destinationStateId)
        {
            TransitionId = transitionId;
            DestinationStateId = destinationStateId;
        }
    }
}
