using System;
using JetBrains.Annotations;
using UnityEngine;

namespace TNRD.StateManagement
{
    public partial class StateMachineGraph
    {
        [Serializable]
        internal class StateData
        {
            [SerializeField, UsedImplicitly] private StateMachineGraph graph;
            [SerializeField] private string guid;
            [SerializeField] private string name;

            [SerializeField]
            private bool isInitialState;

            public StateData(StateMachineGraph graph)
            {
                this.graph = graph;
                guid = Guid.NewGuid().ToString();
                name = string.Empty;
                isInitialState = false;
            }

            public string Name => name;
            public bool IsInitialState => isInitialState;
        }
    }
}
