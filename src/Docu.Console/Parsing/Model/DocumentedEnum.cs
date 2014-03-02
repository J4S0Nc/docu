using System;
using System.Diagnostics;
using System.Reflection;
using System.Xml;

namespace Docu.Parsing.Model
{
    [DebuggerDisplay("Enum {Name.Name,nq} for {TargetType.FullName,nq}")]
    public class DocumentedEnum : IDocumentationMember
    {
        public DocumentedEnum(Identifier name, XmlNode xml, MemberInfo mi, Type targetType)
        {
            Name = name;
            Xml = xml;
            Member = mi;
            TargetType = targetType;
            try
            {
                Value = (int)targetType.GetField(mi.Name).GetValue(null);
            }
            catch (Exception e)
            {
                Value = (uint)targetType.GetField(mi.Name).GetValue(null);
            }
        }
        public object Value { get; set; }
        public MemberInfo Member { get; private set; }

        public Identifier Name { get; private set; }
        public Type TargetType { get; private set; }
        public XmlNode Xml { get; private set; }
    }
}

