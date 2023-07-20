using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RR.Services.RequestClasses
{
    public class RequestLeadCitationReplies
    {
        public int leadCitationId { get; set; }
        public int CampaignId { get; set; }

        public string NominatorId { get; set; }

        public string ReplierId { get; set; }

        public string ReplyCitation { get; set; }
    }
}
