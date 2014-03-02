using System;

namespace Docu.Parsing.Model
{
    public sealed class EnumIdentifier : Identifier, IEquatable<EnumIdentifier>, IComparable<EnumIdentifier>
    {
        private readonly TypeIdentifier _typeId;

        public EnumIdentifier(string name, TypeIdentifier typeId)
            : base(name)
        {
            _typeId = typeId;
        }

        public override NamespaceIdentifier CloneAsNamespace()
        {
            return _typeId.CloneAsNamespace();
        }

        public override TypeIdentifier CloneAsType()
        {
            return _typeId.CloneAsType();
        }

        public override bool Equals(Identifier obj)
        {
            // no need for expensive GetType calls since the class is sealed.
            return Equals(obj as EnumIdentifier);
        }

        public bool Equals(EnumIdentifier other)
        {
            // no need for expensive GetType calls since the class is sealed.
            if(((object)other) == null)
            {
                return false;
            }

            return (Name == other.Name) && _typeId.Equals(other._typeId);
        }

        public override int CompareTo(Identifier other)
        {
            if(other is TypeIdentifier || other is NamespaceIdentifier
                || other is MethodIdentifier || other is PropertyIdentifier)
            {
                return 1;
            }

            return CompareTo(other as EnumIdentifier);
        }

        public int CompareTo(EnumIdentifier other)
        {
            if(((object)other) == null)
            {
                return -1;
            }

            int comparison = Name.CompareTo(other.Name);
            if(comparison != 0)
            {
                return comparison;
            }

            return _typeId.CompareTo(other._typeId);
        }
    }
}