using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }


        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // seed data for difficulties
            // easy, medium, har
            var difficulties = new List<Difficulty>
            {
                new Difficulty()
                {
                    Id = Guid.Parse("39a9b70f-d799-4d20-a33c-65230e60ac28"),
                    Name = "Easy"
                },
                 new Difficulty()
                {
                    Id = Guid.Parse("3b60291b-1546-4b8d-afab-a951ae5d4829"),
                    Name = "Medium"
                },
                  new Difficulty()
                {
                    Id = Guid.Parse("d249d7cd-9b5e-4bd3-b517-a16f8595a64e"),
                    Name = "Hard"
                },
            };

            // seed difficulties to database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);


            // Seed data for Regions
            var regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.Parse("f7248fc3-2585-4efb-8d1d-1c555f4087f6"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImageUrl = "https://th.bing.com/th/id/R.4a2c082592782100a50e5c4b13aaedb8?rik=rNZjmrDwRoCbNg&pid=ImgRaw&r=0"
                },
                new Region
                {
                    Id = Guid.Parse("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"),
                    Name = "Northland",
                    Code = "NTL",
                    RegionImageUrl = "https://th.bing.com/th/id/R.4a2c082592782100a50e5c4b13aaedb8?rik=rNZjmrDwRoCbNg&pid=ImgRaw&r=0"
                },
                new Region
                {
                    Id = Guid.Parse("14ceba71-4b51-4777-9b17-46602cf66153"),
                    Name = "Bay Of Plenty",
                    Code = "BOP",
                    RegionImageUrl = "https://th.bing.com/th/id/R.4a2c082592782100a50e5c4b13aaedb8?rik=rNZjmrDwRoCbNg&pid=ImgRaw&r=0"
                },
                new Region
                {
                    Id = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
                    Name = "Wellington",
                    Code = "WGN",
                    RegionImageUrl = "https://th.bing.com/th/id/R.4a2c082592782100a50e5c4b13aaedb8?rik=rNZjmrDwRoCbNg&pid=ImgRaw&r=0"
                },
                new Region
                {
                    Id = Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
                    Name = "Nelson",
                    Code = "NSN",
                    RegionImageUrl = "https://th.bing.com/th/id/R.4a2c082592782100a50e5c4b13aaedb8?rik=rNZjmrDwRoCbNg&pid=ImgRaw&r=0"
                },
                new Region
                {
                    Id = Guid.Parse("f077a22e-4248-4bf6-b564-c7cf4e250263"),
                    Name = "Southland",
                    Code = "STL",
                    RegionImageUrl = "https://th.bing.com/th/id/R.4a2c082592782100a50e5c4b13aaedb8?rik=rNZjmrDwRoCbNg&pid=ImgRaw&r=0"
                },
            };

            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
