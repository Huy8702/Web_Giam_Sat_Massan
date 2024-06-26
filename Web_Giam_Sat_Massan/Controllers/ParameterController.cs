using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web_Giam_Sat_Massan.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.CodeAnalysis;
using Web_Giam_Sat_Massan.Models;

namespace Web_Giam_Sat_Massan.Controllers
{
    public class ParameterController : Controller
    {
        private readonly AppDbContext _context;
        private readonly MSProduct _MSProduct;
        
        public ParameterController(AppDbContext context,MSProduct mSProduct)
        {
            _context = context;
            _MSProduct =  mSProduct;
        }

        public ActionResult Index(int? selectedMachineId, int? selectedShiftId, int? selectedProductId)
        {
            // Lấy danh sách lines từ cơ sở dữ liệu hoặc một nguồn dữ liệu nào đó
            
            var shifts = GetShifts();
            var machines = GetMachines();
            var products = GetProducts();

            
            ViewBag.Shifts = new SelectList(shifts,"ID", "Name");
            ViewBag.Machines = new SelectList(machines, "ID", "Name");           
            ViewBag.Products = products;

            ViewBag.Shifts = new SelectList(shifts, "ID", "Name", selectedShiftId);
            ViewBag.Machines = new SelectList(machines, "ID", "Name", selectedMachineId);
            ViewBag.Products = products; // Truyền danh sách sản phẩm vào ViewBag
            ViewBag.SelectedProductId = selectedProductId; // Truyền ID sản phẩm được chọn vào ViewBag

            return View();

        }

        [HttpPost]
        public ActionResult ShiftUpdate(int selectedMachineId, int selectedShiftId, int selectedProductId)
        {
            // Xử lý logic khi nhận được giá trị selectedLineId
            // Ví dụ: Lưu vào cơ sở dữ liệu hoặc thực hiện hành động khác
            var product = _context.Product.FirstOrDefault(p => p.ID == selectedProductId);
            if (product != null)
            {
                switch (selectedShiftId)
                {
                    case 1:
                        if (selectedMachineId == 1)
                        {
                            _MSProduct.product11 = product;
                        }

                        if (selectedMachineId == 2)
                        {
                            _MSProduct.product21 = product;
                        }
                        break;
                    case 2:
                        if (selectedMachineId == 1)
                        {
                            _MSProduct.product12 = product;
                        }
                        if (selectedMachineId == 2)
                        {
                            _MSProduct.product22 = product;
                        }
                        break;
                    case 3:
                        if (selectedMachineId == 1)
                        {
                            _MSProduct.product13 = product;
                        }
                        if (selectedMachineId == 2)
                        {
                            _MSProduct.product23 = product;
                        }
                        break;
                }
            }

            ChooseDB(selectedMachineId);
            return RedirectToAction("Index", new { selectedMachineId, selectedShiftId, selectedProductId });
        }

        [HttpPost]
        public ActionResult PlcDataWriteUpdate(Web_Giam_Sat_Massan.Models.PlcDataWrite11 model, int selectedMachineId, int selectedShiftId, int selectedProductId)
        {
            IoT.WirteData(model);
            return RedirectToAction("Index", new { selectedMachineId, selectedShiftId, selectedProductId });
        }
        

        private IEnumerable<Machine> GetMachines()
        {
            return _context.Machine.ToList();
        }

        private IEnumerable<Shift> GetShifts() 
        {
            return _context.Shift.ToList();
        }

        private IEnumerable<Product> GetProducts()
        {
            // Thay thế bằng dữ liệu thực tế từ cơ sở dữ liệu
            return _context.Product.ToList();
        
        }

        public void ChooseDB(int machineId)
        {
            
            switch (machineId)
            {
                case 1:
                    IoT.writedb = 13;
                    break;
                case 2:
                    IoT.writedb = 14;
                    break;
            }
            
        }
    }
}
