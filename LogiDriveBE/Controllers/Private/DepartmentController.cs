using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;
using Microsoft.AspNetCore.Mvc;

namespace LogiDriveBE.Controllers.Private
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentBao _departmentBao;

        public DepartmentController(IDepartmentBao departmentBao)
        {
            _departmentBao = departmentBao;
        }

        [HttpPost]
        public async Task<ActionResult<OperationResponse<Department>>> CreateDepartment([FromBody] Department department)
        {
            var response = await _departmentBao.CreateDepartmentAsync(department);
            return StatusCode(response.Code, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OperationResponse<Department>>> GetDepartmentById(int id)
        {
            var response = await _departmentBao.GetDepartmentByIdAsync(id);
            return StatusCode(response.Code, response);
        }

        [HttpGet]
        public async Task<ActionResult<OperationResponse<IEnumerable<Department>>>> GetAllDepartments()
        {
            var response = await _departmentBao.GetAllDepartmentsAsync();
            return StatusCode(response.Code, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OperationResponse<Department>>> UpdateDepartment(int id, [FromBody] Department department)
        {
            if (id != department.IdDepartment)
            {
                return BadRequest(new OperationResponse<Department>(400, "ID mismatch"));
            }

            var response = await _departmentBao.UpdateDepartmentAsync(department);
            return StatusCode(response.Code, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OperationResponse<bool>>> DeleteDepartment(int id)
        {
            var response = await _departmentBao.DeleteDepartmentAsync(id);
            return StatusCode(response.Code, response);
        }
    }
}
