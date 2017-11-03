using Microsoft.Extensions.DependencyInjection;
using System;

namespace Interactors
{
    public class BaseInteractor
    {
        protected IServiceProvider Provider { get; }

        public BaseInteractor(IServiceProvider provider)
        {
            Provider = provider;
        }

        protected T GetService<T>()
        {
            var service = Provider.GetService<T>();
            if (service == null)
                throw new NullReferenceException($"Implementation of {nameof(T)} interface does not found.");

            return service;
        }
    }
}
