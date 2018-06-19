using BasicApiExample.Filter;
using Schroders.Contracts;
using Services.TestModelServices;
using System.Threading.Tasks;
using System.Web.Http;

namespace BasicApiExample.Controllers
{
    // Needs NTLM Authentication on client side
    [Authorize]
    [UniversalExceptionFilter]
    [RoutePrefix("api/testmodels")]
    public class TestModelController : ApiController
    {
        private static ITestModelService _service;
        
        public TestModelController(ITestModelService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("")]
        public async Task<IHttpActionResult> GetAll()
        {
            return Ok(await _service.GetModels());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var result = await _service.GetModel(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] TestModel model)
        {
            return Ok(await _service.Create(model));
        }

        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> Update([FromBody] TestModel model)
        {
            var result = await _service.Update(model);

            if (!result)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Remove(int id)
        {
            var result = await _service.Delete(id);

            if (!result)
                return NotFound();

            return Ok(result);
        }
    }
}
