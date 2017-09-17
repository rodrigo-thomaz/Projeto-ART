using RThomaz.Domain.Financeiro.Entities;
using System.Data.Entity.ModelConfiguration;

namespace RThomaz.Infra.Data.Persistence.Configurations
{
    public class ProgramadorConfiguration : ComplexTypeConfiguration<Programador>
    {
        public ProgramadorConfiguration()
        {
            //DataInicial
            Property(x => x.DataInicial)
                .HasColumnName("DataInicial")
                .IsRequired();

            //DataFinal
            Property(x => x.DataFinal)
                .HasColumnName("DataFinal")
                .IsRequired();

            //Frequencia
            Property(x => x.Frequencia)
                .HasColumnName("Frequencia")
                .IsRequired();

            //Dia
            Property(x => x.Dia)
                .HasColumnName("Dia")
                .IsOptional();

            //HasDomingo
            Property(x => x.HasDomingo)
                .HasColumnName("HasDomingo")
                .IsOptional();

            //HasSegunda
            Property(x => x.HasSegunda)
                .HasColumnName("HasSegunda")
                .IsOptional();

            //HasTerca
            Property(x => x.HasTerca)
                .HasColumnName("HasTerca")
                .IsOptional();

            //HasQuarta
            Property(x => x.HasQuarta)
                .HasColumnName("HasQuarta")
                .IsOptional();

            //HasQuinta
            Property(x => x.HasQuinta)
                .HasColumnName("HasQuinta")
                .IsOptional();

            //HasSexta
            Property(x => x.HasSexta)
                .HasColumnName("HasSexta")
                .IsOptional();

            //HasSabado
            Property(x => x.HasSabado)
                .HasColumnName("HasSabado")
                .IsOptional();

            //Historico
            Property(x => x.Historico)
                .HasColumnName("Historico")
                .HasMaxLength(500)
                .IsRequired();

            //ValorVencimento
            Property(x => x.ValorVencimento)
                .HasColumnName("ValorVencimento")
                .IsRequired()
                .HasPrecision(18, 2);

            //Observacao
            Property(x => x.Observacao)
                .HasColumnName("Observacao")
                .HasMaxLength(4000)
                .IsOptional();
        }
    }
}
