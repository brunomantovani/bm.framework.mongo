using BM.Framework.Mongo.Tests.Fakes.Common;
using System.Collections.Generic;

namespace BM.Framework.Mongo.Tests.Fakes.DomainModels
{
    public class FullName
        : ValueObject
    {
        private FullName() { }

        public FullName(
            string fullName)
        {
            var names = fullName.Split(" ");
            FirstName = names[0];
            LastName = fullName.Replace($"{FirstName} ", string.Empty);
        }

        public FullName(
            string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}
