using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices
{
    [ToString, Equals(DoNotAddEqualityOperators = true)]
    public class GuidAndEmail
    {
        public GuidAndEmail(Guid id, string email)
        {
            Id = id;
            Email = email;
        }
        public Guid Id { get; set; }
        public string Email { get; set; }

    }
}
