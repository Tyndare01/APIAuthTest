using DAL.Models;
using DAL.Models.DTO;
using DAL.Models.ViewModel;

namespace BLL.Interfaces;

public interface IUserService
{
    public UserViewModel? Create(CreateUserDto userDto);

    public string? Login(LoginDTO loginDto);

    public UserViewModel? UpdatePassword(int id, ChangePasswordDTO changePasswordDto);

    public UserViewModel? UpdatePhoneNumber(int id, ChangePhoneNumberDTO changePhoneNumberDto);

    public UserViewModel? UpdateDatas(int id, ChangeDataDTO changeDataDto);

    public UserViewModel? UpdateRole(int id, ChangeRoleDTO changeRoleDto);

    public UserViewModel? GetById(int id);

    public UserViewModel? GetByEmail(string Email);

    public IEnumerable<UserViewModel> GetAll();

    public bool Delete(int id);


}
