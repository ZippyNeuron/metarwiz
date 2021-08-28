using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ZippyNeuron.Metarwiz.Abstractions;

namespace ZippyNeuron.Metarwiz.Factories
{
    public abstract class MetarFactoryBase
    {
        protected IMetarItem CreateMetarItem(Type t, int position, string item)
        {
            return (IMetarItem)Activator.CreateInstance(t, new object[] { position, item });
        }

        protected bool IsMatch(Type type, int position, string item)
        {
            return (bool)type
                .GetMethod("IsMatch", BindingFlags.Static | BindingFlags.Public)
                .Invoke(null, new object[] { position, item });
        }

        protected IEnumerable<Type> GetTypesOfBase(Type baseType)
        {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(g => g.GetTypes())
                .Where(t =>
                    t.IsInterface == false &&
                    t.IsAbstract == false &&
                    t.BaseType == baseType
                );
        }
    }
}
