using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using be_company.Data;
using be_company.Models;

namespace be_company.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees([FromQuery] string? department = null, [FromQuery] string? position = null)
        {
            var employeesQuery = _context.Employees
                                         .Include(e => e.Department)
                                         .Include(e => e.EmployeeType)
                                         .AsQueryable();

            if (!string.IsNullOrEmpty(department))
            {
                employeesQuery = employeesQuery.Where(e => e.Department.DepartmentName.Contains(department));
            }

            if (!string.IsNullOrEmpty(position))
            {
                employeesQuery = employeesQuery.Where(e => e.Position.Contains(position));
            }

            var employees = await employeesQuery.ToListAsync();
            return Ok(employees);
        }

        // GET: api/employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _context.Employees
                                         .Include(e => e.Department)
                                         .Include(e => e.EmployeeType)
                                         .FirstOrDefaultAsync(e => e.EmployeeID == id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // POST: api/employees
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.EmployeeID }, employee);
        }

        // PUT: api/employees/5
        // PUT: api/employees/5
[HttpPut("{id}")]
public async Task<IActionResult> PutEmployee(int id, Employee employee)
{
    // Kiểm tra xem ID trong URL có khớp với ID trong đối tượng employee không
    if (id != employee.EmployeeID)
    {
        return BadRequest("ID không khớp.");
    }

    // Kiểm tra xem nhân viên có tồn tại trong cơ sở dữ liệu không
    var existingEmployee = await _context.Employees
        .Include(e => e.Department)  // Bao gồm thông tin Department nếu cần
        .Include(e => e.EmployeeType) // Bao gồm thông tin EmployeeType nếu cần
        .FirstOrDefaultAsync(e => e.EmployeeID == id);

    if (existingEmployee == null)
    {
        return NotFound("Không tìm thấy nhân viên với ID: " + id);
    }

    // Cập nhật thông tin nhân viên
    existingEmployee.FirstName = employee.FirstName;
    existingEmployee.LastName = employee.LastName;
    existingEmployee.Department = employee.Department;  // Cập nhật thông tin department nếu cần
    existingEmployee.EmployeeType = employee.EmployeeType; // Cập nhật loại nhân viên nếu cần
    existingEmployee.Position = employee.Position;
    existingEmployee.Email = employee.Email;
    existingEmployee.PhoneNumber = employee.PhoneNumber;
    existingEmployee.Address = employee.Address;

    try
    {
        // Lưu thay đổi vào cơ sở dữ liệu
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException ex)
    {
        // Nếu có lỗi trong khi cập nhật, kiểm tra xem nhân viên có tồn tại hay không
        if (!EmployeeExists(id))
        {
            return NotFound("Không tìm thấy nhân viên với ID: " + id);
        }
        else
        {
            // Ném lỗi nếu có sự cố với việc đồng bộ dữ liệu
            return StatusCode(500, "Có lỗi khi cập nhật nhân viên: " + ex.Message);
        }
    }

    // Trả về kết quả sau khi cập nhật thành công
    return NoContent();
}

        // DELETE: api/employees/5
       [HttpDelete("{id}")]
public async Task<IActionResult> DeleteEmployee(int id)
{
    var employee = await _context.Employees.FindAsync(id);
    if (employee == null)
    {
        return NotFound();
    }

    _context.Employees.Remove(employee);
    await _context.SaveChangesAsync();

    return NoContent();
}


        // Kiểm tra xem nhân viên có tồn tại trong cơ sở dữ liệu hay không
       private bool EmployeeExists(int id)
{
    return _context.Employees.Any(e => e.EmployeeID == id);
}

    }
}
