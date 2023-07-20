using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RR.DataBaseConnect;
using RR.Models.OtherRewardsInfo;
using RR.Services;
using RR.Services.RequestClasses;
using System.Linq;

namespace RR.RewardsWebApi.Controllers.OtherRewardsInfo
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtherRewardsResultsController:ControllerBase
    {
        private readonly DataBaseAccess dataBaseAccess;

        public OtherRewardsServices OtherRewardsServices { get; set; }
        public OtherRewardsResultsController(DataBaseAccess dataBaseAccess) 
        {   
            this.dataBaseAccess = dataBaseAccess;

            OtherRewardsServices = new OtherRewardsServices(dataBaseAccess);
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<OtherRewardResults>>> Get()
        {

            return await dataBaseAccess.OtherRewardResults.Include(x=>x.OtherRewards).Include(x=>x.Employee).Include(x=>x.Campaigns).ToListAsync();

        }
        [HttpGet]
        [Route("result/{CampaignId:int}/{Count:int}")]

        public async Task<ActionResult<IEnumerable<OtherRewardResults>>> GetResult([FromRoute] int CampaignId, int Count)
        {



            /*var results = (from l in dataBaseAccess.OtherRewardResults
                           where l.CampaignId == CampaignId
                           group l by l.NomineeId into g
                           select new
                           {
                               EmployeeId = g.First().NomineeId,
                               Stars = g.Sum(s => s.Stars) / g.ToList().Count(),
                               Award = g.First().AwardCategory
                           }).Take(Count);*/
           
            var arr = dataBaseAccess.OtherRewardResults.Where(x => x.Campaigns.Id == CampaignId).
                GroupBy(x => x.NomineeId).Select(s => new
                {
                    NomineeId = s.First().NomineeId,
                    Stars = s.Sum(sum => sum.Stars) / s.ToList().Count(),
                    AwardC=s.First().AwardCategory

                }) ;
            return Ok(arr);


        }

        [HttpPost]
        [Route("Vote")]
        public async Task<ActionResult<OtherRewardResults>> addVote(RequestVote requestVote)
        {
            var result = await OtherRewardsServices.addVote(requestVote);

            return Ok(new
            {
                NomineeId = result.Value.NomineeId,
                NominatorId = result.Value.NominatorId,
                VoterId = result.Value.VoterId,
                Status = "Voted"
            });
        }

        [HttpPut]
        [Route("UpdateVote")]

        public async Task<ActionResult<OtherRewardResults>> updateVote(UpdateVote updateVote)
        {
            var result = await OtherRewardsServices.updateVote(updateVote);

            if(result==null)
            {
                return BadRequest("Not Valid Yaar");
            }

            return Ok(new { NomineeId=result.Value.NomineeId, NominatorId=result.Value.NominatorId,
                VoterId=result.Value.VoterId, Status = "Updated" });
        }
    }
}
