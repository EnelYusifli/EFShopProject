using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.DataAccess.Migrations
{
    public partial class ShowCanceledInvoices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE usp_GetCanceledInvoiceReport @startTime DateTime,@endTime DateTime
                                    AS

                                    SELECT TotalPrice, CreatedDate
                                    FROM Invoices
                                    WHERE CreatedDate > @startTime AND CreatedDate < @endTime  AND IsCanceled = 1;
                                    EXEC usp_GetCanceledInvoiceReport @startTime, @endTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE usp_GetCanceledInvoiceReport");
        }
    }
}
