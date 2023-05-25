using System.Reflection.Metadata.Ecma335;
using DAL.Models.DTO;
using DAL.Models.ViewModel;

namespace DAL.Models.Mapper;

public  static class UserMapper
{
    public static UserViewModel ToUserViewModel(this User user)
    {
        return new UserViewModel
        {
            Id = user.Id,
            Email = user.Email,
            Firstname = user.Firstname,
            Lastname = user.Lastname,
            PhoneNumber = user.PhoneNumber,
            Pseudo = user.Pseudo,
            Role = user.Role
        };
        
    }

    public static User ToUser(this CreateUserDto userdto)
    {
        return new User(userdto.Pseudo, userdto.Email, userdto.Password);
    }

    public static IEnumerable<UserViewModel> ToUserViewModelsList(this IEnumerable<User> users)
    {
        List<UserViewModel> list = new List<UserViewModel>();
        foreach (var user in users)
        {
            list.Add(user.ToUserViewModel());

        }
        return list;
    }
}