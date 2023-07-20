using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RR.Models.Rewards_Campaigns
{
    public class Campaigns
    {
        public int Id { get; set; }

        public string CampaignName { get; set; }

        public string StartDate { get; set; }

        // voting
        public string votingDate { get; set; }

        public string EndDate { get; set; }

        public int RewardId { get; set; }

        public RewardType? RewardTypes { get; set; }

      //  public int softdelete { get;set; }
    }
}
