using System;
using System.Collections.Generic;
using System.Reflection;
using Docu.Parsing.Model;

namespace Docu.Parsing
{
    public static class DocumentableMemberFinder
    {
        public static IEnumerable<IDocumentationMember> ReflectMembersForDocumenting(IEnumerable<Type> types)
        {
            foreach (var type in types)
            {
                if (type.IsSpecialName) continue;
                if (type.Name.StartsWith("__")) continue; // probably a lambda generated class

                yield return new ReflectedType(IdentifierFor.Type(type), type);

                foreach (var method in type.GetMethods())
                {
                    if (method.IsSpecialName || (method.DeclaringType != null && method.DeclaringType.Name.Equals("Object")))
                        continue; //skip object base methods and special names

                    yield return new ReflectedMethod(IdentifierFor.Method(method, type), method, type);
                }

                foreach (var constructor in type.GetConstructors())
                {
                    yield return new ReflectedMethod(IdentifierFor.Method(constructor, type), constructor, type);
                }

                foreach (var property in type.GetProperties(BindingFlags.Static | BindingFlags.Public))
                {
                    yield return new ReflectedProperty(IdentifierFor.Property(property, type, true), property, type, true);
                }
                foreach (var property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
                {
                    yield return new ReflectedProperty(IdentifierFor.Property(property, type, false), property, type, false);
                }

                foreach (var ev in type.GetEvents())
                {
                    yield return new ReflectedEvent(IdentifierFor.Event(ev, type), ev, type);
                }

                if (type.IsEnum)
                {
                    foreach (var member in type.GetMembers(BindingFlags.Static | BindingFlags.Public))
                    {
                        yield return new ReflectedEnum(IdentifierFor.Enum(member, type), member, type);

                    }
                }
                else
                {
                    foreach (var field in type.GetFields())
                    {
                        yield return new ReflectedField(IdentifierFor.Field(field, type), field, type);
                    }
                }
            }
        }
    }
}