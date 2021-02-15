using System;

namespace ChessMeters.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class EnumDisplayAttribute : Attribute
    {
        public string UI { get; set; }
    }
}
