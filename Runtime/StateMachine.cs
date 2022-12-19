using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using TNRD.StateManagement.Contracts;
using UnityEngine.Assertions;

namespace TNRD.StateManagement
{
    public abstract class StateMachine : IStateMachine, IUpdateReceiver
    {
        private struct TransitionData
        {
            public Enum Source { get; set; }
            public Enum Destination { get; set; }
        }

        private readonly IStateFactory stateFactory;
        private readonly ITransitionFactory transitionFactory;
        private readonly IUpdateProvider updateProvider;

        private readonly Dictionary<Enum, Type> stateIdToStateType =
            new Dictionary<Enum, Type>();

        private readonly Dictionary<Enum, Type> transitionIdToTransitionType =
            new Dictionary<Enum, Type>();

        private readonly Dictionary<Enum, TransitionData> transitionIdToTransitionData =
            new Dictionary<Enum, TransitionData>();

        private Enum initialStateId;
        private IState currentState;

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
        protected void SetInitialState(Enum stateId)
        {
            initialStateId = stateId;
        }

        [PublicAPI]
        protected void AddTransition<TSourceState, TTransition, TDestinationState>(
            Enum sourceStateId,
            Enum transitionId,
            Enum destinationStateId
        )
            where TSourceState : IState
            where TTransition : ITransition
            where TDestinationState : IState
        {
            Assert.IsFalse(transitionIdToTransitionType.ContainsKey(transitionId));
            Assert.IsFalse(transitionIdToTransitionData.ContainsKey(transitionId));

            if (stateIdToStateType.ContainsKey(sourceStateId))
                Assert.AreEqual(stateIdToStateType[sourceStateId], typeof(TSourceState));
            if (stateIdToStateType.ContainsKey(destinationStateId))
                Assert.AreEqual(stateIdToStateType[destinationStateId], typeof(TDestinationState));

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
        public void Transition(Enum transitionId)
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
        public void OnTransitionFinished(Enum transitionId)
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
    }
}
