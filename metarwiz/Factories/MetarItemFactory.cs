﻿using System;
using System.Collections.Generic;
using ZippyNeuron.Metarwiz.Abstractions;

namespace ZippyNeuron.Metarwiz.Factories
{
    public class MetarItemFactory : MetarFactoryBase, IMetarItemFactory
    {
        internal MetarItemFactory() { }

        public IMetarItem Create(int position, string item)
        {
            IEnumerable<Type> types = GetTypesOfBase(typeof(MwMetarItem));

            foreach (Type t in types)
            {
                if (IsMatch(t, position, item))
                {
                    return CreateMetarItem(t, position, item);
                }
            }

            return null;
        }
    }
}
