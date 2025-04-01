using Esms.Business.Models;
using Esms.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Esms.API.Controllers.Generic
{
    [Authorize(Roles = "Admin")]
    public class BaseController<T> : ControllerBase where T : class
    {
        private readonly IServiceBase<T> _service;
        private readonly ILogger<BaseController<T>> _logger;

        public BaseController(IServiceBase<T> service, ILogger<BaseController<T>> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<T>>> GetAll()
        {
            _logger.LogInformation("Fetching all {Entity}", typeof(T).Name);
            var entities = await _service.GetAll();
            return Ok(entities);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<T>> GetById(int id)
        {
            _logger.LogInformation("Fetching {Entity} with ID {Id}", typeof(T).Name, id);
            var entity = await _service.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult<T>> Create([FromBody] T entity)
        {
            _logger.LogInformation("Creating new {Entity}", typeof(T).Name);
            var createdEntity = await _service.AddAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = createdEntity }, createdEntity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] T entity)
        {
            _logger.LogInformation("Updating {Entity} with ID {Id}", typeof(T).Name, id);
            if (id == 0) return BadRequest("Invalid ID");
            await _service.UpdateAsync(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting {Entity} with ID {Id}", typeof(T).Name, id);
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }

    [Route("Generic/AddressType")]
    public class AddressTypeController : BaseController<AddressType>
    {
        public AddressTypeController(IServiceBase<AddressType> service, ILogger<BaseController<AddressType>> logger)
            : base(service, logger)
        {
        }
    }

    [Route("Generic/Employee")]
    public class EmployeeController : BaseController<Employee>
    {
        public EmployeeController(IServiceBase<Employee> service, ILogger<BaseController<Employee>> logger)
            : base(service, logger)
        {
        }
    }

    [Route("Generic/EmployeesAddresses")]
    public class EmployeesAddressesController : BaseController<EmployeesAddresses>
    {
        public EmployeesAddressesController(IServiceBase<EmployeesAddresses> service, ILogger<BaseController<EmployeesAddresses>> logger)
            : base(service, logger)
        {
        }
    }

    /// <summary>
    /// Deletes a specific TodoItem.
    /// </summary>
    /// <remarks>Remarks here</remarks>
    [Route("Generic/Address")]
    public class AddressController : BaseController<Address>
    {
        public AddressController(IServiceBase<Address> service, ILogger<BaseController<Address>> logger)
            : base(service, logger)
        {
        }
    }
}
