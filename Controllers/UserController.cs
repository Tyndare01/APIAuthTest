using BLL.Interfaces;
using DAL.Models;
using DAL.Models.DTO;
using DAL.Models.ViewModel;
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
    [HttpPost]
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


}