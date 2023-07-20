using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RR.Services.RequestClasses
{
    public class RequestPeerToPeer
    {
        public int CampaignId { get; set; }
        public string NominatorId { get; set; }
        public string NomineeId { get; set; }


        
        public string? AwardCategory { get; set; }

        public int? Month { get; set; }

        public string? Citation { get; set; }
    }
}
