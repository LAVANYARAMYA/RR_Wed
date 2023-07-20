using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RR.Services.RequestClasses
{
    public class UpdateVote
    {
        public int voteId {  get; set; }
        public int idOfNomination { get; set; }

      //  public int RewardId { get; set; }

     //   public int CampaignId { get; set; }

        public string VoterId { get; set; }

     //   public string NominatorId { get; set; }

    //    public string NomineeId { get; set; }

    //    public string AwardCategory { get; set; }

        public int Stars { get; set; }
    }
}
