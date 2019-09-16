namespace ControleDApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class idIdentity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agendamento",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Hora = c.DateTime(nullable: false),
                        Paciente_Id = c.Int(),
                        Insulina_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuarios", t => t.Paciente_Id)
                .ForeignKey("dbo.Insulina", t => t.Insulina_Id)
                .Index(t => t.Paciente_Id)
                .Index(t => t.Insulina_Id);
            
            CreateTable(
                "dbo.Insulina",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Refeicao",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QtdCarboidrato = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QtdInsulina = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Insulina_Id = c.Int(),
                        Pessoa_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Insulina", t => t.Insulina_Id)
                .ForeignKey("dbo.Usuarios", t => t.Pessoa_Id)
                .Index(t => t.Insulina_Id)
                .Index(t => t.Pessoa_Id);
            
            CreateTable(
                "dbo.AlimentoConsumo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QtdAlimento = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QtdCarboidrato = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QtdInsulina = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Alimento_Id = c.Int(),
                        Refeicao_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Alimento", t => t.Alimento_Id)
                .ForeignKey("dbo.Refeicao", t => t.Refeicao_Id)
                .Index(t => t.Alimento_Id)
                .Index(t => t.Refeicao_Id);
            
            CreateTable(
                "dbo.Alimento",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false),
                        Classificacao = c.Int(nullable: false),
                        Umidade = c.String(),
                        Proteina = c.String(),
                        Lipideos = c.String(),
                        Colesterol = c.String(),
                        Carboidrato = c.String(),
                        FibraAlimentar = c.String(),
                        Cinzas = c.String(),
                        Calcio = c.String(),
                        Magnesio = c.String(),
                        Manganes = c.String(),
                        Fosforo = c.String(),
                        Ferro = c.String(),
                        Sodio = c.String(),
                        Potassio = c.String(),
                        Cobre = c.String(),
                        Zinco = c.String(),
                        Retinol = c.String(),
                        Re = c.String(),
                        Rae = c.String(),
                        Tiamina = c.String(),
                        Riboflavina = c.String(),
                        Piridoxina = c.String(),
                        Niacina = c.String(),
                        VitaminaC = c.String(),
                        Energia_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Energia", t => t.Energia_Id)
                .Index(t => t.Energia_Id);
            
            CreateTable(
                "dbo.Energia",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        kcal = c.String(),
                        kj = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        UsuarioId = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Sobrenome = c.String(),
                        Senha = c.String(),
                        QtdInsulinaPorGramaCarbo = c.Decimal(precision: 18, scale: 2),
                        Cpf = c.Long(nullable: false),
                        Crm = c.Int(),
                        IdPerfil = c.Int(nullable: false),
                        senhaTemporaria = c.Boolean(),
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
                        Perfil_Id = c.Int(),
                    })
                .PrimaryKey(t => t.UsuarioId)
                .ForeignKey("dbo.Perfil", t => t.Perfil_Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Perfil_Id);
            
            CreateTable(
                "dbo.Claims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuarios", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Usuarios", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Perfil",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UsuarioRole",
                c => new
                    {
                        UsuarioId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UsuarioId, t.RoleId })
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId)
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .Index(t => t.UsuarioId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsuarioRole", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Agendamento", "Insulina_Id", "dbo.Insulina");
            DropForeignKey("dbo.UsuarioRole", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.Refeicao", "Pessoa_Id", "dbo.Usuarios");
            DropForeignKey("dbo.Usuarios", "Perfil_Id", "dbo.Perfil");
            DropForeignKey("dbo.Logins", "UserId", "dbo.Usuarios");
            DropForeignKey("dbo.Claims", "UserId", "dbo.Usuarios");
            DropForeignKey("dbo.Agendamento", "Paciente_Id", "dbo.Usuarios");
            DropForeignKey("dbo.Refeicao", "Insulina_Id", "dbo.Insulina");
            DropForeignKey("dbo.AlimentoConsumo", "Refeicao_Id", "dbo.Refeicao");
            DropForeignKey("dbo.Alimento", "Energia_Id", "dbo.Energia");
            DropForeignKey("dbo.AlimentoConsumo", "Alimento_Id", "dbo.Alimento");
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropIndex("dbo.UsuarioRole", new[] { "RoleId" });
            DropIndex("dbo.UsuarioRole", new[] { "UsuarioId" });
            DropIndex("dbo.Logins", new[] { "UserId" });
            DropIndex("dbo.Claims", new[] { "UserId" });
            DropIndex("dbo.Usuarios", new[] { "Perfil_Id" });
            DropIndex("dbo.Usuarios", "UserNameIndex");
            DropIndex("dbo.Alimento", new[] { "Energia_Id" });
            DropIndex("dbo.AlimentoConsumo", new[] { "Refeicao_Id" });
            DropIndex("dbo.AlimentoConsumo", new[] { "Alimento_Id" });
            DropIndex("dbo.Refeicao", new[] { "Pessoa_Id" });
            DropIndex("dbo.Refeicao", new[] { "Insulina_Id" });
            DropIndex("dbo.Agendamento", new[] { "Insulina_Id" });
            DropIndex("dbo.Agendamento", new[] { "Paciente_Id" });
            DropTable("dbo.Roles");
            DropTable("dbo.UsuarioRole");
            DropTable("dbo.Perfil");
            DropTable("dbo.Logins");
            DropTable("dbo.Claims");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Energia");
            DropTable("dbo.Alimento");
            DropTable("dbo.AlimentoConsumo");
            DropTable("dbo.Refeicao");
            DropTable("dbo.Insulina");
            DropTable("dbo.Agendamento");
        }
    }
}
