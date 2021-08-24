using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using ZippyNeuron.Metarwiz.Abstractions;

namespace ZippyNeuron.Metarwiz.Factories
{
    public class MetarItemFactory
    {
        internal static IMetarItem Create(int position, string metarItem)
        {
            IEnumerable<Type> types = GetIMetarItemTypes();

            foreach (Type t in types)
            {
                if (IsMatch(t, position, metarItem))
                {
                    return CreateMetarItem(t, position, metarItem);
                }
            }

            return null;
        }

        private static IMetarItem CreateMetarItem(Type t, int position, string item)
        {
            return (IMetarItem)Activator.CreateInstance(t, new object[] { position, item });
        }

        private static bool IsMatch(Type type, int position, string item)
        {
            return (bool)type
                .GetMethod("IsMatch", BindingFlags.Static | BindingFlags.Public)
                .Invoke(null, new object[] { position, item });
        }

        private static IEnumerable<Type> GetIMetarItemTypes()
        {
            var type = typeof(IMetarItem);

            return AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(g => g.GetTypes())
                .Where(t =>
                    type.IsAssignableFrom(t) &&
                    t.IsInterface == false &&
                    t.IsAbstract == false
                );
        }
    }
}
