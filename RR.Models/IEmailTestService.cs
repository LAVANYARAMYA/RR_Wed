using RR.Models.Rewards_Campaigns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RR.Models
{
    public  interface IEmailTestService
    {
        void SendAllMailId(List<string> mails, string campname, string rewardType);

        void sendEmail(string email, string campname, string rewardType);
    }
}
