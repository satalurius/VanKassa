using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using VanKassa.Backend.Core.Helpers;
using VanKassa.Backend.Core.Services.Interface.AdminDashboard;
using VanKassa.Backend.Infrastructure.Data;
using VanKassa.Domain.Dtos.AdminDashboard.Orders;
using VanKassa.Shared.Data.Helpers;

namespace VanKassa.Backend.Core.Services.AdminDashboard;
public class AdminDashboardReportsService : IAdminDashboardReportsService
{
    private readonly IDbContextFactory<VanKassaDbContext> dbContextFactory;

    public AdminDashboardReportsService(IDbContextFactory<VanKassaDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public async Task<OrdersReportDto> GenerateOrdersReportAsync()
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var orders = await dbContext.Orders
            .Include(order => order.OrderProducts)
            .ThenInclude(orderProduct => orderProduct.Product)
            .ThenInclude(product => product.Category)
            .Include(order => order.Outlet)
            .OrderByDescending(order => order.Date)
            .ToListAsync();

        var reportName = $"Отчет по продажам за {DateTime.Now.Date.ToShortDateString()}";

        var workBook = await Task.Run(() =>
        {
            var workBook = new XLWorkbook();
            var workSheet = workBook.Worksheets.Add("Отчет");

            workSheet.Cell("A1").Value = reportName;
            workSheet.Range("A1:H1").Row(1).Merge();
            workSheet.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            workSheet.Cell("A3").Value = "Идентификатор";
            workSheet.Cell("B3").Value = "Дата";
            workSheet.Cell("C3").Value = "Статус";
            workSheet.Cell("D3").Value = "Цена";
            workSheet.Cell("E3").Value = "Филиал";
            workSheet.Cells("F3").Value = "Товары";
            workSheet.Range("F3:G3").Row(1).Merge();
            workSheet.Cell("F3").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            workSheet.Cell("F4").Value = "Наименование";
            workSheet.Cell("G4").Value = "Категория";
            workSheet.Cell("H4").Value = "Цена товара";
            var titleRows = workSheet.Cells($"A1:H1, A2:H2, A3:H3, A4:H4").Style;
            titleRows.Border.SetTopBorder(XLBorderStyleValues.Thin);
            titleRows.Border.SetRightBorder(XLBorderStyleValues.Thin);
            titleRows.Border.SetBottomBorder(XLBorderStyleValues.Thin);
            titleRows.Border.SetLeftBorder(XLBorderStyleValues.Thin);

            var valuesStartCell = 5;

            var previousProductsCell = 0;

            for (var i = 0; i < orders.Count; i++)
            {
                if (orders[i].OrderProducts.Count() == 0)
                {
                    continue;
                }

                var cell = valuesStartCell + previousProductsCell;
                workSheet.Cell(cell, 1).Value = orders[i].OrderId.ToString();
                workSheet.Cell(cell, 2).Value = orders[i].Date.ToLongDateString();
                workSheet.Cell(cell, 3).Value = OrdersHelper.GetCanceledStringFromValue(orders[i].Canceled);
                workSheet.Cell(cell, 4).Value = orders[i].Price;
                workSheet.Cell(cell, 5).Value = OutletHelper.BuildOutletNameByAddresses(orders[i].Outlet.City, orders[i].Outlet.Street, orders[i].Outlet.StreetNumber);

                var products = orders[i].OrderProducts.Select(order => order.Product).ToList();

                var productCount = 0;

                foreach (var product in products)
                {
                    var productCell = cell + productCount;

                    workSheet.Cell(productCell, 6).Value = product.Name;
                    workSheet.Cell(productCell, 7).Value = product.Category.Name;
                    workSheet.Cell(productCell, 8).Value = product.Price;

                    var productRows = workSheet.Cells($"A{productCell}:H{productCell}").Style;
                    productRows.Border.SetTopBorder(XLBorderStyleValues.Thin);
                    productRows.Border.SetRightBorder(XLBorderStyleValues.Thin);
                    productRows.Border.SetBottomBorder(XLBorderStyleValues.Thin);
                    productRows.Border.SetLeftBorder(XLBorderStyleValues.Thin);

                    productCount++;
                }

                previousProductsCell += productCount;
                
                var rows = workSheet.Cells($"A{cell}:H{cell}").Style;
                rows.Border.SetTopBorder(XLBorderStyleValues.Thin);
                rows.Border.SetRightBorder(XLBorderStyleValues.Thin);
                rows.Border.SetBottomBorder(XLBorderStyleValues.Thin);
                rows.Border.SetLeftBorder(XLBorderStyleValues.Thin);
            }

            return workBook;
        });

        using var stream = new MemoryStream();
        workBook.SaveAs(stream);

        var content = stream.ToArray();

        return new OrdersReportDto
        {
            Content = content,
            ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            FileName = $"{reportName}.xlsx"
        };
    }

}
