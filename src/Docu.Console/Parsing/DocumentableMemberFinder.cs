using System;
using System.Collections.Generic;
using System.Reflection;
using Docu.Parsing.Model;

namespace Docu.Parsing
{
    public class DocumentableMemberFinder : IDocumentableMemberFinder
    {
        public IEnumerable<IDocumentationMember> GetMembersForDocumenting(IEnumerable<Type> types)
        {
            foreach (var type in types)
            {
                if (type.IsSpecialName) continue;
                if (type.Name.StartsWith("__")) continue; // probably a lambda generated class

                yield return new UndocumentedType(Identifier.FromType(type), type);

                foreach (var method in type.GetMethods())
                {
                    if (method.IsSpecialName || method.DeclaringType.Name.Equals("Object"))
                        continue;

                    yield return new UndocumentedMethod(Identifier.FromMethod(method, type), method, type);
                }

                foreach (var property in type.GetProperties(BindingFlags.Static | BindingFlags.Public))
                {
                    yield return new UndocumentedProperty(Identifier.FromProperty(property, type, true), property, type);
                }
                foreach (var property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
                {
                    yield return new UndocumentedProperty(Identifier.FromProperty(property, type, false), property, type);
                }
                foreach (var ev in type.GetEvents())
                {
                    yield return new UndocumentedEvent(Identifier.FromEvent(ev, type), ev, type);
                }

                foreach (var field in type.GetFields())
                {
                    yield return new UndocumentedField(Identifier.FromField(field, type), field, type);
                }
            }
        }
    }
}