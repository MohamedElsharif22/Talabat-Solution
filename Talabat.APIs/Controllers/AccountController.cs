using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Extentions;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services.Interfaces;

namespace Talabat.APIs.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _tokenService = tokenService;
        }
        // Regesteration
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            if (CheckEmailExists(model.Email).Result.Value)
            {
                return BadRequest(new ApiResponse(400, "Email is already Exists !"));
            }

            var user = new AppUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Email.Split('@')[0]
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return BadRequest(result);
            var userDto = new UserDto()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            };

            return Ok(userDto);

        }
        // Login
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto credintal)
        {
            var user = await _userManager.FindByEmailAsync(credintal.Email);
            if (user is null)
                return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, credintal.Password, false);

            if (!result.Succeeded)
                return Unauthorized(new ApiResponse(401));



            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            }); ;
        }

        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var userEamil = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(userEamil);

            return Ok(new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            });
        }

        [Authorize]
        [HttpGet("GetCurrentUserAddress")]
        public async Task<ActionResult<Address>> GetCurrentUserAddress()
        {
            var user = await _userManager.FindUserWithNavigationsAsync(User);

            var address = _mapper.Map<AddressDto>(user?.Address);

            return Ok(address);
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateCurrentUserAddress(AddressDto model)
        {
            var user = await _userManager.FindUserWithNavigationsAsync(User);

            var MappedAddress = _mapper.Map<Address>(model);

            user.Address = MappedAddress;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return BadRequest(new ApiResponse(400));

            return Ok();
        }

        [HttpGet("emailExists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }

    }
}
