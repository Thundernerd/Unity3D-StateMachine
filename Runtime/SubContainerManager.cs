#if HAS_ZENJECT
using System;
using System.Collections.Generic;
using Zenject;

namespace TNRD.StateManagement
{
    public abstract class SubContainerManager
    {
        [Inject] private readonly DiContainer container;
        private readonly Dictionary<Enum, DiContainer> enumToSubContainer;

        protected SubContainerManager()
        {
            enumToSubContainer = new Dictionary<Enum, DiContainer>();
        }

        public DiContainer GetContainer(Enum id)
        {
            if (enumToSubContainer.TryGetValue(id, out DiContainer subContainer))
                return subContainer;

            enumToSubContainer[id] = (subContainer = container.CreateSubContainer());
            return subContainer;
        }
    }
}
#endif
