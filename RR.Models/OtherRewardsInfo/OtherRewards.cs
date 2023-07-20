using RR.Models.EmployeeInfo;
using RR.Models.Rewards_Campaigns;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RR.Models.OtherRewardsInfo
{
    public class OtherRewards
    {
        public int Id { get; set; }

        public int RewardId { get; set; }

        public int CampaignId { get; set; }

        public string NominatorId { get; set; }

        public string NomineeId { get; set; }

        public string AwardCategory { get; set; }

     

        public int Month { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }

        [ForeignKey("IdOFCampaign")]
        public Campaigns Campaigns { get; set; }

        [ForeignKey("IdOfCitation")]
        public LeadCitation? LeadCitation { get; set; }

       
    }
}
