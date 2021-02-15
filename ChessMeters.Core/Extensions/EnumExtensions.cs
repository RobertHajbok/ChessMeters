using ChessMeters.Core.Attributes;
using System.ComponentModel;
using System.Linq;

namespace ChessMeters.Core.Extensions
{
    public static class EnumExtensions
    {
        public static string GetEnumDescription<TEnum>(this TEnum item) => item.GetType()
            .GetField(item.ToString())
            .GetCustomAttributes(typeof(DescriptionAttribute), false)
            .Cast<DescriptionAttribute>()
            .FirstOrDefault()?.Description;

        public static string GetEnumDisplayUI<TEnum>(this TEnum item) => item.GetType()
            .GetField(item.ToString())
            .GetCustomAttributes(typeof(EnumDisplayAttribute), false)
            .Cast<EnumDisplayAttribute>()
            .FirstOrDefault()?.UI;
    }
}
