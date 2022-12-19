using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TNRD.StateManagement
{
    [CreateAssetMenu]
    public partial class StateMachineGraph : SerializedScriptableObject
    {
        [SerializeField] private string stateMachineName;
        [SerializeField] private string @namespace;
        [SerializeField, FolderPath] private string destination;
        [SerializeField] private bool useZenject;

        [SerializeField, ListDrawerSettings(CustomAddFunction = nameof(CreateStateData))]
        private List<StateData> states;

        [SerializeField, ListDrawerSettings(CustomAddFunction = nameof(CreateTransitionData))]
        private List<TransitionData> transitions;

        internal string StateMachineName => stateMachineName;
        internal string Namespace => @namespace;
        internal string Destination => destination;

        [BoxGroup("Preview"), ShowInInspector, ReadOnly]
        public string FullStateMachineName => $"{stateMachineName}StateMachine";

        [BoxGroup("Preview"), ShowInInspector, ReadOnly]
        public string SubContainerManagerName => $"{stateMachineName}SubContainerManager";

        [BoxGroup("Preview"), ShowInInspector, ReadOnly]
        public string StateIdName => $"{stateMachineName}StateId";

        [BoxGroup("Preview"), ShowInInspector, ReadOnly]
        public string TransitionIdName => $"{stateMachineName}TransitionId";

        [BoxGroup("Preview"), ShowInInspector, ReadOnly]
        public string BaseStateName => $"Base{stateMachineName}State";

        [BoxGroup("Preview"), ShowInInspector, ReadOnly]
        public string BaseTransitionName => $"Base{stateMachineName}Transition";

        [BoxGroup("Preview"), ShowInInspector, ReadOnly]
        public string StateFactoryName => $"{stateMachineName}StateFactory";

        [BoxGroup("Preview"), ShowInInspector, ReadOnly]
        public string TransitionFactoryName => $"{stateMachineName}TransitionFactory";

        [BoxGroup("Preview"), ShowInInspector, ReadOnly]
        public string UpdateProviderName => $"{stateMachineName}UpdateProvider";

        public bool UseZenject => useZenject;

        internal List<StateData> States => states;
        internal List<TransitionData> Transitions => transitions;

        private StateData CreateStateData()
        {
            return new StateData(this);
        }

        private TransitionData CreateTransitionData()
        {
            return new TransitionData(this);
        }
    }
}
