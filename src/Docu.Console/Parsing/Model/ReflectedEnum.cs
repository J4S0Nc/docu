using System;
using System.Reflection;

namespace Docu.Parsing.Model
{
    public class ReflectedEnum : DocumentedEnum
    {
        public ReflectedEnum(EnumIdentifier name, MemberInfo mi, Type targetType)
            : base(name, null, mi, targetType)
        {
            DeclaringName = mi.DeclaringType != targetType
                ? IdentifierFor.Enum(mi, mi.DeclaringType)
                : name;
        }

        public Identifier DeclaringName { get; private set; }

        public bool Match(Identifier name)
        {
            return Name.Equals(name);
        }
    }
}
