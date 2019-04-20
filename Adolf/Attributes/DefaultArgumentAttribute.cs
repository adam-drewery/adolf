using System;

namespace Adolf.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DefaultArgumentAttribute : Attribute
    {
        public int Ordinal { get; }

        public DefaultArgumentAttribute() { }

        public DefaultArgumentAttribute(int ordinal) => Ordinal = ordinal;
    }
}