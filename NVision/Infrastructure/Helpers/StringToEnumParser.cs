using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Infrastructure.Helpers
{
    public static class StringToEnumParser<T> where T : Enum 
    {
        public static T ToEnum(string str)
        {
            var enumType = typeof(T);
            foreach (var name in Enum.GetNames(enumType))
            {
                var enumMemberAttribute = ((EnumMemberAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), true)).Single();
                if (enumMemberAttribute.Value == str) return (T)Enum.Parse(enumType, name);
            }

            return default(T);
        }
    }
}
