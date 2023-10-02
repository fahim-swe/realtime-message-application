using api.Dto;
using api.Helper;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using repository.Entities;
using repository.Interfaces;

namespace api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AccountController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignInDto signInDto)
        {
            if(!ModelState.IsValid) return BadRequest(new {message = "Invalid data"});

            if(await _unitOfWork.UserRepository.ExistsAsync(filter => filter.Email == signInDto.Email))
                return Conflict(new {message = "Email already exists"});

            var user = _mapper.Map<User>(signInDto);
            
            user.PasswordSalt = Utils.GenerateSalt();
            user.PasswordHash = Utils.GeneratePasswordHash(signInDto.Password, user.PasswordSalt);

            var candidateRole = await _unitOfWork.RoleRepository.FindOneAsync(filter => filter.Name == "normal");

            var userRole = new UserRole
            {
                Role = candidateRole,
                UserId = user.Id
            };

            user.UserRoles = new List<UserRole> { userRole };
            _unitOfWork.UserRepository.InsertOneAsync(user);

            return await _unitOfWork.Commit() ?  CreatedAtAction(nameof(SignUp), user) : BadRequest();
        }
    }
}