using RR.Models.Rewards_Campaigns;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RR.Models.OtherRewardsInfo
{
    public class LeadCitation
    {
        public int Id { get; set; }

        public string NominatorId { get; set; }

        public string Citation { get; set; }

        [ForeignKey("CampaignId")]
        public Campaigns? Campaigns { get; set; }

        public virtual ICollection<LeadCitationReplies> LeadCitationReplies { get; set; }= new HashSet<LeadCitationReplies>();
    }
}
