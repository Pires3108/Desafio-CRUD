using ClienteCRUD.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;

namespace ClienteCRUD.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Funcionarios { get; set; }
        public DbSet<Equipe> Equipes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Telefone).HasMaxLength(20);
                entity.Property(e => e.Endereco).HasMaxLength(200);
                entity.Property(e => e.Cidade).HasMaxLength(100);
                entity.Property(e => e.Estado).HasMaxLength(2);
                entity.Property(e => e.Cep).HasMaxLength(10);
                entity.Property(e => e.Usuario).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Senha).IsRequired().HasMaxLength(100);
                
                entity.Property(e => e.DataCadastro).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.Ativo).HasDefaultValue(true);

                // Índices únicos
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.Usuario).IsUnique();
            });

            modelBuilder.Entity<Equipe>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(150);
                entity.HasIndex(e => e.Nome).IsUnique();
                entity.HasMany(e => e.Funcionarios).WithOne(c => c.Equipe).HasForeignKey(c => c.EquipeId);
            });
        }
    }
} 