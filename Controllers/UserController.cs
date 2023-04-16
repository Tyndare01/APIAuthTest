using System.Security.Claims;
using BLL.Interfaces;
using DAL.Models;
using DAL.Models.DTO;
using DAL.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIAuthTest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

   
    //public ActionResult<ResponseModel<UserViewModel>>
    
    // Récupère des DTOs, chèque leurs validités (exemple : authentification Ok?)
    // et renvoit au serviceUser qui modele le DTO en un objet final qu'il envoit au Ripository
    // Le ripository renvoit l'objet final et le retransforme en viewmodel et le renvoit au contrôleur 

    #region Register
    [HttpPost("register")]
    public ActionResult<UserViewModel> Register(CreateUserDto createDto)
    {
        if (ModelState.IsValid)
        {
            UserViewModel? user = _userService.Create(createDto);

            if (user == null)
            {
                return BadRequest();
            }

            return Ok(user);
        }

        return BadRequest();
    }
    #endregion
    
    #region Login

    [HttpPost("login")]
    public ActionResult<string> Login(LoginDTO loginDto)
    {
        if (ModelState.IsValid)
        {
            string? jwt = _userService.Login(loginDto);

            if (!string.IsNullOrEmpty(jwt)) 
            {
                return Ok(jwt);
            }

        }

        return BadRequest();
    }
    
    #endregion
    
    #region ChangePassword
    [HttpPatch("password/{id})")]
    [Authorize]
    public ActionResult<UserViewModel> ChangePassword(int id, ChangePasswordDTO changePasswordDto)
    {

        if (id.ToString() != User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value)
        {
            return Unauthorized();
        }
        
        // seul l'utilisateur peut changer son mot de passe 
        // (il faut que l'id de l'utilisateur soit le même que celui du token)
        // (le token est un objet qui contient des claims, dont le claim NameIdentifier qui est l'id de l'utilisateur)


        if (ModelState.IsValid)
        {
            UserViewModel? userViewModel = _userService.UpdatePassword(id, changePasswordDto);

            if (userViewModel is not null)
            {
                return Ok(userViewModel);
            }
            
        }

        return BadRequest();

    }
    #endregion
    
    #region ChangePhoneNumber

    [HttpPatch("phone/{id})")]
    [Authorize]
    
    public ActionResult<UserViewModel> ChangePhoneNumber(int id, ChangePhoneNumberDTO changePhoneNumberDto)
    {

        if (id.ToString() != User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value)
        {
            return Unauthorized();
        }
        
        if (ModelState.IsValid)
        {
            UserViewModel? userViewModel = _userService.UpdatePhoneNumber(id, changePhoneNumberDto);

            if (userViewModel is not null)
            {
                return Ok(userViewModel);
            }
            
        }

        return BadRequest();

    }

    #endregion
    
    #region ChangeDatas

    [HttpPatch("data/{id})")]
    [Authorize]
    public ActionResult<UserViewModel> ChangeDatas(int id, ChangeDataDTO changeData)
    {
        
        if (id.ToString() != User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value)
        {
            return Unauthorized();
        }
        
        if (ModelState.IsValid)
        {
            UserViewModel? userViewModel = _userService.UpdateDatas(id, changeData);

            if (userViewModel is not null)
            {
                return Ok(userViewModel);
            }
            
        }

        return BadRequest();

    }

    #endregion

    #region ChangeRole
    [Authorize(Roles = "Admin")]

    [HttpPatch("role/{id})")]
    public ActionResult<UserViewModel> ChangeRole(int id, ChangeRoleDTO changeRole)
    {
        
        
        
        if (ModelState.IsValid)
        {
            UserViewModel? userViewModel = _userService.UpdateRole(id, changeRole);

            if (userViewModel is not null)
            {
                return Ok(userViewModel);
            }
            
        }

        return BadRequest();

    }

    #endregion

    #region GetById

    [HttpGet("{id:int}")]
    [Authorize]
    public ActionResult<UserViewModel> GetById(int id)
    {
        if (ModelState.IsValid)
        {
            UserViewModel? user = _userService.GetById(id);
            return user is not null ? Ok(user) : BadRequest();
        }

        return BadRequest();
    }

    #endregion
    
    #region GetByEmail

    [HttpGet("{email}")]
    [Authorize]
    public ActionResult<UserViewModel> GetByEmail(string email)
    {
        if (ModelState.IsValid)
        {
            UserViewModel? user = _userService.GetByEmail(email);
            return user is not null ? Ok(user) : BadRequest();
        }

        return BadRequest();
    }

    #endregion

    #region GetAll
    [HttpGet]
    [Authorize]
    public ActionResult<IEnumerable<UserViewModel>> GetAll()
    {
        return  Ok(_userService.GetAll());
    }
    #endregion

    #region Delete

    [HttpDelete("{id}")]
    [Authorize]
    public ActionResult Delete(int id)
    {
        
        if (id.ToString() != User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value)
        {
            return Unauthorized();
        }
        
        if (ModelState.IsValid)
        {
            return _userService.Delete(id) ? Ok() : BadRequest();
        }

        return BadRequest();
    }

    #endregion
    
}

