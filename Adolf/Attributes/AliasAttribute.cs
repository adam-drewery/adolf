using System;

namespace Adolf.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class AliasAttribute : Attribute
    {
        public AliasAttribute(string name) => Value = name;

        public string Value { get; }
    }
}