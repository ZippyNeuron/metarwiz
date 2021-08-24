using System;
using System.ComponentModel;
using System.Reflection;

namespace ZippyNeuron.Metarwiz.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            if (field != null)
            {
                return field
                    .GetCustomAttribute<DescriptionAttribute>()
                    .Description;
            } else
            {
                return String.Empty;
            }            
        }
    }
}
