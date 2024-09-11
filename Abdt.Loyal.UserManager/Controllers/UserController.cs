using Abdt.Loyal.UserManager.BusinessLogic.Abstractions;
using Abdt.Loyal.UserManager.Domain;
using Abdt.Loyal.UserManager.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            if (userDtoRegister is null)
                return BadRequest();

            var user = _mapper.Map<User>(userDtoRegister);
            var registredUser = await _service.Register(user);

            return Created(new Uri("api/v1/users/", UriKind.Relative), _mapper.Map<UserDto>(registredUser));
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserDtoLogin userDtoLogin)
        {
            if (userDtoLogin is null)
                return BadRequest();

            var jwt = await _service.Login(userDtoLogin.Email, userDtoLogin.PasswordHash);

            if (string.IsNullOrWhiteSpace(jwt))
                return Unauthorized();

            return Ok(jwt);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] UserDtoUpdate userDtoUpdate)
        {
            if (userDtoUpdate is null)
                return BadRequest();

            var user = _mapper.Map<User>(userDtoUpdate);
            var updatedUser = await _service.Update(user);

            return Ok(_mapper.Map<UserDto>(updatedUser));
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            await _service.Delete(id);
            return NoContent();
        }
    }
}
