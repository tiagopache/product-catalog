namespace Catalog.Model.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitializeDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Tipo = c.String(),
                    DataEnvio = c.DateTime(nullable: false),
                    IdProduto = c.Int(nullable: false),
                    IdSku = c.Int(nullable: false),
                    CreatedOn = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedOn = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                    DeletedOn = c.DateTime(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Products",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    IdParceiro = c.Int(nullable: false),
                    SKU = c.String(nullable: false, maxLength: 128),
                    Nome = c.String(),
                    NomeReduzido = c.String(),
                    Modelo = c.String(),
                    Ean = c.String(),
                    Url = c.String(),
                    ForaDeLinha = c.Boolean(nullable: false),
                    Preco = c.Decimal(nullable: false, precision: 18, scale: 2),
                    PrecoDe = c.Decimal(precision: 18, scale: 2),
                    Disponivel = c.Boolean(nullable: false),
                    Estoque = c.Int(nullable: false),
                    Ativo = c.Boolean(nullable: false),
                    Altura = c.Double(),
                    Largura = c.Double(),
                    Comprimento = c.Double(),
                    Peso = c.Double(),
                    CreatedOn = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedOn = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                    DeletedOn = c.DateTime(),
                })
                .PrimaryKey(t => new { t.Id, t.IdParceiro, t.SKU });
        }

        public override void Down()
        {
            DropTable("dbo.Products");
            DropTable("dbo.Notifications");
        }
    }
}
