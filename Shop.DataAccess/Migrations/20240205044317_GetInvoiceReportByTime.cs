using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.DataAccess.Migrations
{
    public partial class GetInvoiceReportByTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE usp_GetInvoiceReport @startTime DateTime,@endTime DateTime
                                    AS

                                    SELECT Id,TotalPrice,CreatedDate FROM Invoices
                                    WHERE CreatedDate>@startTime AND CreatedDate<@endTime
                                    EXEC usp_GetInvoiceReport @startTime, @endTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE usp_GetInvoiceReport");
        }
    }
}
