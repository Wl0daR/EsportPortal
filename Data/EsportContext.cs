using Microsoft.EntityFrameworkCore;
using EsportPortal.Models;

namespace EsportPortal.Data
{
    public class EsportContext : DbContext
    {
        public EsportContext(DbContextOptions<EsportContext> options) : base(options) { }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<PlayerHistory> PlayerHistories { get; set; }
        public DbSet<TeamTournamentHistory> TeamTournamentHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>()
                .HasMany(t => t.Players)
                .WithOne(p => p.Team)
                .HasForeignKey(p => p.TeamId);

            modelBuilder.Entity<PlayerHistory>()
                .HasOne(ph => ph.Player)
                .WithMany(p => p.PlayerHistories)
                .HasForeignKey(ph => ph.PlayerId);


            modelBuilder.Entity<TeamTournamentHistory>()
                .HasOne(tth => tth.Tournament)
                .WithMany(t => t.TeamTournamentHistories)
                .HasForeignKey(tth => tth.TournamentId);

            modelBuilder.Entity<TeamTournamentHistory>()
                .HasOne(tth => tth.Player)
                .WithMany(p => p.TeamTournamentHistories)
                .HasForeignKey(tth => tth.PlayerId);
        }
    }
}
