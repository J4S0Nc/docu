using System;
using System.Collections.Generic;
using Docu.Documentation.Comments;
using Docu.Parsing.Model;

namespace Docu.Documentation
{
    public class MethodParameter : BaseDocumentationElement, IReferrer
    {
        public MethodParameter(string name, bool hasDefault, object defualtValue, IReferencable reference)
            : base(new NullIdentifier(name))
        {
            Reference = reference;
            HasDefault = hasDefault;
            DefualtValue = defualtValue;
        }
        public object DefualtValue { get; set; }
        public bool HasDefault { get; set; }
        public string PrettyName
        {
            get
            {
                if (!HasDefault)
                    return Name;

                if (DefualtValue == null)
                    return Name + " = null";
                if (DefualtValue is string)
                    return Name + " = \"" + DefualtValue + "\"";
                return Name + " = " + DefualtValue;
            }
        }

        public IReferencable Reference { get; set; }
    }

    public sealed class NullIdentifier : Identifier, IEquatable<NullIdentifier>
    {
        public NullIdentifier(string name)
            : base(name)
        {}

        public override NamespaceIdentifier CloneAsNamespace()
        {
            return null;
        }

        public override TypeIdentifier CloneAsType()
        {
            return null;
        }

        public override bool Equals(Identifier obj)
        {
            // no need for expensive GetType calls since the class is sealed.
            return Equals(obj as NullIdentifier);
        }

        public bool Equals(NullIdentifier other)
        {
            // no need for expensive GetType calls since the class is sealed.
            if(((object)other) == null)
            {
                return false;
            }

            return (Name == other.Name);
        }

        public override int CompareTo(Identifier other)
        {
            return -1;
        }
    }
}