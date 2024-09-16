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
        private readonly IUserService<User> _service;
        private readonly IMapper _mapper;

        public UserController(IUserService<User> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddUser([FromBody] UserDtoRegister userDtoRegister)
        {
            var user = _mapper.Map<User>(userDtoRegister);
            var result = await _service.Add(user);

            if (!result.IsSuccess)
                return Conflict(result.Error);

            return Created(new Uri("api/v1/users/", UriKind.Relative), _mapper.Map<UserDto>(result.Value));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] long id)
        {
            if (id < 0)
                return BadRequest();

            var result = await _service.Get(id);
            if (!result.IsSuccess)
                return NotFound(result.Error);

            return Ok(_mapper.Map<UserDto>(result.Value));
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
        [Route("delete")]
        public async Task<IActionResult> Delete([FromQuery] long id)
        {
            if (id < 0)
                return BadRequest();

            await _service.Delete(id);
            return NoContent();
        }
    }
}
