using RR.Models.EmployeeInfo;
using RR.Models.PeerToPeerInfo;
using RR.Models.Rewards_Campaigns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RR.Models.PeerToPeerInfo
{
    public class PeerToPeer
    {
        [Key]
        public int Id { get; set; }
        public string NominatorId { get; set; }

        public string? AwardCategory { get; set; }

        public int? Month { get; set; }

        public string? Citation { get; set; }


        [DisplayName("IdOfNominee")]
        public string? NomineeId { get; set; }

        [DisplayName("IdOfCampaignAdded")]
        public int CampaignId { get; set; }


        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }


        [ForeignKey("CampaignId")]
        public Campaigns? Campaigns { get; set; }


        [ForeignKey("PeerToPeerResultsId")]
        public PeerToPeerResults? PeerToPeerResults { get; set; }
    }
}
