using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web_Giam_Sat_Massan.Data;
using Web_Giam_Sat_Massan.Models;
using Web_Giam_Sat_Massan.Data;
using System.Globalization;

namespace Web_Giam_Sat_Massan.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly MSProduct _mSProduct;
        public HomeController(ILogger<HomeController> logger, AppDbContext context, MSProduct mSProduct)
        {
            _logger = logger;
            _context = context;
            _mSProduct = mSProduct;
        }

        public IActionResult Index()
        {
            var _shift1 = _context.Shift1.Where(s => s.ID == 1).FirstOrDefault();
            var _shift2 = _context.Shift2.Where(s => s.ID == 1).FirstOrDefault();
            var _shift3 = _context.Shift3.Where(s => s.ID == 1).FirstOrDefault();

            if (_shift1 == null || _shift2 == null || _shift3 == null )
            {
                return NotFound();
            }
            
            var homeViewModel = new HomeViewModel
            {
                plcData = IoT._PlcData,
                s1 = _shift1,
                s2 = _shift2,
                s3 = _shift3,
                mSProduct = _mSProduct,
                isconnect = IoT.isConnected
            };
            return View(homeViewModel);
        }

        public IActionResult GetData()
        {
            var _shift1 = _context.Shift1.Where(s => s.ID == 1).FirstOrDefault();
            var _shift2 = _context.Shift2.Where(s => s.ID == 1).FirstOrDefault();
            var _shift3 = _context.Shift3.Where(s => s.ID == 1).FirstOrDefault();
            var homeViewModel = new HomeViewModel
            {
                plcData = IoT._PlcData,
                s1 = _shift1,
                s2 = _shift2,
                s3 = _shift3,
                mSProduct = _mSProduct,
                isconnect = IoT.isConnected
            };
            return Json(homeViewModel);
        }

        [HttpPost]
        public IActionResult Shift1Record([FromBody] RecordViewModel model)
        {
            var _shift1 = _context.Shift1.Where(s => s.ID == 1).FirstOrDefault();
           
           
            DateTime currentDate = DateTime.Now;
            // Tạo đối tượng DateOnly từ ngày hiện tại
            DateOnly dateOnly = new DateOnly(currentDate.Year, currentDate.Month, currentDate.Day);

            Record newRecord1 = new Record()
            {
                Date = dateOnly,
                Shift = "Ca 1",
                StartTime = _shift1.StartTime,
                EndTime = _shift1.EndTime,
                ShiftLeaderCode = _shift1.ShiftLeaderCode,
                ShiftLeaderName = _shift1.ShiftLeaderName,
                ProductCode = _mSProduct.product11.Code,
                ProductName = _mSProduct.product11.Name,
                Machine = "Máy 1",
                sl = model.sl11,
                slt = model.slt11,
                hs = model.hs11,
                sgbcgv = model.sgbcgv11,
                ptcgv = model.ptcgv11,
                sgbr = model.sgr11,
                ptr = model.ptr11
            };

            Record newRecord2 = new Record()
            {
                Date = dateOnly,
                Shift = "Ca 1",
                StartTime = _shift1.StartTime,
                EndTime = _shift1.EndTime,
                ShiftLeaderCode = _shift1.ShiftLeaderCode,
                ShiftLeaderName = _shift1.ShiftLeaderName,
                ProductCode = _mSProduct.product21.Code,
                ProductName = _mSProduct.product21.Name,
                Machine = "Máy 2",
                sl = model.sl22,
                slt = model.slt22,
                hs = model.hs11,
                sgbcgv = model.sgbcgv22,
                ptcgv = model.ptcgv22,
                sgbr = model.sgr22,
                ptr = model.ptr22
            };

            _context.Add(newRecord1);
            _context.Add(newRecord2);
            _context.SaveChanges();
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult Shift2Record([FromBody] RecordViewModel model)
        {
            var _shift2 = _context.Shift2.Where(s => s.ID == 1).FirstOrDefault();

            DateTime currentDate = DateTime.Now;
            // Tạo đối tượng DateOnly từ ngày hiện tại
            DateOnly dateOnly = new DateOnly(currentDate.Year, currentDate.Month, currentDate.Day);

            Record newRecord1 = new Record()
            {
                Date = dateOnly,
                Shift = "Ca 2",
                StartTime = _shift2.StartTime,
                EndTime = _shift2.EndTime,
                ShiftLeaderCode = _shift2.ShiftLeaderCode,
                ShiftLeaderName = _shift2.ShiftLeaderName,
                ProductCode = _mSProduct.product12.Code,
                ProductName = _mSProduct.product12.Name,
                Machine = "Máy 1",
                sl = model.sl11,
                slt = model.slt11,
                hs = model.hs11,
                sgbcgv = model.sgbcgv11,
                ptcgv = model.ptcgv11,
                sgbr = model.sgr11,
                ptr = model.ptr11
            };

            Record newRecord2 = new Record()
            {
                Date = dateOnly,
                Shift = "Ca 2",
                StartTime = _shift2.StartTime,
                EndTime = _shift2.EndTime,
                ShiftLeaderCode = _shift2.ShiftLeaderCode,
                ShiftLeaderName = _shift2.ShiftLeaderName,
                ProductCode = _mSProduct.product22.Code,
                ProductName = _mSProduct.product22.Name,
                Machine = "Máy 2",
                sl = model.sl22,
                slt = model.slt22,
                hs = model.hs11,
                sgbcgv = model.sgbcgv22,
                ptcgv = model.ptcgv22,
                sgbr = model.sgr22,
                ptr = model.ptr22
            };

            _context.Add(newRecord1);
            _context.Add(newRecord2);
            _context.SaveChanges();
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult Shift3Record([FromBody] RecordViewModel model)
        {
            var _shift3 = _context.Shift3.Where(s => s.ID == 1).FirstOrDefault();
            DateTime currentDate = DateTime.Now;
            // Tạo đối tượng DateOnly từ ngày hiện tại
            DateOnly dateOnly = new DateOnly(currentDate.Year, currentDate.Month, currentDate.Day);

            Record newRecord1 = new Record()
            {
                Date = dateOnly,
                Shift = "Ca 3",
                StartTime = _shift3.StartTime,
                EndTime = _shift3.EndTime,
                ShiftLeaderCode = _shift3.ShiftLeaderCode,
                ShiftLeaderName = _shift3.ShiftLeaderName,
                ProductCode = _mSProduct.product13.Code,
                ProductName = _mSProduct.product13.Name,
                Machine = "Máy 1",
                sl = model.sl11,
                slt = model.slt11,
                hs = model.hs11,
                sgbcgv = model.sgbcgv11,
                ptcgv = model.ptcgv11,
                sgbr = model.sgr11,
                ptr = model.ptr11
            };

            Record newRecord2 = new Record()
            {
                Date = dateOnly,
                Shift = "Ca 3",
                StartTime = _shift3.StartTime,
                EndTime = _shift3.EndTime,
                ShiftLeaderCode = _shift3.ShiftLeaderCode,
                ShiftLeaderName = _shift3.ShiftLeaderName,
                ProductCode = _mSProduct.product23.Code,
                ProductName = _mSProduct.product23.Name,
                Machine = "Máy 2",
                sl = model.sl22,
                slt = model.slt22,
                hs = model.hs11,
                sgbcgv = model.sgbcgv22,
                ptcgv = model.ptcgv22,
                sgbr = model.sgr22,
                ptr = model.ptr22
            };

            _context.Add(newRecord1);
            _context.Add(newRecord2);
            _context.SaveChanges();
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult Start1()
        {
            IoT.Btn(0, 0);
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult Stop1()
        {
            IoT.Btn(0, 1);
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult Reset1()
        {
            IoT.Btn(0, 2);
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult Start2()
        {
            IoT.Btn(1, 0);
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult Stop2()
        {
            IoT.Btn(1, 1);
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult Reset2()
        {
            IoT.Btn(1, 2);
            return Json(new { success = true });
        }
    }
}
