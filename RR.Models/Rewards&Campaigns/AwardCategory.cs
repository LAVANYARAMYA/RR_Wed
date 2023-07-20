using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RR.Models.Rewards_Campaigns
{
    public  class AwardCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [DisplayName("DisplayName")]
        public int IdOfReward { get; set; }

        [ForeignKey("RewardId")]
        public RewardType? RewardType { get; set; }
    }
}
