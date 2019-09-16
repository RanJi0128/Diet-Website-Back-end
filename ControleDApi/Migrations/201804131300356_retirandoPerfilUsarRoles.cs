namespace ControleDApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class retirandoPerfilUsarRoles : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Usuarios", "Perfil_Id", "dbo.Perfil");
            DropIndex("dbo.Usuarios", new[] { "Perfil_Id" });
            DropColumn("dbo.Usuarios", "IdPerfil");
            DropColumn("dbo.Usuarios", "Perfil_Id");
            DropTable("dbo.Perfil");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Perfil",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Usuarios", "Perfil_Id", c => c.Int());
            AddColumn("dbo.Usuarios", "IdPerfil", c => c.Int(nullable: false));
            CreateIndex("dbo.Usuarios", "Perfil_Id");
            AddForeignKey("dbo.Usuarios", "Perfil_Id", "dbo.Perfil", "Id");
        }
    }
}
