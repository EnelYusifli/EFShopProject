using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.DataAccess.Migrations
{
    public partial class TheMostAddedProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE usp_GetTheMostAddedProducts @count int
                                    AS 

                                    SELECT TOP (@count)
                                        p.Name,
                                        COUNT(cp.ProductId) AS ProductCount
                                    FROM 
                                        Products p
                                    JOIN 
                                        CartProducts cp ON p.Id = cp.ProductId
                                    GROUP BY 
                                        p.Name
                                    ORDER BY 
                                        ProductCount DESC

                                    EXEC usp_GetTheMostAddedProducts @count");
                                
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE usp_GetTheMostAddedProducts");
        }
    }
}
