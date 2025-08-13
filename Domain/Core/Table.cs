using System;

namespace Domain
{
    public class TableAttribute : Attribute
    {

        public string Name { get; private set; }

        public TableAttribute(string name)
        {
            this.Name = name;
        }
    }
}
