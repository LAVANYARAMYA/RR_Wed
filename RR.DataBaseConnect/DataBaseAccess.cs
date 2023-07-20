using Microsoft.EntityFrameworkCore;
using RR.Models;
using RR.Models.EmployeeInfo;
using RR.Models.OtherRewardsInfo;
using RR.Models.PeerToPeerInfo;
using RR.Models.Rewards_Campaigns;

namespace RR.DataBaseConnect
{
    public class DataBaseAccess:DbContext
    {
        public DataBaseAccess(DbContextOptions<DataBaseAccess> options) : base(options) { }

        public DbSet<Employee> Employee { get; set; }

        public DbSet<Roles> Roles { get; set; }

        public DbSet<EmployeeRoles> EmployeeRoles { get; set; }

        public DbSet<UserNamePassword> UserNamePassword { get; set; }


        public DbSet<RewardType> RewardType { get; set; }

        public DbSet<Campaigns> Campaigns { get; set; }

        public DbSet<PeerToPeer> PeerToPeer { get; set; }

        public DbSet<PeerToPeerResults> PeerToPeerResults { get; set; }

        public DbSet<OtherRewards> OtherRewards { get; set; }
        public DbSet<LeadCitation> LeadCitation { get; set; }

        public DbSet<LeadCitationReplies> LeadCitationReplies { get; set; }

        public DbSet<OtherRewardResults> OtherRewardResults { get; set; }

        public DbSet<AwardCategory> AwardCategory { get; set; }

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasIndex(p => new { p.EmployeeId })
                .IsUnique(true);
        }
    }
}