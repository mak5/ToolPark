using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolPark.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BLL.ToolServices;
using Common.Dtos;

namespace ToolPark.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToolsController : ControllerBase
    {
        private readonly ILogger<ToolsController> _logger;
        private readonly IToolService _toolService;

        public ToolsController(ILogger<ToolsController> logger, IToolService toolService)
        {
            _logger = logger;
            _toolService = toolService;
        }

        // GET: api/Tools
        [HttpGet]
        public IEnumerable<ToolDto> Get()
        {
            try
            {
                return _toolService.GetTools();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                throw;
            }
        }

        // GET: api/Tools/5
        [HttpGet("{id}", Name = "Get")]
        public ToolDto Get(string id)
        {
            try
            {
                return _toolService.GetToolById(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                throw;
            }
        }

        // POST: api/Tools
        [HttpPost]
        public void Post([FromBody] ToolDto toolDto)
        {
            try
            {
                _toolService.AddTool(toolDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                throw;
            }
        }

        // PUT: api/Tools/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] ToolDto toolDto)
        {
            try
            {
                _toolService.UpdateTool(id, toolDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                throw;
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            try
            {
                _toolService.DeleteTool(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                throw;
            }
        }

        // GET: api/Tools/IsUnique/5
        [HttpGet("IsUnique/{id}")]
        public bool IsUnique(string id)
        {
            try
            {
                return !_toolService.SerialNumberExist(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                throw;
            }
        }

    }
}
