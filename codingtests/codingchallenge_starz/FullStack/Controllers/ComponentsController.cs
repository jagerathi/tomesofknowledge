using System.Threading.Tasks;
using FullStack.Application.Interfaces;
using FullStack.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FullStack.Controllers
{
    /// <summary>
    /// Component API Controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ComponentsController : ControllerBase
    {

        private readonly ILogger<ComponentsController> _logger;
        private readonly IComponentService _componentService;

        public ComponentsController(ILogger<ComponentsController> logger, IComponentService componentService)
        {
            _logger = logger;
            _componentService = componentService;
        }

        /// <summary>
        /// Get Components API Method
        /// </summary>
        /// <param name="listComponentView">List Component View Model</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Get([FromQuery]ListComponentViewModel listComponentView)
        {
            var components = await _componentService.ListComponents(listComponentView);
            return Ok(components);
        }


        //TODO Number 3, Implement the UpdateComponent API
        [HttpPut("updatecomponent/{id}")]
        public async Task<ActionResult> UpdateComponent([FromRoute] int id, [FromQuery]UpdateComponentViewModel updateComponentView)
        {
            await _componentService.UpdateComponent(id, updateComponentView);
            return Ok(id);
        }
    }
}
