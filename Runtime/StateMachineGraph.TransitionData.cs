using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TNRD.StateManagement
{
    public partial class StateMachineGraph
    {
        [Serializable]
        internal class TransitionData
        {
            [SerializeField, HideInInspector] private StateMachineGraph graph;
            [SerializeField, HideInInspector] private string guid;

            [SerializeField, ValueDropdown(nameof(GetSourceStates))]
            private string source;

            [SerializeField, ValueDropdown(nameof(GetDestinationStates)),
             DisableIf("@string.IsNullOrEmpty(this.source)")]
            private string[] destinations;

            public TransitionData(StateMachineGraph graph)
            {
                this.graph = graph;
                guid = Guid.NewGuid().ToString();
                source = string.Empty;
                destinations = Array.Empty<string>();
            }

            private IEnumerable<string> GetSourceStates()
            {
                return graph.states.Select(x => x.Name)
                    .Except(graph.Transitions.Select(x=>x.source));
            }

            private IEnumerable<string> GetDestinationStates()
            {
                return graph.states.Select(x => x.Name)
                    .Except(destinations)
                    .Except(new string[] { source });
            }

            public string Source => source;
            public string[] Destinations => destinations;
        }
    }
}
