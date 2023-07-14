using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TNRD.StateManagement
{
    [CreateAssetMenu(fileName = "State Machine Graph")]
    public partial class StateMachineGraph : ScriptableObject
    {
        [SerializeField] private string stateMachineName;
        [SerializeField] private string @namespace;
        [SerializeField] private string destination;
#if HAS_ZENJECT
        [SerializeField] private bool useZenject;
#endif

        [SerializeField] private List<StateData> states;

        [SerializeField] private List<TransitionData> transitions;

        internal string StateMachineName => stateMachineName;
        internal string Namespace => @namespace;
        internal string Destination => destination;

        public string FullStateMachineName => $"{stateMachineName}StateMachine";
        public string SubContainerManagerName => $"{stateMachineName}SubContainerManager";
        public string StateIdName => $"{stateMachineName}StateId";
        public string TransitionIdName => $"{stateMachineName}TransitionId";
        public string BaseStateName => $"Base{stateMachineName}State";
        public string BaseTransitionName => $"Base{stateMachineName}Transition";
        public string StateFactoryName => $"{stateMachineName}StateFactory";
        public string TransitionFactoryName => $"{stateMachineName}TransitionFactory";
        public string UpdateProviderName => $"{stateMachineName}UpdateProvider";

#if HAS_ZENJECT
        public bool UseZenject => useZenject;
#endif

        internal List<StateData> States => states;

        internal List<TransitionData> GetTransitions()
        {
            List<TransitionData> transitionData = new();

            List<string> anyStates = GetAnyStates();
            transitionData.AddRange(UpdateExistingTransitions(transitions, anyStates));
            transitionData.AddRange(CreateAnyStateTransitions(anyStates));

            return transitionData;
        }

        private List<string> GetAnyStates()
        {
            List<string> anyStates = new();

            foreach (StateData state in states)
            {
                bool hasTransition = transitions.Any(transition =>
                    transition.Source == state.Name || transition.Destinations.Contains(state.Name));

                if (!hasTransition)
                    anyStates.Add(state.Name);
            }

            return anyStates;
        }

        private IEnumerable<TransitionData> UpdateExistingTransitions(
            IEnumerable<TransitionData> existingTransitions,
            IReadOnlyCollection<string> anyStates
        )
        {
            return existingTransitions.Select(existingTransition => new TransitionData(this,
                    existingTransition.Source,
                    existingTransition.Destinations.Concat(anyStates).ToArray()))
                .ToList();
        }

        private IEnumerable<TransitionData> CreateAnyStateTransitions(List<string> anyStates)
        {
            List<TransitionData> anyStateTransitions = new();

            foreach (string anyState in anyStates)
            {
                foreach (StateData stateData in states)
                {
                    if (stateData.IsInitialState)
                        continue;

                    if (stateData.Name == anyState)
                        continue;

                    anyStateTransitions.Add(new TransitionData(this, anyState, new[] { stateData.Name }));
                }
            }

            return anyStateTransitions;
        }
    }
}
