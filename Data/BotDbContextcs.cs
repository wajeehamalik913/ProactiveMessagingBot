using System.Collections.Generic;
using Microsoft.BotBuilderSamples.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.BotBuilderSamples.Data
{
    public class BotDbContext : DbContext
    {
        public BotDbContext(DbContextOptions options) : base(options) { }

    public virtual DbSet<BotInfo> BotInfo { get; set; } = null!;

}
}
