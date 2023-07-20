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
    public class OtherRewardResults
    {
        public int Id { get; set; }

        
        
        public int RewardId { get; set; }

        public int CampaignId { get; set; }

        public string VoterId { get; set; }

        public string NominatorId { get; set; }

        public string NomineeId { get; set; }

        public string AwardCategory { get; set; }

        public int Stars { get; set; }

        public string CampaignName { get; set; }

        [ForeignKey("NomineeEmpId")]
        public Employee? Employee { get; set; }
        [ForeignKey("CampId")]
        public Campaigns? Campaigns { get; set; }

        [ForeignKey("IfOfNomination")]
        public OtherRewards? OtherRewards { get; set; }

    }
}
