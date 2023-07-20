using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RR.DataBaseConnect;
using RR.Models.EmployeeInfo;
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
    public class PeerToPeerServices
    {
        private readonly DataBaseAccess dataBaseAccess;

        public PeerToPeerServices()
        {

        }

        public PeerToPeerServices(DataBaseAccess dataBaseAccess)
        {
            this.dataBaseAccess = dataBaseAccess;
        }
    


        public async Task<ActionResult<IEnumerable<PeerToPeer>>> GetPeerToPeerAsync(int campId)
        {
            var result = await  dataBaseAccess.PeerToPeer.Where(x=>x.Campaigns.Id==campId).ToListAsync();

            result.ForEach(  x =>
            {
                x.Employee =  dataBaseAccess.Employee.FirstOrDefault(emp => emp.EmployeeId.Equals(x.NomineeId));
                x.Campaigns = dataBaseAccess.Campaigns.Include(x=>x.RewardTypes).FirstOrDefault(x=>x.Id==campId);
                
            });

            return result;

        }

        public async Task<ActionResult<PeerToPeer>> GetByNominator(int campId, string NominatorId)
        {
            
                var result = await dataBaseAccess.PeerToPeer.Include(x=>x.Employee)
                .Include(x=>x.Campaigns).FirstOrDefaultAsync(x => x.NominatorId.Equals(NominatorId)&&x.Campaigns.Id==campId);

            if (result == null)
            {
                return null;
            }
             return result;
            
            
        }

       


        public async Task<ActionResult<PeerToPeer>> AddPeerToPeerNominees(RequestNomination requestPeerToPeer)
        {
            PeerToPeer peerToPeer = new PeerToPeer();
            peerToPeer.CampaignId = requestPeerToPeer.CampaignId;
            peerToPeer.NominatorId = requestPeerToPeer.NominatorId;

            peerToPeer.NomineeId = requestPeerToPeer.NomineeId;

            peerToPeer.AwardCategory = requestPeerToPeer.AwardCategory;

            peerToPeer.Month = requestPeerToPeer.Month;

            peerToPeer.Citation = requestPeerToPeer.Citation;

            //  p2.employee = dataBaseAccess.Employees.Find(p.empId);
            PeerToPeerResults result = new PeerToPeerResults();
            result.NomineeId = requestPeerToPeer.NomineeId;
            result.NominatorId = requestPeerToPeer.NominatorId;
            result.Citation = requestPeerToPeer.Citation;


            //Employee
            Employee employee = await dataBaseAccess.Employee.FirstOrDefaultAsync(x => x.EmployeeId.Equals(peerToPeer.NomineeId));
            result.Employee = employee;


            //Campaign 
            Campaigns campaign = await dataBaseAccess.Campaigns.FirstOrDefaultAsync(x => x.Id.Equals(peerToPeer.CampaignId));

            result.campaigns = campaign;


            peerToPeer.Campaigns = campaign;
            peerToPeer.PeerToPeerResults = result;
            peerToPeer.Employee = employee;

            await dataBaseAccess.PeerToPeer.AddAsync(peerToPeer);
            await dataBaseAccess.SaveChangesAsync();

            return peerToPeer;
        }

        public async Task<ActionResult<PeerToPeer>> editNomination(RequestPeerToPeer requestPeerToPeer)
        {

            // Always fetch with foreign key
            PeerToPeer peerToPeer = await dataBaseAccess.PeerToPeer
                .FirstOrDefaultAsync(x => x.NominatorId.Equals(requestPeerToPeer.NominatorId) && x.Campaigns.Id == requestPeerToPeer.CampaignId);

            if(peerToPeer == null)
            {
                return null;
            }


            peerToPeer.CampaignId = requestPeerToPeer.CampaignId;
            peerToPeer.NominatorId = requestPeerToPeer.NominatorId;

            peerToPeer.NomineeId = requestPeerToPeer.NomineeId;

            peerToPeer.AwardCategory = requestPeerToPeer.AwardCategory;

            peerToPeer.Month = requestPeerToPeer.Month;

            peerToPeer.Citation = requestPeerToPeer.Citation;

            //Add Employee
            Employee employee = dataBaseAccess.Employee.FirstOrDefault(x => x.EmployeeId.Equals(requestPeerToPeer.NomineeId));
            peerToPeer.Employee = employee;

            PeerToPeerResults results = await dataBaseAccess.PeerToPeerResults.FirstOrDefaultAsync(x => x.NominatorId.Equals(requestPeerToPeer.NominatorId));

            results.NomineeId=requestPeerToPeer.NomineeId;
            results.Citation=requestPeerToPeer.Citation;
            results.awardCategory = requestPeerToPeer.AwardCategory;

            results.Employee = employee;

             dataBaseAccess.PeerToPeerResults.Update(results);

            //Add Peer Results;

            peerToPeer.PeerToPeerResults= results;


            dataBaseAccess.PeerToPeer.Update(peerToPeer);

            await dataBaseAccess.SaveChangesAsync();

            return peerToPeer;
        }
    }
}
