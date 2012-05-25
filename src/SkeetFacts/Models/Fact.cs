using System;

namespace SkeetFacts.Models
{
    public class Fact
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public DateTime Created { get; set; }
    }
}