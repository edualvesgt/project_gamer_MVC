using Microsoft.EntityFrameworkCore;
using Projeto_gamer_Back.Models;

namespace Projeto_gamer_Back.Infra
{
    public class Context : DbContext
    {
        //CONFIGURACAO DE ACESSO AO BANCO 
        public Context()
        {

        }

        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // AUTENTICACAO PELO SQL 
                //user Id = "LOGIN (NOME DO USUARIO)"
                //pwd = "SENHA (COLOCAR SUA SENHA)"

                //STRING DE CONEXAO COM O BANCO DE DADOS
                //LINHA IMPORTATE 
                // DATA SOURCE E O NOME DO GERENCIADOR DO BANCO 
                // INITIAL CATALOG O NOME DO BANCO DE DADOS 

                // INTEGRATED SECURITY : AUTENTICACAO PELO WINDOWNS
                // TRUST SERVER CERTIFICATE : AUTENTICACAO PELO WINDOWNS/ AUTENTICACAO SQL
                optionsBuilder.UseSqlServer("Data Source = NOTE13-S14; Initial Catalog = projectGamer; user Id = sa; pwd = Senai@134; TrustServerCertificate = true");
            }
        }

        public DbSet<Jogador> Jogador { get; set; }

        public DbSet<Equipe> Equipe { get; set; }

    }
}