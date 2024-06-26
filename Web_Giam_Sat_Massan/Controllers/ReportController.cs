using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Web_Giam_Sat_Massan.Data;
using Web_Giam_Sat_Massan.Models;

using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Table = DocumentFormat.OpenXml.Wordprocessing.Table;
using DocumentFormat.OpenXml;


namespace Web_Giam_Sat_Massan.Controllers
{
    public class ReportController : Controller
    {
        private readonly AppDbContext _context;

        public ReportController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new ReportViewModel();
            model.StartDate = DateTime.Today;
            model.EndDate = DateTime.Today;
            return View(model);
        }

        [HttpPost]
        public IActionResult GenerateReport(ReportViewModel model)
        {
            // Lấy dữ liệu từ cơ sở dữ liệu dựa trên các thông tin trong model
            DateOnly startdate = new DateOnly(model.StartDate.Year, model.StartDate.Month, model.StartDate.Day);
            DateOnly enddate = new DateOnly(model.EndDate.Year, model.EndDate.Month, model.EndDate.Day);
            var records = _context.Record
                .Where(r => r.Date >= startdate && r.Date <= enddate);

            if (!string.IsNullOrEmpty(model.Shift))
            {
                records = records.Where(r => r.Shift == model.Shift);
            }

            if (!string.IsNullOrEmpty(model.Machine))
            {
                records = records.Where(r => r.Machine == model.Machine);
            }

            model.Records = records.ToList() ?? new List<Record>();
            return View("Report", model);
        }

        
        [HttpPost]
        public IActionResult ExportToWord([FromBody] ReportViewModel model)
        {
            if (model?.Records == null || !model.Records.Any())
            {
                return BadRequest("No data to export");
            }

            var reportData = GenerateWordReport(model);

            return File(reportData, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "Report.docx");
        }



        public IActionResult ExportToPDF()
        {
            // Logic xuất báo cáo ra PDF
            var model = new ReportViewModel(); // Thay bằng logic thực tế để lấy dữ liệu
            byte[] reportData = GeneratePDFReport(model);

            // Trả về file PDF
            return File(reportData, "application/pdf", "Report.pdf");
        }

        // Hàm sinh báo cáo Word (ví dụ)
        private byte[] GenerateWordReport(ReportViewModel model)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var wordDocument = WordprocessingDocument.Create(memoryStream, WordprocessingDocumentType.Document, true))
                {
                    var mainPart = wordDocument.AddMainDocumentPart();
                    mainPart.Document = new Document();
                    var body = mainPart.Document.AppendChild(new Body());

                    var table = new Table();

                    var props = new TableProperties(
                        new TableBorders(
                            new TopBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 6 },
                            new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 6 },
                            new LeftBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 6 },
                            new RightBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 6 },
                            new InsideHorizontalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 6 },
                            new InsideVerticalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 6 }
                        )
                    );
                    table.AppendChild(props);

                    // Add header row
                    var headerRow = new TableRow();
                    headerRow.Append(
                        CreateTableCell("STT", 10),
                        CreateTableCell("Ngày", 10),
                        CreateTableCell("Ca", 10),
                        CreateTableCell("Thời gian bắt đầu", 10),
                        CreateTableCell("Thời gian kết thúc", 10),
                        CreateTableCell("Mã trưởng ca", 10),
                        CreateTableCell("Trưởng ca", 10),
                        CreateTableCell("Mã sản phẩm", 10),
                        CreateTableCell("Tên sản phẩm", 10),
                        CreateTableCell("Máy", 10),
                        CreateTableCell("Số lượng", 10),
                        CreateTableCell("Số lượng gói tốt", 10),
                        CreateTableCell("Hiệu suất", 10),
                        CreateTableCell("Số gói bị cấn gia vị", 10),
                        CreateTableCell("Phần trăm cấn gia vị", 10),
                        CreateTableCell("Số gói rỗng", 10),
                        CreateTableCell("Phần trăm rỗng", 10)
                    );
                    table.Append(headerRow);

                    // Add data rows
                    foreach (var record in model.Records)
                    {
                        var row = new TableRow();
                        row.Append(
                            CreateTableCell(record.ID.ToString(), 10),
                            CreateTableCell(record.Date.ToShortDateString(), 10),
                            CreateTableCell(record.Shift, 10),
                            CreateTableCell(record.StartTime.ToString(), 10),
                            CreateTableCell(record.EndTime.ToString(), 10),
                            CreateTableCell(record.ShiftLeaderCode, 10),
                            CreateTableCell(record.ShiftLeaderName, 10),
                            CreateTableCell(record.ProductCode, 10),
                            CreateTableCell(record.ProductName, 10),
                            CreateTableCell(record.Machine, 10),
                            CreateTableCell(record.sl.ToString(), 10),
                            CreateTableCell(record.slt.ToString(), 10),
                            CreateTableCell(record.hs.ToString(), 10),
                            CreateTableCell(record.sgbcgv.ToString(), 10),
                            CreateTableCell(record.ptcgv.ToString(), 10),
                            CreateTableCell(record.sgbr.ToString(), 10),
                            CreateTableCell(record.ptr.ToString(), 10)
                        );
                        table.Append(row);
                    }

                    body.Append(table);
                    mainPart.Document.Save();
                }

                return memoryStream.ToArray();
            }
        }

        private TableCell CreateTableCell(string text, int fontSize)
        {
            var runProperties = new RunProperties();
            runProperties.Append(new FontSize { Val = (fontSize * 2).ToString() }); // OpenXML font size is in half-points

            var run = new Run();
            run.Append(runProperties);
            run.Append(new Text(text));

            var paragraph = new Paragraph();
            paragraph.Append(run);

            var tableCell = new TableCell();
            tableCell.Append(paragraph);

            return tableCell;
        }



        // Hàm sinh báo cáo PDF (ví dụ)
        private byte[] GeneratePDFReport(ReportViewModel model)
        {
            // Thực hiện logic sinh báo cáo PDF và trả về dữ liệu dưới dạng byte array
            MemoryStream ms = new MemoryStream();
            // Đoạn code sinh báo cáo PDF ở đây
            return ms.ToArray();
        }
    }
}
