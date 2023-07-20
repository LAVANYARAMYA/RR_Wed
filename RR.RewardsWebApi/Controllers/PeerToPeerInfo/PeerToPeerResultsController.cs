using Microsoft.AspNetCore.Mvc;
using RR.DataBaseConnect;
using RR.Models.PeerToPeerInfo;

namespace RR.RewardsWebApi.Controllers.PeerToPeerInfo
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeerToPeerResultsController:ControllerBase
    {
        private readonly DataBaseAccess dataBaseAccess;
        public PeerToPeerResultsController(DataBaseAccess dataBaseAccess) { this.dataBaseAccess = dataBaseAccess; }

        [HttpGet]
        [Route("{requiredNoOfWinners}/{campId}")]

        public async Task<ActionResult<IEnumerable<PeerToPeerResults>>> GetResult([FromRoute] int requiredNoOfWinners,int campId)
        {

            var arr = dataBaseAccess.PeerToPeerResults.Where(x=>x.campaigns.Id==campId).GroupBy(p => p.NomineeId);

            var result = (from l in arr select new { Id = l.Key, CountOfNominations = l.ToList().Count() })
                .OrderByDescending(x => x.CountOfNominations).Take(requiredNoOfWinners);





            return Ok(result);


        }
    }
}
