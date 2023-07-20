using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RR.DataBaseConnect;
using RR.Models.EmployeeInfo;
using RR.Models.OtherRewardsInfo;
using RR.Models.Rewards_Campaigns;
using RR.Services.RequestClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RR.Services
{
    public class OtherRewardsServices
    {
        private readonly DataBaseAccess dataBaseAccess;

        public OtherRewardsServices()
        {

        }

        public OtherRewardsServices(DataBaseAccess dataBaseAccess)
        {
            this.dataBaseAccess = dataBaseAccess;
        }


        public async Task<ActionResult<IEnumerable<OtherRewards>>> GetAllNominations()
        {
            var result = await dataBaseAccess.OtherRewards.ToListAsync();
            result.ForEach(x =>
            {
                /*x.Employee = dataBaseAccess.Employee.FirstOrDefault();*/
            });
            return result;
        }
        public async Task<ActionResult<IEnumerable<OtherRewards>>> GetAllNominationByCampId(int campId,int rewardId)
        {
            var result=await dataBaseAccess.OtherRewards.Include(x=>x.Employee).Include(x=>x.LeadCitation).ThenInclude(x=>x.LeadCitationReplies).Include(x=>x.Campaigns).ThenInclude(x=>x.RewardTypes).Where(x=>x.Campaigns.Id == campId && x.RewardId==rewardId).ToListAsync();
           /* result.ForEach(async x =>
            {
                //x.Employee = await dataBaseAccess.Employee.FirstOrDefaultAsync(e => e.EmployeeId.Equals(x.NomineeId));
                *//*x.LeadCitation=await dataBaseAccess.LeadCitation.
                Include(c=>c.LeadCitationReplies).
                FirstOrDefaultAsync(l=>l.NominatorId.Equals(x.NominatorId));*//*

            });*/

            return result;
        }
        // Get Nomination By NominatorId and Campaign Id
        public async Task<ActionResult<IEnumerable<OtherRewards>>> GetNominationById(string NominatorID, int campaignId)
        {
            List<OtherRewards> otherRewards= await dataBaseAccess.OtherRewards.Include(x=>x.Employee).Include(x=>x.Campaigns).Include(x=>x.LeadCitation).ThenInclude(x=>x.LeadCitationReplies)
                .Where(x => x.NominatorId.Equals(NominatorID) && x.Campaigns.Id == campaignId).ToListAsync() ;
            

            if(otherRewards == null)
            {
                return null;
            }
            return otherRewards;
        }

        public async Task<ActionResult<OtherRewards>> addNomination(RequestNomination requestOtherRewards)
        {
            OtherRewards otherRewards = new OtherRewards();

            otherRewards.CampaignId = requestOtherRewards.CampaignId;

            otherRewards.NominatorId = requestOtherRewards.NominatorId;

            otherRewards.NomineeId = requestOtherRewards.NomineeId;

            otherRewards.AwardCategory = requestOtherRewards.AwardCategory;

            otherRewards.Month = requestOtherRewards.Month;


            // Add Campaigns

            Campaigns campaign = await dataBaseAccess.Campaigns.FirstOrDefaultAsync(x => x.Id == otherRewards.CampaignId);

            otherRewards.RewardId = campaign.RewardId;

            otherRewards.Campaigns = campaign;


            // Add Employee reference

            Employee employee = await dataBaseAccess.Employee.FirstOrDefaultAsync(x => x.EmployeeId.Equals(otherRewards.NomineeId));

            otherRewards.Employee = employee;

            // Add Citation
            LeadCitation citation = new LeadCitation();

            citation.Citation = requestOtherRewards.Citation;
            citation.NominatorId = requestOtherRewards.NominatorId;
            otherRewards.LeadCitation = citation;

            /// cmp added 
            citation.Campaigns = campaign;

            // Add Final Connections

            otherRewards.LeadCitation = citation;

            await dataBaseAccess.OtherRewards.AddAsync(otherRewards);

            await dataBaseAccess.SaveChangesAsync();


            return otherRewards;
        }
      
        // Update Nomination
        public async Task<ActionResult<OtherRewards>> updateNomination(UpdateNomination updateNomination)
        {
            OtherRewards otherRewards = await dataBaseAccess.OtherRewards.FindAsync(updateNomination.otherRewardsId);

            if(otherRewards == null)
            {
                return null;
            }

            otherRewards.CampaignId = updateNomination.CampaignId;

            otherRewards.NominatorId = updateNomination.NominatorId;

            otherRewards.NomineeId = updateNomination.NomineeId;

            otherRewards.AwardCategory = updateNomination.AwardCategory;

            otherRewards.Month = updateNomination.Month;


            // Add Employee reference of the nominee

            Employee employee = await dataBaseAccess.Employee.FirstOrDefaultAsync(x => x.EmployeeId.Equals(otherRewards.NomineeId));

            otherRewards.Employee = employee;




            // Add Campaign reference

            Campaigns campaigns = await dataBaseAccess.Campaigns.FirstOrDefaultAsync(x => x.Id == otherRewards.CampaignId);

            otherRewards.Campaigns = campaigns;

            LeadCitation leadCitation = await dataBaseAccess.LeadCitation.FindAsync(updateNomination.leadCitationId);

            leadCitation.Citation = updateNomination.Citation;
            leadCitation.NominatorId = updateNomination.NominatorId;
            

            /// cmp added 
            leadCitation.Campaigns = campaigns;




            // Add Final Connections

            otherRewards.LeadCitation = leadCitation;

            dataBaseAccess.OtherRewards.Update(otherRewards);

            await dataBaseAccess.SaveChangesAsync();


            return otherRewards;

        }
     



        // CitationReplyController
        public async Task<ActionResult<LeadCitationReplies>> AddReply(RequestLeadCitationReplies requestLeadCitationReplies)
        {
            LeadCitationReplies leadCitationReplies = new LeadCitationReplies();

            // Foreign KEy values added

            leadCitationReplies.Campaigns = dataBaseAccess.Campaigns.FirstOrDefault(x => x.Id == requestLeadCitationReplies.CampaignId);


            // required values

            leadCitationReplies.ReplyCitation = requestLeadCitationReplies.ReplyCitation;

            leadCitationReplies.NominatorId = requestLeadCitationReplies.NominatorId;

            leadCitationReplies.ReplierId = requestLeadCitationReplies.ReplierId;

            leadCitationReplies.CampaignId = requestLeadCitationReplies.CampaignId;

            // leadCitationReplies.LeadCitation = dataBaseAccess.LeadCitation.
            //    FirstOrDefault(x => x.NominatorId.Equals(requestLeadCitationReplies.NominatorId) && x.Campaigns.Id == requestLeadCitationReplies.CampaignId);

            leadCitationReplies.LeadCitationId = requestLeadCitationReplies.leadCitationId;
            await dataBaseAccess.LeadCitationReplies.AddAsync(leadCitationReplies);

            await dataBaseAccess.SaveChangesAsync();

            return leadCitationReplies;
        }

        public async Task<ActionResult<IEnumerable<LeadCitation>>> getCitationWithReplies()
        {
            var result = await dataBaseAccess.LeadCitation.ToListAsync();

           /* result.ForEach(async x =>
            {
               // x.LeadCitationReplies = dataBaseAccess.LeadCitationReplies.Where(r => r.LeadCitation.Id == x.Id).ToList();
            });*/

            return result;
        }

        public async Task<ActionResult<OtherRewardResults>> addVote(RequestVote requestVote)
        {
            OtherRewardResults otherRewardResults = new OtherRewardResults();

            OtherRewards nomination = await dataBaseAccess.OtherRewards.FindAsync(requestVote.idOfNomination);
            otherRewardResults.OtherRewards = nomination;
            //
            otherRewardResults.VoterId = requestVote.VoterId;
            otherRewardResults.Stars = requestVote.Stars;

            //

            otherRewardResults.AwardCategory= nomination.AwardCategory;

            otherRewardResults.NomineeId = nomination.NomineeId;

            otherRewardResults.NominatorId = nomination.NominatorId;

            otherRewardResults.RewardId = nomination.RewardId;

            otherRewardResults.CampaignId = nomination.CampaignId;

           

            otherRewardResults.Employee=await dataBaseAccess.Employee.FirstOrDefaultAsync(x=>x.EmployeeId.Equals(nomination.NomineeId));

            otherRewardResults.Campaigns = await dataBaseAccess.Campaigns.FindAsync(nomination.CampaignId);

            otherRewardResults.CampaignName = otherRewardResults.Campaigns.CampaignName;

            await dataBaseAccess.OtherRewardResults.AddAsync(otherRewardResults);

            await dataBaseAccess.SaveChangesAsync();

            return otherRewardResults;
        }

        public async Task<ActionResult<OtherRewardResults>> updateVote(UpdateVote updateVote)
        {
            OtherRewardResults otherRewardResults = await dataBaseAccess.OtherRewardResults.FindAsync(updateVote.voteId);

            if(otherRewardResults==null)
            {
                return null;
            }
            OtherRewards nomination = await dataBaseAccess.OtherRewards.FindAsync(updateVote.idOfNomination);

            otherRewardResults.OtherRewards = nomination;

            otherRewardResults.VoterId = updateVote.VoterId;

            otherRewardResults.AwardCategory = nomination.AwardCategory;

            otherRewardResults.NomineeId = nomination.NomineeId;

            otherRewardResults.NominatorId = nomination.NominatorId;

            otherRewardResults.RewardId = nomination.RewardId;

            otherRewardResults.CampaignId = nomination.CampaignId;

            otherRewardResults.Stars = updateVote.Stars;



            otherRewardResults.Employee = await dataBaseAccess.Employee.FirstOrDefaultAsync(x => x.EmployeeId.Equals(nomination.NomineeId));

            otherRewardResults.Campaigns = await dataBaseAccess.Campaigns.FindAsync(nomination.CampaignId);

            otherRewardResults.CampaignName = otherRewardResults.Campaigns.CampaignName;

            dataBaseAccess.OtherRewardResults.Update(otherRewardResults);

            await dataBaseAccess.SaveChangesAsync();

            return otherRewardResults;
        }






















        /* public async Task<ActionResult<OtherRewards>> AddReward(RequestOtherRewards requestOtherRewards)
       {
           OtherRewards otherRewards = new OtherRewards();

           otherRewards.CampaignId = requestOtherRewards.CampaignId;

           otherRewards.NominatorId = requestOtherRewards.NominatorId;

           otherRewards.NomineeId = requestOtherRewards.NomineeId;

           otherRewards.AwardCategory = requestOtherRewards.AwardCategory;

           otherRewards.Month = requestOtherRewards.Month;

         //  otherRewards.Stars = requestOtherRewards.Stars;

           Campaigns campaign = await dataBaseAccess.Campaigns.FirstOrDefaultAsync(x => x.Id == otherRewards.CampaignId);

           otherRewards.RewardId = campaign.RewardId;


           // Add Employee reference

           Employee employee = await dataBaseAccess.Employee.FirstOrDefaultAsync(x => x.EmployeeId.Equals(otherRewards.NomineeId));

           otherRewards.Employee = employee;




           // Add Campaign reference

           Campaigns campaigns = await dataBaseAccess.Campaigns.FirstOrDefaultAsync(x => x.Id == otherRewards.CampaignId);

           otherRewards.Campaigns = campaigns;



           // Results table post

           OtherRewardResults otherRewardResults = new OtherRewardResults();

           otherRewardResults.RewardId = otherRewards.RewardId;



           otherRewardResults.CampaignId = otherRewards.CampaignId;

           otherRewardResults.NominatorId = otherRewards.NominatorId;

           otherRewardResults.NomineeId = otherRewards.NomineeId;

           otherRewardResults.AwardCategory = otherRewards.AwardCategory;

           otherRewardResults.Employee = employee;

           otherRewardResults.Campaigns = campaigns;

           otherRewardResults.Stars = 0;

           otherRewardResults.CampaignName = dataBaseAccess.Campaigns.Find(otherRewardResults.CampaignId).CampaignName;

           // add Citation

           LeadCitation citation = new LeadCitation();

           citation.Citation = requestOtherRewards.Citation;
           citation.NominatorId = requestOtherRewards.NominatorId;
           otherRewards.LeadCitation = citation;

           /// cmp added 
           citation.Campaigns = campaigns;




           // Add Final Connections

           otherRewards.LeadCitation = citation;

           otherRewards.OtherRewardResults = otherRewardResults;



           //save to database

           await dataBaseAccess.OtherRewards.AddAsync(otherRewards);

           await dataBaseAccess.SaveChangesAsync();


           return otherRewards;



       }*/
        //  =========================

        /*  public async Task<ActionResult<OtherRewards>> updateNominations(UpdateNomination requestOtherRewards)
        {
            *//* OtherRewards otherRewards = await dataBaseAccess.OtherRewards.
                 FirstOrDefaultAsync(x => x.NominatorId.Equals(requestOtherRewards.NominatorId) &&
                 x.Campaigns.Id== requestOtherRewards.CampaignId);*//*

            OtherRewards otherRewards = await dataBaseAccess.OtherRewards.FindAsync(requestOtherRewards.otherRewardsId);

            if(otherRewards == null)
            {
                return null;
            }

            otherRewards.CampaignId = requestOtherRewards.CampaignId;

            otherRewards.NominatorId = requestOtherRewards.NominatorId;

            otherRewards.NomineeId = requestOtherRewards.NomineeId;

            otherRewards.AwardCategory = requestOtherRewards.AwardCategory;

            otherRewards.Month = requestOtherRewards.Month;

          //  otherRewards.Stars = requestOtherRewards.Stars;

            Campaigns campaign = await dataBaseAccess.Campaigns.FirstOrDefaultAsync(x => x.Id == otherRewards.CampaignId);

            otherRewards.RewardId = campaign.RewardId;


            // Add Employee reference of the nominator

            Employee employee = await dataBaseAccess.Employee.FirstOrDefaultAsync(x => x.EmployeeId.Equals(otherRewards.NomineeId));

            otherRewards.Employee = employee;




            // Add Campaign reference

            Campaigns campaigns = await dataBaseAccess.Campaigns.FirstOrDefaultAsync(x => x.Id == otherRewards.CampaignId);

            otherRewards.Campaigns = campaigns;



            // Results table post

            OtherRewardResults otherRewardResults = new OtherRewardResults();

            otherRewardResults.RewardId = otherRewards.RewardId;



            otherRewardResults.CampaignId = otherRewards.CampaignId;

            otherRewardResults.NominatorId = otherRewards.NominatorId;

            otherRewardResults.NomineeId = otherRewards.NomineeId;

            otherRewardResults.AwardCategory = otherRewards.AwardCategory;

            otherRewardResults.Employee = employee;

            otherRewardResults.Campaigns = campaigns;

            otherRewardResults.Stars = otherRewards.Stars;

            otherRewardResults.CampaignName = dataBaseAccess.Campaigns.Find(otherRewardResults.CampaignId).CampaignName;

            // add Citation

            LeadCitation citation = new LeadCitation();

            citation.Citation = requestOtherRewards.Citation;
            citation.NominatorId = requestOtherRewards.NominatorId;
            otherRewards.LeadCitation = citation;

            /// cmp added 
            citation.Campaigns = campaigns;




            // Add Final Connections

            otherRewards.LeadCitation = citation;

            otherRewards.OtherRewardResults = otherRewardResults;



            //save to database

             dataBaseAccess.OtherRewards.Update(otherRewards);

            await dataBaseAccess.SaveChangesAsync();


            return otherRewards;

        }*/
    }
}
