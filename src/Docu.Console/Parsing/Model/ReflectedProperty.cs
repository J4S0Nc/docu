using System;
using System.Reflection;

namespace Docu.Parsing.Model
{
    public class ReflectedProperty : DocumentedProperty
    {
        public ReflectedProperty(Identifier name, PropertyInfo property, Type targetType, bool isStatic)
            : base(name, null, property, targetType)
        {
            DeclaringName = property.DeclaringType != targetType
                ? IdentifierFor.Property(property, property.DeclaringType, isStatic )
                : name;
        }

        public Identifier DeclaringName { get; private set; }

        public bool Match(Identifier name)
        {
            return Name.Equals(name);
        }
    }
}
