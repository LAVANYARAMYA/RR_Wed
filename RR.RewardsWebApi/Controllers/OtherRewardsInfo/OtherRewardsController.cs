using Microsoft.AspNetCore.Mvc;
using RR.DataBaseConnect;
using RR.Models.OtherRewardsInfo;
using RR.Services;
using RR.Services.RequestClasses;

namespace RR.RewardsWebApi.Controllers.OtherRewardsInfo
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtherRewardsController : ControllerBase
    {

        private readonly DataBaseAccess dataBaseAccess;

        public OtherRewardsServices OtherRewardsServices;
        public OtherRewardsController(DataBaseAccess dataBaseAccess)
        {
            this.dataBaseAccess = dataBaseAccess;
            OtherRewardsServices = new OtherRewardsServices(dataBaseAccess);
        }



        // GET: api/<MonthlyRewardsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OtherRewards>>> GetRewards()
        {
            var result = await OtherRewardsServices.GetAllNominations();

            return Ok(result.Value);

        }
        [HttpGet]
        [Route("GetById/{NominatorId}/{campaignId:int}")]

        public async Task<ActionResult<OtherRewards>> getNominationsById([FromRoute] string NominatorId, int campaignId)
        {
            var result = await OtherRewardsServices.GetNominationById(NominatorId, campaignId);

            if (result == null)
            {
                return BadRequest("Not yet Nominated");
            }

            return Ok(result.Value);
        }
        [HttpGet]
        [Route("GetByCamp/{CampaignId}/{RewardId}")]
        public async Task<ActionResult<IEnumerable<OtherRewards>>> getNominationByCampaignId([FromRoute]  int CampaignId, int RewardId)
        {


            var res= await OtherRewardsServices.GetAllNominationByCampId(CampaignId, RewardId);

            return Ok(res.Value.Select(x => new
            {
                Campaigns = x.Campaigns.CampaignName,
                CampaignId = x.CampaignId,
                RewardType = x.Campaigns.RewardTypes.RewardTypes,
                startDate = x.Campaigns.StartDate,
                endDate = x.Campaigns.EndDate,
                NomainatorId = x.NominatorId,
                NominatorName = dataBaseAccess.Employee.FirstOrDefault(e => e.EmployeeId.Equals(x.NominatorId)).Name,
                NomineeName = x.Employee.Name,
                NomineeId = x.NomineeId,
                Designation = x.Employee.Designation,
                Month = x.Month,
                AwardCategory = x.AwardCategory,
                CitationId=x.LeadCitation.Id,
                Citation = x.LeadCitation.Citation,
                Reply = x.LeadCitation.LeadCitationReplies.Select(y => new
                {
                    ReplierId = y.ReplierId,
                    Comments = y.ReplyCitation
                })






            })) ;

           /* return Ok(res.Value);*/
        }
            

            

        // POST api/<MonthlyRewardsController>
        [HttpPost]
        [Route("AddNomination")]
        public async Task<ActionResult<OtherRewards>> AddNomination(RequestNomination requestOtherRewards)
        {
            var result = await OtherRewardsServices.addNomination(requestOtherRewards);

            return Ok(result.Value);

        }

        // PUT api/<MonthlyRewardsController>/5
        [HttpPut]
        [Route("UpdateNomination")]
        public async Task<ActionResult<OtherRewards>> update(UpdateNomination updateNomination)
        {
            var result = await OtherRewardsServices.updateNomination(updateNomination);

            if (result == null)
            {
                return BadRequest("Not Found");
            }
            else
            {
                return Ok(result.Value);
            }
        }
    }
}
