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
}
