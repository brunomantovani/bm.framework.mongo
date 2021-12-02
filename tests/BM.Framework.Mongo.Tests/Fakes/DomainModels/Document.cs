using BM.Framework.Mongo.Tests.Fakes.Common;

namespace BM.Framework.Mongo.Tests.Fakes.DomainModels
{
    public abstract class Document
        : ValueObject
    {
        protected Document()
            : base()
        {
        }

        protected Document(
            string value)
            : this()
        {
            RawValue = value.ToNumeric();
        }
        public string RawValue { get; protected set; }

        public override string ToString()
        {
            return RawValue;
        }
    }
}
