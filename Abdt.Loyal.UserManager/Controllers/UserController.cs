using Abdt.Loyal.UserManager.BusinessLogic.Abstractions;
using Abdt.Loyal.UserManager.Domain;
using Abdt.Loyal.UserManager.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Abdt.Loyal.UserManager.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UserController : ControllerBase
    {
        private readonly IAccountManager<User> _service;
        private readonly IMapper _mapper;

        public UserController(IAccountManager<User> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserDtoRegister userDtoRegister)
        {
            var user = _mapper.Map<User>(userDtoRegister);
            var result = await _service.Register(user);

            if (!result.IsSuccess)
                return Conflict(result.Error);

            return Created(new Uri("api/v1/users/", UriKind.Relative), _mapper.Map<UserDto>(result.Value));
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserDtoLogin userDtoLogin)
        {
            var result = await _service.Login(userDtoLogin.Email, userDtoLogin.PasswordHash);
            if (!result.IsSuccess)
                return Unauthorized(result.Error);

            return Ok(result.Value);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] UserDtoUpdate userDtoUpdate)
        {
            var user = _mapper.Map<User>(userDtoUpdate);
            var result = await _service.Update(user);

            if (!result.IsSuccess)
                return NotFound(result.Error);

            return Ok(_mapper.Map<UserDto>(result.Value));
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            if (id < 0)
                return BadRequest();

            await _service.Delete(id);
            return NoContent();
        }
    }
}
