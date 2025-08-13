using System;

namespace Domain
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class IdentifierAttribute : Attribute
    {
        public string Name { get; private set; }

        public IdentifierAttribute(string name)
        {
            this.Name = name;
        }
    }
}
