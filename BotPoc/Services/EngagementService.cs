using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BotPoc.Services
{
    public interface IEngagementService
    {
        Task<List<Engagement>> GetEngagements();
    }

    public class EngagementService : IEngagementService
    {
        public Task<List<Engagement>> GetEngagements()
        {
            var engagements = new List<Engagement>() {
                new Engagement() { Type = EngagementType.Payments },
                new Engagement() { Type = EngagementType.Betalkoll },
                new Engagement() { Type = EngagementType.Cards }
            };

            return Task.FromResult(engagements);
        }
    }
}