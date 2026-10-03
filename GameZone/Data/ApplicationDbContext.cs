namespace GameZone.Data;

public class ApplicationDbContext : DbContext {
    public DbSet<Game> Games { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<GameDevice> GameDevices { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<GameDevice>()
            .HasKey(e => new {e.GameId, e.DeviceId});

        base.OnModelCreating(modelBuilder);
    }
}
