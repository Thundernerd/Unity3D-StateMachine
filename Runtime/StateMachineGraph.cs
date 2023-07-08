using System.Collections.Generic;
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

        [SerializeField]
        private List<StateData> states;

        [SerializeField]
        private List<TransitionData> transitions;

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
        internal List<TransitionData> Transitions => transitions;
    }
}
