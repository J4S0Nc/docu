using System;
using System.Collections.Generic;
using Docu.Parsing.Comments;
using Docu.Parsing.Model;

namespace Docu.Documentation.Generators
{
    internal class EnumGenerator : BaseGenerator, IGenerator<DocumentedEnum>
    {
        readonly IDictionary<Identifier, IReferencable> _matchedAssociations;

        public EnumGenerator(IDictionary<Identifier, IReferencable> matchedAssociations, ICommentParser commentParser)
            : base(commentParser)
        {
            _matchedAssociations = matchedAssociations;
        }

        public void Add(List<Namespace> namespaces, DocumentedEnum association)
        {
            if (association.Member == null )
            { 
                return;
            }
            
            DeclaredType type = FindType(association, namespaces);
          
            var doc = Enumeration.Unresolved(
                IdentifierFor.Enum(association.Member, association.TargetType), type);
           
            //var evalue = Enum.Parse(association.Value.GetType(), association.Value.ToString());
            if (association.Value is uint)
            {
                doc.EnumValue ="0x" + ((uint) association.Value).ToString("X");
            }
            if (association.Value is int)
            {
                doc.EnumValue = ((int)association.Value).ToString();
            }

            ParseSummary(association, doc);
            ParseRemarks(association, doc);
            ParseExample(association, doc);

            _matchedAssociations[association.Name] = doc;
            type.AddEnum(doc);
        }
    }
}
