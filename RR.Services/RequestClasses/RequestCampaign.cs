using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RR.Services.RequestClasses
{
    public class RequestCampaign
    {
        public string CampaignName { get; set; }

        public string StartDate { get; set; }

        public string VotingDate { get; set; }
        public string EndDate { get; set; }

        public int RewardId { get; set; }
    }
}
