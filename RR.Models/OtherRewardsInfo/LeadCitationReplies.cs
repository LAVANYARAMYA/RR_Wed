using RR.Models.Rewards_Campaigns;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RR.Models.OtherRewardsInfo
{
     public class LeadCitationReplies
    {
        public int Id { get; set; }

        public int CampaignId { get; set; }

        public string NominatorId { get; set; }

        public string ReplierId { get; set; }

        public string ReplyCitation { get; set; }

        [ForeignKey("LeadCitationId")]
        public int LeadCitationId { get; set; }
        [ForeignKey("CampaignId")]
        public Campaigns? Campaigns { get; set; }
    }
}
