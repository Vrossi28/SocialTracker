using ExternalIntegration.Twitter.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalIntegration
{
    public class SocialTrackerContext : DbContext
    {
        public DbSet<UserBaseData> User { get; set; }
        public DbSet<UserAllData> UserAllData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(@"Data Source=DATA_SOURCE;Initial Catalog=INITIAL_CATALOG;User ID =LOGIN; Password=PASSWORD");
    }
}
