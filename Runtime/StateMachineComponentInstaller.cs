#if HAS_ZENJECT
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace TNRD.StateManagement
{
    public abstract class StateMachineComponentInstaller<TSubContainerManager> : MonoInstaller
        where TSubContainerManager : SubContainerManager
    {
        [Inject] private TSubContainerManager subContainerManager;

        [SerializeField] private Component componentToInstall;
        [SerializeField] private bool bindInterfaces;

        [SerializeField, HideIf(nameof(bindInterfaces))]
        private bool withId;

        [SerializeField, ShowIf("@!this.bindInterfaces && this.withId")]
        private string id;

        private Type installedType;

        protected abstract IEnumerable<Enum> GetIds();

        /// <inheritdoc />
        public sealed override void InstallBindings()
        {
            installedType = componentToInstall.GetType();

            foreach (Enum stateId in GetIds())
            {
                if (bindInterfaces)
                {
                    subContainerManager.GetContainer(stateId)
                        .BindInterfacesAndSelfTo(installedType)
                        .FromInstance(componentToInstall);
                }
                else if (withId)
                {
                    subContainerManager.GetContainer(stateId)
                        .Bind(installedType)
                        .WithId(id)
                        .FromInstance(componentToInstall);
                }
                else
                {
                    subContainerManager.GetContainer(stateId)
                        .Bind(installedType)
                        .FromInstance(componentToInstall);
                }
            }
        }

        private void OnDestroy()
        {
            foreach (Enum stateId in GetIds())
            {
                if (bindInterfaces)
                {
                    subContainerManager.GetContainer(stateId)
                        .UnbindInterfacesTo(installedType);
                }
                else if (withId)
                {
                    subContainerManager.GetContainer(stateId)
                        .UnbindId(installedType, id);
                }
                else
                {
                    subContainerManager.GetContainer(stateId)
                        .Unbind(installedType);
                }
            }
        }
    }
}
#endif
