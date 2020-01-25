using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VoBoo.Models;

namespace VoBoo.Data
{
	public class ApplicationDbContext : IdentityDbContext<
            ApplicationUser,
            ApplicationRole,
            string,
            IdentityUserClaim<string>,
            ApplicationUserRole,
            IdentityUserLogin<string>,
            IdentityRoleClaim<string>,
            IdentityUserToken<string>>
    {
        public DbSet<Word> Words { get; set; }
        public DbSet<DictionaryTranslation> DictionaryTranslations { get; set; }
        public DbSet<WordTranslation> Translations { get; set; }
        public DbSet<UserDictionary> Dictionaries { get; set; }
        public DbSet<Language> Languages { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<DictionaryTranslation>(entity =>
            {
                entity.HasOne(dt => dt.UserDictionary)
                    .WithMany(dt => dt.DictionaryTranslations)
                    .HasForeignKey(dt => dt.UserDictionaryId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(entity =>
            {
                entity.HasMany(u => u.UserRoles)
                    .WithOne(ur => ur.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.Dictionaries)
                    .WithOne(d => d.User)
                    .HasForeignKey(d => d.UserId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<ApplicationRole>(entity =>
            {
                entity.HasMany(r => r.UserRoles)
                    .WithOne(ur => ur.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
