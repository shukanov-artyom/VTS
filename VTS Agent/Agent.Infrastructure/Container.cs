using System;
using System.Collections.Generic;
using Ninject;

namespace Agent.Infrastructure
{
    public static class Container
    {
        private static IKernel kernel = new StandardKernel();

        public static void Initialize(IDictionary<Type, Type> mapping)
        {
            foreach (KeyValuePair<Type, Type> pair in mapping)
            {
                kernel.Bind(pair.Key).To(pair.Value);
            }
        }

        public static void AddConstant(Type type, object constant)
        {
            kernel.Bind(type).ToConstant(constant);
        }

        /// <summary>
        /// Removes constant mapping from the container.
        /// </summary>
        public static void RemoveConstant(Type sourceType)
        {
            kernel.Unbind(sourceType);
        }

        /// <summary>
        /// Provides an instance. 
        /// </summary>
        public static T GetInstance<T>() where T : class
        {
            return kernel.Get<T>();
        }
    }
}
