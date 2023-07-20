using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RR.DataBaseConnect;
using RR.Models.Rewards_Campaigns;
using RR.Services;
using RR.Services.RequestClasses;

namespace RR.RewardsWebApi.Controllers.Rewards_Campaigns
{
    [Route("api/[controller]")]
    [ApiController]
    public class AwardController : ControllerBase
    {

       public CampaignServices CampaignServices { get; set; }

        public AwardController(DataBaseAccess dataBaseAccess)
        {
            CampaignServices = new CampaignServices(dataBaseAccess);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AwardCategory>>> Get()
        {
            return await CampaignServices.getAwardsCategory();
        }

        [HttpPost]
        public async Task<ActionResult<AwardCategory>> add(RequestAward requestAward)
        {
            return await CampaignServices.addAwardCategory(requestAward);
        }

    }
}
