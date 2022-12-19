using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TNRD.StateManagement
{
    public abstract class StateMachineComponentInstaller<TSubContainerManager> : MonoInstaller
        where TSubContainerManager : SubContainerManager
    {
        [Inject] private TSubContainerManager subContainerManager;

        [SerializeField] private Component componentToInstall;
        [SerializeField] private string id;
        private Type installedType;

        protected abstract IEnumerable<Enum> GetIds();

        /// <inheritdoc />
        public sealed override void InstallBindings()
        {
            installedType = componentToInstall.GetType();

            foreach (Enum stateId in GetIds())
            {
                subContainerManager.GetContainer(stateId)
                    .Bind(installedType)
                    .WithId(id)
                    .FromInstance(componentToInstall);
            }
        }

        private void OnDestroy()
        {
            foreach (Enum stateId in GetIds())
            {
                subContainerManager.GetContainer(stateId)
                    .UnbindId(installedType, id);
            }
        }
    }
}
