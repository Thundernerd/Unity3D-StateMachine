using System;

namespace TNRD.StateManagement
{
    public struct PossibleTransition
    {
        public Enum TransitionId { get; }
        public Enum DestinationStateId { get; }

        public PossibleTransition(Enum transitionId, Enum destinationStateId)
        {
            TransitionId = transitionId;
            DestinationStateId = destinationStateId;
        }
    }
}
