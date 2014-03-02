using System;
using System.Collections.Generic;
using Docu.Parsing.Model;

namespace Docu.Documentation
{
    public class Enumeration : BaseDocumentationElement, IReferencable
    {
        public Enumeration(EnumIdentifier identifier, DeclaredType type)
            : base(identifier)
        {
            Type = type;
            
        }

        public IReferencable ReturnType { get; set; }

        public DeclaredType Type { get; set; }

        public string EnumValue { get;  set; }

        public string FullName
        {
            get { return Name; }
        }

        public string PrettyName
        {
            get { return Name; }
        }

        public void Resolve(IDictionary<Identifier, IReferencable> referencables)
        {
            if (referencables.ContainsKey(identifier))
            {
                IsResolved = true;
                IReferencable referencable = referencables[identifier];
                var en = referencable as Enumeration;

                if (en == null)
                {
                    throw new InvalidOperationException("Cannot resolve to '" + referencable.GetType().FullName + "'");
                }
                
                ReturnType = en.ReturnType;

                if (ReturnType != null && !ReturnType.IsResolved)
                {
                    ReturnType.Resolve(referencables);
                }

                if (!Summary.IsResolved)
                {
                    Summary.Resolve(referencables);
                }

                if (!Remarks.IsResolved)
                {
                    Remarks.Resolve(referencables);
                }
            }
            else
            {
                ConvertToExternalReference();
            }
        }

        public static Enumeration Unresolved(EnumIdentifier fieldIdentifier, DeclaredType type)
        {
            return new Enumeration(fieldIdentifier, type) { IsResolved = false };
        }

        public static Enumeration Unresolved(EnumIdentifier fieldIdentifier, DeclaredType type, IReferencable returnType)
        {
            return new Enumeration(fieldIdentifier, type) { IsResolved = false, ReturnType = returnType };
        }
    }
}
