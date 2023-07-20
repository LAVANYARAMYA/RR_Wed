using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RR.DataBaseConnect;
using RR.Models.OtherRewardsInfo;
using RR.Services;

namespace RR.RewardsWebApi.Controllers.OtherRewardsInfo
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeadCitationController:ControllerBase
    {
         private readonly DataBaseAccess dataBaseAccess;
        public OtherRewardsServices OtherRewardsServices;
        public LeadCitationController(DataBaseAccess dataBaseAccess) 
        { 
            OtherRewardsServices = new OtherRewardsServices(dataBaseAccess);
            this.dataBaseAccess = dataBaseAccess;
        }


        [HttpGet]

        public async Task<ActionResult<IEnumerable<LeadCitation>>> GetLeadCitation()
        {
             var result= await dataBaseAccess.LeadCitation.Include(r=>r.LeadCitationReplies).ToListAsync();

            return Ok(result.Select(x => new
            {
                NominatorId = x.NominatorId,

                Replies = x.LeadCitationReplies.Select(y => new
                {
                    ReplierId = y.ReplierId,
                    Comment = y.ReplyCitation
                })
            })) ;
            /*var query = (from a in dataBaseAccess.LeadCitation
                         join b in dataBaseAccess.LeadCitationReplies on a.NominatorId equals b.NominatorId
                         where b.CampaignId == a.Campaigns.Id

                         select new { NominatorId = a.NominatorId, ReplierId = b.ReplierId, Comment = b.ReplyCitation }).ToList();
            return Ok(query);*/

           /* var result = await OtherRewardsServices.getCitationWithReplies();

            return Ok(result);
*/


            /* return Ok(result.Value.Select(x => new
             {
                 LeadId = x.NominatorId,

                 LeadCitation = x.Citation,

                 LeadReplies = x.LeadCitationReplies.Select(r => new
                 {
                     ReplierID = r.ReplierId,
                     AdditionalCitation = r.ReplyCitation

                 })

             }));*/
        }

        [HttpGet]
        [Route("{NominatorId}/{CampaignId}")]
        public async Task<ActionResult<IEnumerable<LeadCitation>>> Get([FromRoute] string NominatorId, int CampaignId)
        {
            /*var query = (from a in dataBaseAccess.LeadCitation
                         join b in dataBaseAccess.LeadCitationReplies on a.Id equals b.LeadCitation.Id
                         where b.NominatorId == NominatorId && b.CampaignId == CampaignId

                         select new { NominatorId = a.NominatorId, ReplierId = b.ReplierId, Comment = b.ReplyCitation }).ToList();

            return Ok(query);*/

            var result= await dataBaseAccess.LeadCitation.Include(x=>x.LeadCitationReplies)
                .Where(x=>x.NominatorId.Equals(NominatorId)&&x.Campaigns.Id==CampaignId).ToListAsync();

            return Ok(result.Select(x => new
            {
                NominatorId = x.NominatorId,

                Replies = x.LeadCitationReplies.Select(y => new
                {
                    ReplierId = y.ReplierId,
                    Reply = y.ReplyCitation
                })
            })) ;
             


        }
    }
}
