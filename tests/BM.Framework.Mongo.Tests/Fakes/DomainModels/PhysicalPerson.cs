using System;
using BM.Framework.Mongo.Tests.Fakes.Common;

namespace BM.Framework.Mongo.Tests.Fakes.DomainModels
{
    public class PhysicalPerson
        : IAggregateRoot
    {
        private PhysicalPerson()
        {
        }

        public PhysicalPerson(
            Cpf cpf,
            FullName fullName,
            bool active = true)
        {
            Cpf = cpf;
            FullName = fullName;
            CreatedAt = DateTime.Now;
            Active = active;
        }

        public Cpf Cpf { get; private set; }
        public FullName FullName { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool Active { get; private set; }

        internal void ChangeFullName(
            FullName fullName)
        {
            FullName = fullName;
        }
    }
}
