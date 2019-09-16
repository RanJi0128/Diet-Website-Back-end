using ControleDApi.Models;
using ControleDApi.Models.Auth;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ControleDApi.DAL
{
    public class Context : IdentityDbContext<Usuario, CustomRole,
    int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {

        public Context()
            : base("BD_ControleD")
        {
          //  Database.SetInitializer<Context>(null);
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = false;
        }
        public static Context Create()
        {
            return new Context();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)

        {

            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<IdentityUser>()
            //    .ToTable("Usuarios")
            //    .Property(p => p.Id)
            //    .HasColumnName("UsuarioId");

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Usuario>()
                .ToTable("Usuarios")
                .Property(p => p.Id)
                .HasColumnName("UsuarioId");
            modelBuilder.Entity<CustomUserRole>()
                .ToTable("UsuarioRole")
                .Property(x => x.UserId).HasColumnName("UsuarioId");
            modelBuilder.Entity<CustomUserLogin>()
                .ToTable("Logins");
            modelBuilder.Entity<CustomUserClaim>()
                .ToTable("Claims");
            modelBuilder.Entity<CustomRole>()
                 .ToTable("Roles");

            //modelBuilder.Entity<UsuarioUsuarios>()
            //    .Property(x => x.Id).HasColumnName("Id");

            modelBuilder.Entity<Usuario>()
               .HasMany<Usuario>(s => s.Usuarios)
               .WithMany()
               .Map(cs =>
               {
                   cs.MapLeftKey("Medico_Id");
                   cs.MapRightKey("Paciente_Id");
                   cs.ToTable("UsuarioUsuarios");
               });

        }


        public DbSet<Alimento> Alimento { get; set; }
        public DbSet<AlimentoConsumo> AlimentoConsumo { get; set; }
        public DbSet<Refeicao> Refeicao { get; set; }
        public DbSet<Insulina> Insulina { get; set; }
        public DbSet<Agendamento> Agendamento { get; set; }
        //public DbSet<CustomUserRole> UsuarioRole { get; set; }
        //public DbSet<Usuario> Usuario { get; set; }

        //public DbSet<CustomRole> CustomRole { get; set; }
        //public DbSet<CustomUserClaim> CustomUserClaim { get; set; }
        //public DbSet<CustomUserLogin> CustomUserLogin { get; set; }
        //public DbSet<CustomUserRole> CustomUserRole { get; set; }


    }
}