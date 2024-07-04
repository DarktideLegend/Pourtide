using ACE.Database.Models.Auth;
using ACE.Database.Models.Pourtide;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Database.Models.Pourtide
{
    public partial class PourtideDbContext : DbContext
    {
        public PourtideDbContext()
        {
        }

        public PourtideDbContext(DbContextOptions<PourtideDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CharacterLogin> CharacterLogin { get; set; }
        public virtual DbSet<XpCap> XpCap { get; set; }
        public DbSet<PKStatsDamage> PKStatsDamages { get; set; }
        public DbSet<PKStatsKill> PKStatsKills { get; set; }
        public DbSet<PkTrophyCooldown> PkTrophyCooldowns { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = Common.ConfigManager.Config.MySql.Pourtide;

                var connectionString = $"server={config.Host};port={config.Port};user={config.Username};password={config.Password};database={config.Database};{config.ConnectionOptions}";

                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), builder =>
                {
                    builder.EnableRetryOnFailure(10);
                });
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<XpCap>(entity =>
            {
                entity.ToTable("xp_cap");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.DailyTimestamp)
                    .HasColumnType("datetime")
                    .IsRequired()
                    .HasDefaultValueSql("GETDATE()"); 

                entity.Property(e => e.WeeklyTimestamp)
                    .HasColumnType("datetime")
                    .IsRequired()
                    .HasDefaultValueSql("DATEADD(DAY, 6, CAST(GETDATE() AS DATE))"); 

                entity.Property(e => e.Week)
                  .IsRequired()
                  .HasDefaultValue(1);
            });

            modelBuilder.Entity<CharacterLogin>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.Property(e => e.Id).HasColumnName("characterLoginLogId");

                entity.ToTable("character_login");

                entity.Property(e => e.AccountId)
                    .HasColumnName("accountId");

                entity.Property(e => e.AccountName)
                    .HasColumnName("accountName");

                entity.Property(e => e.SessionIP)
                    .HasColumnName("sessionIP");

                entity.Property(e => e.CharacterId)
                    .HasColumnName("characterId");

                entity.Property(e => e.CharacterName)
                    .HasColumnName("characterName");

                entity.Property(e => e.LoginDateTime)
                    .HasColumnName("loginDateTime");

                entity.Property(e => e.LogoutDateTime)
                    .HasColumnName("logoutDateTime");

                entity.Property(e => e.HomeRealmId).HasColumnName("home_realm_id"); 
                entity.Property(e => e.CurrentRealmId).HasColumnName("current_realm_id"); 
            });

            modelBuilder.Entity<PKStatsDamage>(entity =>
            {
                entity.ToTable("pk_stats_damage"); 

                entity.HasKey(e => e.PKDamageId); 

                entity.Property(e => e.PKDamageId).HasColumnName("pk_damage_id"); 
                entity.Property(e => e.AttackerId).HasColumnName("attacker_id"); 
                entity.Property(e => e.DefenderId).HasColumnName("defender_id"); 
                entity.Property(e => e.DamageAmount).HasColumnName("damage_amount"); 
                entity.Property(e => e.HomeRealmId).HasColumnName("home_realm_id"); 
                entity.Property(e => e.CurrentRealmId).HasColumnName("current_realm_id"); 
                entity.Property(e => e.EventTime).HasColumnName("event_time"); 
                entity.Property(e => e.IsCrit).HasColumnName("is_crit"); 
                entity.Property(e => e.CombatMode).HasColumnName("combat_mode"); 
            });

            modelBuilder.Entity<PKStatsKill>(entity =>
            {
                entity.ToTable("pk_stats_kills"); 

                entity.HasKey(e => e.PKKillsId); 

                entity.Property(e => e.PKKillsId).HasColumnName("pk_kills_id"); 
                entity.Property(e => e.KillerId).HasColumnName("killer_id"); 
                entity.Property(e => e.VictimId).HasColumnName("victim_id"); 
                entity.Property(e => e.HomeRealmId).HasColumnName("home_realm_id"); 
                entity.Property(e => e.CurrentRealmId).HasColumnName("current_realm_id"); 
                entity.Property(e => e.EventTime).HasColumnName("event_time"); 
            });

            modelBuilder.Entity<PkTrophyCooldown>(entity =>
            {
                entity.ToTable("pk_trophy_cooldown"); 

                entity.HasKey(e => e.TrophyCooldownId); 

                entity.Property(e => e.TrophyCooldownId).HasColumnName("trophy_cooldown_id"); 
                entity.Property(e => e.KillerId).HasColumnName("killer_id"); 
                entity.Property(e => e.VictimId).HasColumnName("victim_id"); 
                entity.Property(e => e.HomeRealmId).HasColumnName("home_realm_id"); 
                entity.Property(e => e.CurrentRealmId).HasColumnName("current_realm_id"); 
                entity.Property(e => e.CooldownEndTime).HasColumnName("cooldown_end_time"); 
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
