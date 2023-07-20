using Microsoft.AspNetCore.Mvc;
using RR.DataBaseConnect;
using RR.Models.PeerToPeerInfo;
using RR.Models.Rewards_Campaigns;
using RR.Services;
using RR.Services.RequestClasses;

namespace RR.RewardsWebApi.Controllers.PeerToPeerInfo
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeerToPeerController : ControllerBase
    {
        private readonly DataBaseAccess dataBaseAccess;
        public PeerToPeerServices PeerToPeerServices;
        public EmployeeServices EmployeeServices;
        public PeerToPeerController(DataBaseAccess dataBaseAccess)
        {
            PeerToPeerServices = new PeerToPeerServices(dataBaseAccess);
            EmployeeServices = new EmployeeServices(dataBaseAccess);
            this.dataBaseAccess = dataBaseAccess;
        }

        [HttpGet]
        [Route("{CampaignId:int}")]
        

        public async Task<ActionResult<IEnumerable<PeerToPeer>>> GetPeerToPeer([FromRoute] int CampaignId)
        {
           

            var res = await PeerToPeerServices.GetPeerToPeerAsync(CampaignId);
            // return Ok(res);


            return Ok(res.Value.Select(x => new
            {
                Campaigns = x.Campaigns.CampaignName,
                RewardType=x.Campaigns.RewardTypes.RewardTypes,
                startDate = x.Campaigns.StartDate,
                endDate = x.Campaigns.EndDate,
                NomainatorId = x.NominatorId,
                NominatorName = dataBaseAccess.Employee.FirstOrDefault(e => e.EmployeeId.Equals(x.NominatorId)).Name,
                NomineeName = x.Employee.Name,
                NomineeId = x.NomineeId,
                Designation = x.Employee.Designation,
                Month = x.Month,
                AwardCategory = x.AwardCategory,
                Citation = x.Citation,
               




            }));

        }
        [HttpGet]
        [Route("get/GetByNominatorId/{CampaignId:int}/{NominatorId}")]
        public async Task<ActionResult<PeerToPeer>> GetbyNominatorId(int CampaignId, string NominatorId)
        {
            var result = await PeerToPeerServices.GetByNominator(CampaignId, NominatorId);

            if (result == null)
            {
                return BadRequest("Not Yet Nominated");
            }
            return Ok(new { NominatorId = result.Value.NominatorId, NomineeId = result.Value.NomineeId });
        }



        [HttpPost]

        public async Task<ActionResult<PeerToPeer>> AddPeerToPeerNominees(RequestNomination requestPeerToPeer)
        {
            if(requestPeerToPeer.NominatorId.Equals(requestPeerToPeer.NomineeId))
            {
                return Ok("You cant vote yourself ");
            }


            var res = await PeerToPeerServices.AddPeerToPeerNominees(requestPeerToPeer);
            return Ok(new { res.Value.NominatorId , res.Value.NomineeId} );

        }

        /* public bool IsAvailableNominatorId(string NominatorId)
         {
             return (dataBaseAccess.PeerToPeer?.Any(x => x.NominatorId.Equals(NominatorId))).GetValueOrDefault();

         }*/

        [HttpPut]
        public async   Task<ActionResult<PeerToPeer>> EditNomination(RequestPeerToPeer requestPeerToPeer)
        {
            var result=await PeerToPeerServices.editNomination(requestPeerToPeer);

            if (result == null)
            {
                return BadRequest("Not nominated");
            }
            return Ok(result.Value.NominatorId+"Updated");

        }
    }
}
