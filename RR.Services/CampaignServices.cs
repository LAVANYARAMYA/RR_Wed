using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RR.DataBaseConnect;
using RR.Models.OtherRewardsInfo;
using RR.Models.PeerToPeerInfo;
using RR.Models.Rewards_Campaigns;
using RR.Services.RequestClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RR.Services
{
    public class CampaignServices
    {
        private readonly DataBaseAccess dataBaseAccess;
        public PeerToPeerServices PeerToPeerServices { get; set; }
        public CampaignServices()
        {

        }

        public CampaignServices(DataBaseAccess dataBaseAccess)
        {
            this.dataBaseAccess = dataBaseAccess;
            this.PeerToPeerServices = new PeerToPeerServices();
        }
        public async Task<ActionResult<IEnumerable<Campaigns>>> GetCampaign()
        {
            var result = await dataBaseAccess.Campaigns.Include(x=>x.RewardTypes).ToListAsync();

          //  result.ForEach(x => x.RewardTypes = dataBaseAccess.RewardType.FirstOrDefault(y => y.Id == x.RewardId));

            


            return result;
        }

        

        public async Task<ActionResult<Campaigns>> AddCampaign(RequestCampaign requestCampaign)
        {
            Campaigns campaign = new Campaigns();

            campaign.StartDate = requestCampaign.StartDate;
            campaign.votingDate = requestCampaign.VotingDate;
            campaign.EndDate = requestCampaign.EndDate;
            campaign.CampaignName = requestCampaign.CampaignName;
            campaign.RewardId = requestCampaign.RewardId;

            RewardType rewardType =await dataBaseAccess.RewardType.FirstOrDefaultAsync(x => x.Id == requestCampaign.RewardId);
            campaign.RewardTypes = rewardType;

            await dataBaseAccess.Campaigns.AddAsync(campaign);

            await dataBaseAccess.SaveChangesAsync();

            return campaign;
        }
        public async Task<ActionResult<Campaigns>> updateCampaign(RequestUpdateCampaign requestUpdateCampaign)
        {
            Campaigns campaign = await dataBaseAccess.Campaigns.FirstOrDefaultAsync(x => x.Id == requestUpdateCampaign.CampaignId);

            campaign.StartDate = requestUpdateCampaign.StartDate;
            campaign.votingDate = requestUpdateCampaign.VotingDate;
            campaign.EndDate = requestUpdateCampaign.EndDate;
            campaign.CampaignName = requestUpdateCampaign.CampaignName;
            campaign.RewardId = requestUpdateCampaign.RewardId;

            RewardType rewardType = await dataBaseAccess.RewardType.FirstOrDefaultAsync(x => x.Id == requestUpdateCampaign.RewardId);
            campaign.RewardTypes = rewardType;

            dataBaseAccess.Campaigns.Update(campaign);
            await dataBaseAccess.SaveChangesAsync();

            return campaign;

        }


        // Add Award Category

        public async Task<ActionResult<AwardCategory>> addAwardCategory(RequestAward requestAward)
        {
            AwardCategory awardCategory = new AwardCategory();
            awardCategory.Name = requestAward.Name;
            awardCategory.IdOfReward = requestAward.IdOfReward;



            awardCategory.RewardType = await dataBaseAccess.RewardType.FirstOrDefaultAsync(x => x.Id == requestAward.IdOfReward);

            
            await dataBaseAccess.AwardCategory.AddAsync(awardCategory);

            await dataBaseAccess.SaveChangesAsync();

            return awardCategory;
        }

       

        public async Task<ActionResult<IEnumerable<AwardCategory>>>  getAwardsCategory()
        {
           return  await dataBaseAccess.AwardCategory.Include(x=>x.RewardType).ToListAsync();
        }

        public async Task<ActionResult<Campaigns>> deleteCampaign(int campId)
        {
            // Peer

            List<PeerToPeerResults> peerToPeerResults = await dataBaseAccess.PeerToPeerResults.Where(x => x.campaigns.Id == campId).ToListAsync();
            foreach (var peerToPeerResult in peerToPeerResults)
            {
                dataBaseAccess.PeerToPeerResults.Remove(peerToPeerResult);
            }
            await dataBaseAccess.SaveChangesAsync();

            List<PeerToPeer> peerToPeers = await dataBaseAccess.PeerToPeer.Include(x => x.PeerToPeerResults).Where(x => x.Campaigns.Id == campId).ToListAsync();
            foreach (var peerToPeer in peerToPeers)
            {
                dataBaseAccess.PeerToPeer.Remove(peerToPeer);



            }
            await dataBaseAccess.SaveChangesAsync();


            //Other REwards
            List<LeadCitationReplies> leadCitationReplies = await dataBaseAccess.LeadCitationReplies.
                Where(x => x.Campaigns.Id == campId).ToListAsync();



            //
            List<OtherRewardResults> otherRewardResults = await dataBaseAccess.OtherRewardResults.
               Where(x => x.Campaigns.Id == campId).ToListAsync();



            foreach (var otherRewardResult in otherRewardResults)
            {
                dataBaseAccess.OtherRewardResults.Remove(otherRewardResult);
            }
            await dataBaseAccess.SaveChangesAsync();
            //



            List<OtherRewards> otherReward = await dataBaseAccess.OtherRewards.Include(x => x.LeadCitation).
               Where(x => x.Campaigns.Id == campId).ToListAsync();



            foreach (var item in otherReward)
            {
                dataBaseAccess.OtherRewards.Remove(item);
            }
            await dataBaseAccess.SaveChangesAsync();



            //



            foreach (var leadCitationReply in leadCitationReplies)
            {
                dataBaseAccess.LeadCitationReplies.Remove(leadCitationReply);
            }
            await dataBaseAccess.SaveChangesAsync();



            List<LeadCitation> leadCitation = await dataBaseAccess.LeadCitation.
               Where(x => x.Campaigns.Id == campId).ToListAsync();



            foreach (var leadC in leadCitation)
            {
                dataBaseAccess.LeadCitation.Remove(leadC);
                await dataBaseAccess.SaveChangesAsync();
            }









            Campaigns campaigns = await dataBaseAccess.Campaigns.FindAsync(campId);
            dataBaseAccess.Campaigns.Remove(campaigns);



            await dataBaseAccess.SaveChangesAsync();



            return campaigns;


        }
        // Not Working
        public async Task<IActionResult> getCampaignDetailsByCampId(int campId ,int rewardId)
        {
            Campaigns campaign = await dataBaseAccess.Campaigns.Include(x=>x.RewardTypes).FirstOrDefaultAsync(x=>x.Id == campId);

            if (campaign == null)
            {
                return null;
            }
            if(campaign.RewardTypes.RewardTypes.ToLower().Equals("peertopeer"))
            {
                var result = await dataBaseAccess.PeerToPeer.Where(x => x.Campaigns.Id == campId).ToListAsync();

                result.ForEach(x =>
                {
                    x.Employee = dataBaseAccess.Employee.FirstOrDefault(emp => emp.EmployeeId.Equals(x.NomineeId));
                    x.Campaigns = dataBaseAccess.Campaigns.Include(x => x.RewardTypes).FirstOrDefault(x => x.Id == campId);

                });

                return (IActionResult)result;
            }
            else
            {
                var result = await dataBaseAccess.OtherRewards
                    .Include(x => x.Employee).Include(x => x.LeadCitation)
                    .ThenInclude(x => x.LeadCitationReplies).Include(x => x.Campaigns).ThenInclude(x => x.RewardTypes)
                    .Where(x => x.Campaigns.Id == campId && x.RewardId == rewardId).ToListAsync();
                return (IActionResult)result;
            }


        }
    }
}
