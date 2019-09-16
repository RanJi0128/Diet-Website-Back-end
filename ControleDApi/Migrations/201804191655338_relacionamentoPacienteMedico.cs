namespace ControleDApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class relacionamentoPacienteMedico : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UsuarioUsuarios",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Ativo = c.Boolean(nullable: false),
                    Medico_Id = c.Int(),
                    Paciente_Id = c.Int(),
                    //  Usuario_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuarios", t => t.Medico_Id)
                .ForeignKey("dbo.Usuarios", t => t.Paciente_Id)
                // .ForeignKey("dbo.Usuarios", t => t.Usuario_Id)
                .Index(t => t.Medico_Id)
                .Index(t => t.Paciente_Id);
               // .Index(t => t.Usuario_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsuarioUsuarios", "Usuario_Id", "dbo.Usuarios");
            DropForeignKey("dbo.UsuarioUsuarios", "Paciente_Id", "dbo.Usuarios");
            DropForeignKey("dbo.UsuarioUsuarios", "Medico_Id", "dbo.Usuarios");
            DropIndex("dbo.UsuarioUsuarios", new[] { "Usuario_Id" });
            DropIndex("dbo.UsuarioUsuarios", new[] { "Paciente_Id" });
            DropIndex("dbo.UsuarioUsuarios", new[] { "Medico_Id" });
            DropTable("dbo.UsuarioUsuarios");
        }
    }
}
