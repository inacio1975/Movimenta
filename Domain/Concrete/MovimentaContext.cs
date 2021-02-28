using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Domain.Entities;

namespace Domain.Concrete
{
    public class MovimentaContext : DbContext
    {
        public DbSet<Banco> Bancos { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Contacto> Contactos { get; set; }
        public DbSet<ContactoMb> ContactoDosMembros { get; set; }
        public DbSet<ContactoSt> ContactoDasStartups { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<EnderecoMb> EnderecoDosMembros { get; set; }
        public DbSet<EnderecoSt> EnderecoDasStartups { get; set; }
        public DbSet<EnderecoCampanha> EnderecoCampanhas { get; set; }
        public DbSet<Membro> Membros { get; set; }
        public DbSet<DocumentoMembro> DocumentoMembros { get; set; } 
        public DbSet<Novidade> Novidades { get; set; }
        public DbSet<Projecto> Projectos { get; set; }
        public DbSet<Startup> Startups { get; set; }
        public DbSet<Carrossel> Carrossels { get; set; } 
        public DbSet<Sucesso> Sucessos { get; set; }
        public DbSet<Movimentador> Movimentadores { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; } 
        public DbSet<MembroUsuario> MembroUsuarios { get; set; } 
        public DbSet<RoleUsuario> RoleUsuarios { get; set; } 
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Provincia> Provincias { get; set; }  
        public DbSet<Municipio> Municipios { get; set; } 
        public DbSet<Categoria> Categorias { get; set; } 
        public DbSet<Campanha> Campanhas { get; set; }
        public DbSet<Recompensa> Recompensas { get; set; }
        public DbSet<Evento> Eventos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Banco>().ToTable("Bancos");
            //modelBuilder.Entity<Comentario>().ToTable("Comentarios");
            //modelBuilder.Entity<Contacto>().ToTable("Contactos");
            //modelBuilder.Entity<ContactoMb>().ToTable("ContactoDosMembros");
            //modelBuilder.Entity<ContactoSt>().ToTable("ContactoDasStartups");
            //modelBuilder.Entity<Documento>().ToTable("Documentos");
            //modelBuilder.Entity<Endereco>().ToTable("Enderecos");
            //modelBuilder.Entity<EnderecoMb>().ToTable("EnderecoDosMembros");
            //modelBuilder.Entity<EnderecoSt>().ToTable("EnderecoDasStartups");
            //modelBuilder.Entity<Membro>().ToTable("Membros");
            //modelBuilder.Entity<Projecto>().ToTable("Projectos");
            //modelBuilder.Entity<Sucesso>().ToTable("Sucessos");
            //modelBuilder.Entity<Movimentador>().ToTable("Movimentadores");
            //modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            //modelBuilder.Entity<MembroUsuario>().ToTable("MembroUsuarios");
            //modelBuilder.Entity<Rol>().ToTable("Roles");
            //modelBuilder.Entity<RoleUsuario>().ToTable("RolesUsuario");
        }
    }
}
