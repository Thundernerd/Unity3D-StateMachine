using System;
using System.Collections.Generic;
using BrunoMikoski.ScriptableObjectCollections;
using JetBrains.Annotations;
using TNRD.StateManagement.Contracts;
using UnityEngine.Assertions;

namespace TNRD.StateManagement
{
    public abstract class StateMachine : IStateMachine, IUpdateReceiver
    {
        private class TransitionData
        {
            public ScriptableObjectCollectionItem Source { get; set; }
            public ScriptableObjectCollectionItem Destination { get; set; }
        }

        private readonly IStateFactory stateFactory;
        private readonly ITransitionFactory transitionFactory;
        private readonly IUpdateProvider updateProvider;

        private readonly Dictionary<ScriptableObjectCollectionItem, Type> stateIdToStateType = new();
        private readonly Dictionary<ScriptableObjectCollectionItem, Type> transitionIdToTransitionType = new();
        private readonly Dictionary<ScriptableObjectCollectionItem, TransitionData> transitionIdToTransitionData = new();

        private ScriptableObjectCollectionItem initialStateId;
        private IState currentState;

        IState IStateMachine.CurrentState => currentState;

        protected StateMachine(
            IStateFactory stateFactory,
            ITransitionFactory transitionFactory,
            IUpdateProvider updateProvider
        )
        {
            this.stateFactory = stateFactory;
            this.stateFactory.Initialize(this);

            this.transitionFactory = transitionFactory;
            this.transitionFactory.Initialize(this);

            this.updateProvider = updateProvider;
            this.updateProvider.Register(this);
        }

        [PublicAPI]
        protected abstract void CreateTransitions();

        /// <inheritdoc />
        [PublicAPI]
        void IStateMachine.Initialize()
        {
            CreateTransitions();

            Type stateType = stateIdToStateType[initialStateId];
            IState state = stateFactory.Instantiate(initialStateId, stateType);

            currentState = state;
            currentState.OnEnter();
        }

        [PublicAPI]
        protected void SetInitialState(ScriptableObjectCollectionItem stateId)
        {
            initialStateId = stateId;
        }

        [PublicAPI]
        protected void AddTransition<TSourceState, TTransition, TDestinationState>(
            ScriptableObjectCollectionItem sourceStateId,
            ScriptableObjectCollectionItem transitionId,
            ScriptableObjectCollectionItem destinationStateId
        )
            where TSourceState : IState
            where TTransition : ITransition
            where TDestinationState : IState
        {
            Assert.IsFalse(transitionIdToTransitionType.ContainsKey(transitionId));
            Assert.IsFalse(transitionIdToTransitionData.ContainsKey(transitionId));

            if (stateIdToStateType.TryGetValue(sourceStateId, out Type sourceStateType))
                Assert.AreEqual(sourceStateType, typeof(TSourceState));
            if (stateIdToStateType.TryGetValue(destinationStateId, out Type destinationStateType))
                Assert.AreEqual(destinationStateType, typeof(TDestinationState));

            stateIdToStateType[sourceStateId] = typeof(TSourceState);
            stateIdToStateType[destinationStateId] = typeof(TDestinationState);
            transitionIdToTransitionType[transitionId] = typeof(TTransition);

            transitionIdToTransitionData[transitionId] = new TransitionData
            {
                Source = sourceStateId,
                Destination = destinationStateId
            };
        }

        [PublicAPI]
        public void Transition(ScriptableObjectCollectionItem transitionId)
        {
            Assert.IsNotNull(currentState);

            TransitionData data = transitionIdToTransitionData[transitionId];
            Assert.AreEqual(data.Source, currentState.StateId);

            currentState.OnExit();

            Type transitionType = transitionIdToTransitionType[transitionId];
            ITransition transition = transitionFactory.Instantiate(transitionId, transitionType);

            currentState = transition;
            currentState.OnEnter();
        }

        /// <inheritdoc />
        [PublicAPI]
        public void OnTransitionFinished(ScriptableObjectCollectionItem transitionId)
        {
            currentState.OnExit();

            TransitionData data = transitionIdToTransitionData[transitionId];
            Type stateType = stateIdToStateType[data.Destination];
            IState state = stateFactory.Instantiate(data.Destination, stateType);

            currentState = state;
            currentState.OnEnter();
        }

        /// <inheritdoc />
        [PublicAPI]
        void IUpdateReceiver.Update()
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (currentState is IUpdateable updateable)
                updateable.Update();
        }

        /// <inheritdoc />
        [PublicAPI]
        void IUpdateReceiver.FixedUpdate()
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (currentState is IFixedUpdateable fixedUpdateable)
                fixedUpdateable.FixedUpdate();
        }

        /// <inheritdoc />
        [PublicAPI]
        void IUpdateReceiver.LateUpdate()
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (currentState is ILateUpdateable lateUpdateable)
                lateUpdateable.LateUpdate();
        }

        /// <inheritdoc />
        [PublicAPI]
        IEnumerable<PossibleTransition> IStateMachine.GetPossibleTransitions()
        {
            List<PossibleTransition> ids = new();

            foreach (KeyValuePair<ScriptableObjectCollectionItem, TransitionData> kvp in transitionIdToTransitionData)
            {
                if (Equals(kvp.Value.Source, currentState.StateId))
                {
                    ids.Add(new PossibleTransition(kvp.Key, kvp.Value.Destination));
                }
            }

            return ids;
        }
    }
}
