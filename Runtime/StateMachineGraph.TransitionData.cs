using System;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace TNRD.StateManagement
{
    public partial class StateMachineGraph
    {
        [Serializable]
        internal class TransitionData
        {
            [Serializable]
            internal class Destination
            {
                public string state;
            }
            
            [SerializeField, UsedImplicitly] private StateMachineGraph graph;
            [SerializeField] private string guid;
            [SerializeField] private string source;
            [SerializeField] private Destination[] destinations;

            public TransitionData(StateMachineGraph graph)
            {
                this.graph = graph;
                guid = Guid.NewGuid().ToString();
                source = string.Empty;
                destinations = Array.Empty<Destination>();
            }

            public string Source => source;
            public string[] Destinations => destinations.Select(x=>x.state).ToArray();
        }
    }
}
