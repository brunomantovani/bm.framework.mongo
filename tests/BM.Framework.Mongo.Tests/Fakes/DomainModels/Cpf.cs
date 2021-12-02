using System;
using System.Collections.Generic;

namespace BM.Framework.Mongo.Tests.Fakes.DomainModels
{
    public class Cpf
        : Document
    {
        public const string Format = @"000\.000\.000\-00";

        private Cpf() { }

        public Cpf(string value)
            : base(value)
        {
        }

        public override string ToString()
        {
            return Convert.ToUInt64(RawValue).ToString(Format);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return RawValue;
        }
    }
}
