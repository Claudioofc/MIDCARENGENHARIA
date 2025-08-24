using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeContatos.Models.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {
        }

        public DbSet<ContatoModel> Contatos { get; set; }
        public DbSet<ClienteModel> Clientes { get; set; }
        public DbSet<VeiculoModel> Veiculos { get; set; }
        public DbSet<OrdemServicoModel> OrdensServico { get; set; }
        public DbSet<OrcamentoModel> Orcamentos { get; set; }
        public DbSet<ItemOrcamentoModel> ItensOrcamento { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar relacionamentos para evitar múltiplos caminhos de cascade
            modelBuilder.Entity<OrdemServicoModel>()
                .HasOne(os => os.Cliente)
                .WithMany()
                .HasForeignKey(os => os.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrdemServicoModel>()
                .HasOne(os => os.Veiculo)
                .WithMany()
                .HasForeignKey(os => os.VeiculoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configurar precisão para o campo Valor
            modelBuilder.Entity<OrdemServicoModel>()
                .Property(os => os.Valor)
                .HasPrecision(18, 2);

            // Configurar relacionamentos para Orçamentos
            modelBuilder.Entity<OrcamentoModel>()
                .HasOne(o => o.Cliente)
                .WithMany()
                .HasForeignKey(o => o.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrcamentoModel>()
                .HasOne(o => o.Veiculo)
                .WithMany()
                .HasForeignKey(o => o.VeiculoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrcamentoModel>()
                .HasOne(o => o.OrdemServico)
                .WithMany()
                .HasForeignKey(o => o.OrdemServicoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configurar relacionamentos para Itens de Orçamento
            modelBuilder.Entity<ItemOrcamentoModel>()
                .HasOne(i => i.Orcamento)
                .WithMany(o => o.Itens)
                .HasForeignKey(i => i.OrcamentoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configurar precisão para campos de valor
            modelBuilder.Entity<OrcamentoModel>()
                .Property(o => o.ValorTotal)
                .HasPrecision(18, 2);

            modelBuilder.Entity<ItemOrcamentoModel>()
                .Property(i => i.ValorUnitario)
                .HasPrecision(18, 2);

            modelBuilder.Entity<ItemOrcamentoModel>()
                .Property(i => i.ValorTotal)
                .HasPrecision(18, 2);
        }
    }
}
