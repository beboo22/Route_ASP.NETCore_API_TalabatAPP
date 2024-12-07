using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.api.DTOs;
using Talabat.api.Errors;
using Talabat.api.Extension;
using Talabat.Core.Entity.Identity;
using Talabat.Core.ServiceContract.Authentication;

namespace Talabat.api.Controllers
{

    public class AccountController : BaseController
    {
        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signIn,IAuthentication authentication,IMapper map)
        {
            _UserManager = userManager;
            _SignIn = signIn;
            _auth = authentication;
            _map = map;
        }

        public UserManager<AppUser> _UserManager { get; }
        public SignInManager<AppUser> _SignIn { get; }
        public IAuthentication _auth { get; }
        public IMapper _map { get; }


        //[HttpGet("SignInFacbook")]

        //public async Task<IActionResult> LoginWithFacebook(string AccessToken)
        //{
        //    var result = await _authService.LoginWithFacebookAsync(AccessToken);

        //    if (result != null)
        //    {
        //        return Ok(result);
        //    }

        //    return BadRequest(new { message = "Invalid Facebook login." });
        //}







        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> login(LoginCriteriaDto account)
        {
            var user = await _UserManager.FindByEmailAsync(account.Email);
            if(user is null) return Unauthorized(new ApiResponse(401));

            var result = await _SignIn.CheckPasswordSignInAsync(user, account.Password,false);

            if(result.Succeeded is false) return Unauthorized(new ApiResponse(401));

            return Ok(new UserDto()
            {
                Email = user.Email,
                Name = user.DisplayName,
                Token= await _auth.CreateTokenAsync(user,_UserManager)
            });
        }


        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterCriteriaDto model)
        {
            var mapAddress = _map.Map<Address>(model.Address);
            var user = new AppUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                PhoneNumber = model.Phonenumber,
                UserName = model.Email.Split("@")[0],
                Address = mapAddress,
            };

            var result = await _UserManager.CreateAsync(user,model.Password);

            return result.Succeeded is false ? BadRequest(new ApiResponse(400)) :
                                              Ok(new UserDto()
                                              {
                                                  Email = user.Email,
                                                  Name = user.DisplayName,
                                                  Token = await _auth.CreateTokenAsync(user, _UserManager)
                                              });
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //[Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email)??string.Empty;
            var user = await _UserManager.FindByEmailAsync(email);

            return user is null ? BadRequest(new ApiResponse(400)) :
                                              Ok(new UserDto()
                                              {
                                                  Email = user.Email,
                                                  Name = user.DisplayName,
                                                  Token = await _auth.CreateTokenAsync(user, _UserManager)
                                              });
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetUserAddress")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email)??string.Empty;

            /// this will pass null 
            /// bec address is a navigation we should include it while fetching
            ///var user = await _UserManager.FindByEmailAsync(email);
            ///return user is null ? BadRequest(new ApiResponse(400)) :
            ///                                 Ok(new AddressDto()
            ///                                 {
            ///                                     Fname = user.Address.FName,
            ///                                     Lname = user.Address.LName,
            ///                                     Street = user.Address.Street,
            ///                                     City = user.Address.City,
            ///                                     Country = user.Address.Country,
            ///                                 });


            var user = await _UserManager.FindUserAddressByEmailAsync(email);

            return user is null ? BadRequest(new ApiResponse(400)) :
                                             Ok(new AddressDto()
                                             {
                                                 Fname = user.Address.FName,
                                                 Lname = user.Address.LName,
                                                 Street = user.Address.Street,
                                                 City = user.Address.City,
                                                 Country = user.Address.Country,
                                             });



        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("UpadateUserAddress")]
        public async Task<ActionResult<AddressCriteriaDto>> UpadateUserAddress(AddressCriteriaDto addressCriteriaDto)
        {
            //get user email
            var email = User.FindFirstValue(ClaimTypes.Email)??string.Empty;

            var user = await _UserManager.FindUserAddressByEmailAsync(email);

            var Updateaddress = _map.Map<Address>(addressCriteriaDto);

            Updateaddress.Id = user.Address.Id;
            user.Address = Updateaddress;
            var result  = await _UserManager.UpdateAsync(user);

            return result.Succeeded ? Ok(addressCriteriaDto) : BadRequest(new ApiResponse(400));

        }
    }
}
