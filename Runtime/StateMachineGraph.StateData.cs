using System;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TNRD.StateManagement
{
    public partial class StateMachineGraph
    {
        [Serializable]
        internal class StateData
        {
            [SerializeField, HideInInspector] private StateMachineGraph graph;
            [SerializeField, HideInInspector] private string guid;
            [SerializeField] private string name;

            [SerializeField, ShowIf(nameof(CanShowIsInitialInState))]
            private bool isInitialState;

            public StateData(StateMachineGraph graph)
            {
                this.graph = graph;
                guid = Guid.NewGuid().ToString();
                name = string.Empty;
                isInitialState = false;
            }

            private bool CanShowIsInitialInState()
            {
                return isInitialState || graph.states.All(x => !x.IsInitialState);
            }

            public string Name => name;
            public bool IsInitialState => isInitialState;
        }
    }
}
