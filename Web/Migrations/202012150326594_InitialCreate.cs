namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        MembroId = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Membroes", t => t.MembroId, cascadeDelete: true)
                .Index(t => t.MembroId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            //CreateTable(
            //    "dbo.Membroes",
            //    c => new
            //        {
            //            MembroId = c.Int(nullable: false, identity: true),
            //            Nome = c.String(nullable: false),
            //            Foto = c.String(),
            //            Cargo = c.String(),
            //            Bio = c.String(),
            //            StartupId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.MembroId)
            //    .ForeignKey("dbo.Startups", t => t.StartupId)
            //    .Index(t => t.StartupId);
            
            //CreateTable(
            //    "dbo.Startups",
            //    c => new
            //        {
            //            StartupId = c.Int(nullable: false, identity: true),
            //            Nome = c.String(nullable: false),
            //            Foto = c.String(),
            //            AreaActuacao = c.String(nullable: false),
            //            BancoId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.StartupId)
            //    .ForeignKey("dbo.Bancoes", t => t.BancoId)
            //    .Index(t => t.BancoId);
            
            //CreateTable(
            //    "dbo.Bancoes",
            //    c => new
            //        {
            //            BancoId = c.Int(nullable: false, identity: true),
            //            Conta = c.String(nullable: false),
            //            Iban = c.String(nullable: false),
            //        })
            //    .PrimaryKey(t => t.BancoId);
            
            //CreateTable(
            //    "dbo.Projectoes",
            //    c => new
            //        {
            //            ProjectoId = c.Int(nullable: false, identity: true),
            //            Titulo = c.String(nullable: false),
            //            Foto = c.String(),
            //            Video = c.String(),
            //            Descricao = c.String(),
            //            Sobre = c.String(),
            //            Objectivofundo = c.String(),
            //            Keywords = c.String(),
            //            Meta = c.Double(),
            //            Arrecadado = c.Double(),
            //            DataInicio = c.DateTime(),
            //            DataFim = c.DateTime(),
            //            Duracao = c.Int(),
            //            CapanhaId = c.Int(),
            //            CategoriaId = c.Int(),
            //            Estado = c.Int(),
            //            MembroId = c.Int(nullable: false),
            //            IdLocalizacao = c.Int(),
            //            Startup_StartupId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.ProjectoId)
            //    .ForeignKey("dbo.Campanhas", t => t.CapanhaId)
            //    .ForeignKey("dbo.Categorias", t => t.CategoriaId)
            //    .ForeignKey("dbo.Membroes", t => t.MembroId, cascadeDelete: true)
            //    .ForeignKey("dbo.EnderecoCampanhas", t => t.IdLocalizacao)
            //    .ForeignKey("dbo.Startups", t => t.Startup_StartupId)
            //    .Index(t => t.CapanhaId)
            //    .Index(t => t.CategoriaId)
            //    .Index(t => t.MembroId)
            //    .Index(t => t.IdLocalizacao)
            //    .Index(t => t.Startup_StartupId);
            
            //CreateTable(
            //    "dbo.Campanhas",
            //    c => new
            //        {
            //            CapanhaId = c.Int(nullable: false, identity: true),
            //            Nome = c.String(),
            //        })
            //    .PrimaryKey(t => t.CapanhaId);
            
            //CreateTable(
            //    "dbo.Categorias",
            //    c => new
            //        {
            //            CategoriaId = c.Int(nullable: false, identity: true),
            //            nome = c.String(),
            //        })
            //    .PrimaryKey(t => t.CategoriaId);
            
            //CreateTable(
            //    "dbo.EnderecoCampanhas",
            //    c => new
            //        {
            //            IdLocalizacao = c.Int(nullable: false, identity: true),
            //            EnderecoId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.IdLocalizacao)
            //    .ForeignKey("dbo.Enderecoes", t => t.EnderecoId, cascadeDelete: true)
            //    .Index(t => t.EnderecoId);
            
            //CreateTable(
            //    "dbo.Enderecoes",
            //    c => new
            //        {
            //            EnderecoId = c.Int(nullable: false, identity: true),
            //            IdPais = c.Int(nullable: false),
            //            IdProvincia = c.Int(),
            //            IdMunicipio = c.Int(),
            //            Rua = c.String(nullable: false),
            //        })
            //    .PrimaryKey(t => t.EnderecoId)
            //    .ForeignKey("dbo.Municipios", t => t.IdMunicipio)
            //    .ForeignKey("dbo.Pais", t => t.IdPais, cascadeDelete: true)
            //    .ForeignKey("dbo.Provincias", t => t.IdProvincia)
            //    .Index(t => t.IdPais)
            //    .Index(t => t.IdProvincia)
            //    .Index(t => t.IdMunicipio);
            
            //CreateTable(
            //    "dbo.Municipios",
            //    c => new
            //        {
            //            IdMunicipio = c.Int(nullable: false, identity: true),
            //            Nome = c.String(nullable: false),
            //            IdProvincia = c.Int(),
            //        })
            //    .PrimaryKey(t => t.IdMunicipio)
            //    .ForeignKey("dbo.Provincias", t => t.IdProvincia)
            //    .Index(t => t.IdProvincia);
            
            //CreateTable(
            //    "dbo.Provincias",
            //    c => new
            //        {
            //            IdProvincia = c.Int(nullable: false, identity: true),
            //            Nome = c.String(nullable: false),
            //            IdPais = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.IdProvincia)
            //    .ForeignKey("dbo.Pais", t => t.IdPais, cascadeDelete: true)
            //    .Index(t => t.IdPais);
            
            //CreateTable(
            //    "dbo.Pais",
            //    c => new
            //        {
            //            IdPais = c.Int(nullable: false, identity: true),
            //            Nome = c.String(nullable: false),
            //        })
            //    .PrimaryKey(t => t.IdPais);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "MembroId", "dbo.Membroes");
            DropForeignKey("dbo.Membroes", "StartupId", "dbo.Startups");
            DropForeignKey("dbo.Projectoes", "Startup_StartupId", "dbo.Startups");
            DropForeignKey("dbo.Projectoes", "IdLocalizacao", "dbo.EnderecoCampanhas");
            DropForeignKey("dbo.EnderecoCampanhas", "EnderecoId", "dbo.Enderecoes");
            DropForeignKey("dbo.Enderecoes", "IdProvincia", "dbo.Provincias");
            DropForeignKey("dbo.Enderecoes", "IdPais", "dbo.Pais");
            DropForeignKey("dbo.Enderecoes", "IdMunicipio", "dbo.Municipios");
            DropForeignKey("dbo.Municipios", "IdProvincia", "dbo.Provincias");
            DropForeignKey("dbo.Provincias", "IdPais", "dbo.Pais");
            DropForeignKey("dbo.Projectoes", "MembroId", "dbo.Membroes");
            DropForeignKey("dbo.Projectoes", "CategoriaId", "dbo.Categorias");
            DropForeignKey("dbo.Projectoes", "CapanhaId", "dbo.Campanhas");
            DropForeignKey("dbo.Startups", "BancoId", "dbo.Bancoes");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.Provincias", new[] { "IdPais" });
            DropIndex("dbo.Municipios", new[] { "IdProvincia" });
            DropIndex("dbo.Enderecoes", new[] { "IdMunicipio" });
            DropIndex("dbo.Enderecoes", new[] { "IdProvincia" });
            DropIndex("dbo.Enderecoes", new[] { "IdPais" });
            DropIndex("dbo.EnderecoCampanhas", new[] { "EnderecoId" });
            DropIndex("dbo.Projectoes", new[] { "Startup_StartupId" });
            DropIndex("dbo.Projectoes", new[] { "IdLocalizacao" });
            DropIndex("dbo.Projectoes", new[] { "MembroId" });
            DropIndex("dbo.Projectoes", new[] { "CategoriaId" });
            DropIndex("dbo.Projectoes", new[] { "CapanhaId" });
            DropIndex("dbo.Startups", new[] { "BancoId" });
            DropIndex("dbo.Membroes", new[] { "StartupId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "MembroId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.Pais");
            DropTable("dbo.Provincias");
            DropTable("dbo.Municipios");
            DropTable("dbo.Enderecoes");
            DropTable("dbo.EnderecoCampanhas");
            DropTable("dbo.Categorias");
            DropTable("dbo.Campanhas");
            DropTable("dbo.Projectoes");
            DropTable("dbo.Bancoes");
            DropTable("dbo.Startups");
            DropTable("dbo.Membroes");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
