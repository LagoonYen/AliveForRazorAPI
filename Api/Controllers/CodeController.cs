using AliveStoreTemplate.Common;
using Microsoft.AspNetCore.Mvc;

namespace AliveStoreTemplate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodeController : ControllerBase
    {
        private CodeValidator _codeValidator { get; set; }

        public CodeController(CodeValidator codeValidator)
        {
            _codeValidator = codeValidator;
        }

        [HttpGet]
        public ActionResult<string> Generate()
        {
            string code = _codeValidator.Generate();

            return Ok(code);
        }

        [HttpGet("{code}")]
        public ActionResult Validate(string code)
        {
            bool isOk = _codeValidator.Validate(code);

            return isOk ? Ok() : BadRequest();
        }
    }
}
