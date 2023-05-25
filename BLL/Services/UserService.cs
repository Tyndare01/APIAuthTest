using BLL.Interfaces;
using DAL.Enums;
using DAL.Interfaces;
using DAL.Models;
using DAL.Models.DTO;
using DAL.Models.Mapper;
using DAL.Models.ViewModel;

namespace BLL.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;
    

    public UserService(IUserRepository userRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public UserViewModel? Create(CreateUserDto userDto)
    {
        
        return _userRepository.Create(userDto.ToUser())?.ToUserViewModel();
    }

    public string? Login(LoginDTO loginDto)
    {
        if (_userRepository.EmailAlreadyUsed(loginDto.Email)) 
        { 
            User user = _userRepository.GetByEmail(loginDto.Email);
            if (user?.Password == loginDto.Password)
            {

                //Generation du JWT Token sur base d'un user

                return _jwtService.GenerateToken(user);
            }
        }

        return null;
    }

    public UserViewModel? UpdatePassword(int id, ChangePasswordDTO changePasswordDto)
    {
        User? user = _userRepository.GetById(id);
        if (user is not  null && user.Password == changePasswordDto.ActualPassword)
        {
            user.Password = changePasswordDto.NewPassword;
            return _userRepository.Update(user)?.ToUserViewModel();
        }
        return null;
    }


    public UserViewModel? UpdatePhoneNumber(int id, ChangePhoneNumberDTO changePhoneNumberDto)
    {
        User? user = _userRepository.GetById(id);
        if (user is not  null)
        {
            user.PhoneNumber = changePhoneNumberDto.PhoneNumber;
            return _userRepository.Update(user)?.ToUserViewModel();
        }
        return null;
    }

    public UserViewModel? UpdateDatas(int id, ChangeDataDTO changeDataDto)
    {
        User? user = _userRepository.GetById(id);
        if (user is not  null)
        {
            user.Pseudo = changeDataDto.Pseudo;
            user.Firstname = changeDataDto.Firstname;
            user.Lastname = changeDataDto.Lastname;

            return _userRepository.Update(user)?.ToUserViewModel();
        }
        return null;
    }

    public UserViewModel? UpdateRole(int id, ChangeRoleDTO changeRoleDto)
    {
        User? user = _userRepository.GetById(id);

        //Roles? role = changeRoleDto.role;
        // On prend la libreraire Enum et on utilisa la classe statique IsDefined
        // ==> donne 2 caractéristiques : le type de l'elem à tester => typeof(Roles)
        // ==> Ensuite on verifie si le nbr est bin défini dans ce typeofRoles
        // ==> Exemple Typeof(Roles) vaut 0 et 1 (Admin /User) si on met 3 ca renvoit une erreur car 3 n'est pas défini
        if (user is not  null && Enum.IsDefined(typeof(Roles), changeRoleDto.role))
        {
            user.Role = changeRoleDto.role;
            return _userRepository.Update(user)?.ToUserViewModel();
        }
        return null;
    }

    public UserViewModel? GetById(int id)
    {
        return _userRepository.GetById(id)?.ToUserViewModel();
    }

    public UserViewModel? GetByEmail(string Email)
    {
        return _userRepository.GetByEmail(Email)?.ToUserViewModel();
    }

    public IEnumerable<UserViewModel> GetAll()
    {
        return _userRepository.GetAll().ToUserViewModelsList();
    }

    public bool Delete(int id)
    {
        User? user = _userRepository.GetById(id);

        return user is not null? _userRepository.Delete(user) : false;

    }
}
// Transforme en User pour l'envoyer à la DB... touser tomodel (Gavin?)
// Test si c'est nul et renvoit null si c'est null sinon execute la fonction 