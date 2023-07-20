using RR.Models.EmployeeInfo;
using RR.Models.Rewards_Campaigns;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RR.Models.PeerToPeerInfo
{
    public class PeerToPeerResults
    {
        public int Id { get; set; }

        public string? NomineeId { get; set; }
        public string? NominatorId { get; set; }

        public string? awardCategory { get; set; }

       
        public string? Citation { get; set; }

        [ForeignKey("IdOfCampaign")]
        public Campaigns campaigns { get; set; }

        [ForeignKey("IdOfNominee")]
        public Employee? Employee { get; set; }
    }
}
