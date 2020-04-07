namespace GerenciadorProcessos.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class incluindoousuariodoRobo : DbMigration
    {
        public override void Up()
        {
            Sql(@"insert into Usuario
values(1, 'robo@teste.com', 'Robo', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 0)");
        }
        
        public override void Down()
        {
        }
    }
}
