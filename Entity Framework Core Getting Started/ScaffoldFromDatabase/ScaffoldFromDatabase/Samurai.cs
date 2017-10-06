using System;
using System.Collections.Generic;

namespace ScaffoldFromDatabase
{
    public partial class Samurai
    {
        public Samurai()
        {
            Quotes = new HashSet<Quote>();
            SamuraiBattle = new HashSet<SamuraiBattle>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public SecretIdentity SecretIdentity { get; set; }
        public ICollection<Quote> Quotes { get; set; }
        public ICollection<SamuraiBattle> SamuraiBattle { get; set; }
    }
}
