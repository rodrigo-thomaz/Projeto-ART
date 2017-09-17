using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace RThomaz.Web.Helpers
{
    public static class EnumHelper
    {
        public static Dictionary<string, string> ConvertEnumToSelect2List<T>() where T : struct
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an Enum type.");
            }

            var results = new Dictionary<string, string>();

            FieldInfo[] fieldInfos = typeof(T).GetFields();

            foreach (var field in fieldInfos)
            {
                if (field.Name.Equals("value__")) continue;

                var value = field.GetRawConstantValue().ToString();
                var description = GetDescription((Enum)field.GetValue(field));

                results.Add(value, description);
            }

            return results;
        }

        public static string GetDescription(this Enum value)
        {
            var type = value.GetType();

            var fi = type.GetField(value.ToString());

            var descriptions = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            return descriptions.Length > 0 ? descriptions[0].Description : value.ToString();
        }
    }
}