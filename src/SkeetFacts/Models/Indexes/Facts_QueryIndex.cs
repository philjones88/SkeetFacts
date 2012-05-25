using System.Linq;
using Raven.Client.Indexes;

namespace SkeetFacts.Models.Indexes
{
    public class Facts_QueryIndex : AbstractIndexCreationTask<Fact>
    {
        public Facts_QueryIndex()
        {
            Map = facts => from fact in facts
                           select new
                                      {
                                          fact.Id,
                                          fact.Created
                                      };
        }
    }
}