using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RR.DataBaseConnect;
using RR.Models.Rewards_Campaigns;

namespace RR.RewardsWebApi.Controllers.Rewards_Campaigns
{
    [Route("api/[controller]")]
    [ApiController]
    public class RewardsController:ControllerBase
    {
        private readonly DataBaseAccess dataBaseAccess;

        public RewardsController(DataBaseAccess dataBaseAccess)
        {
            this.dataBaseAccess = dataBaseAccess;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RewardType>>> GetReward()
        {

            return await dataBaseAccess.RewardType.ToListAsync();
        }


        [HttpPost]
        public async Task<ActionResult<RewardType>> AddReward(RewardType rewardType)
        {
            await dataBaseAccess.RewardType.AddAsync(rewardType);

            await dataBaseAccess.SaveChangesAsync();

            return Ok(rewardType);
        }
    }
}
