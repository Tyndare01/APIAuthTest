using DAL.Models;
using DAL.Models.DTO;
using DAL.Models.ViewModel;

namespace DAL.Interfaces;

public interface IUserRepository
{
    // récupère des DTOs et renvoit des viewmodels
    
    public User? Create(User user);
    
    public bool EmailAlreadyUsed(string email);

    public User? GetByEmail(string email);
    



    public User? GetById(int id);

    public IEnumerable<User> GetAll(); // On peut pas avoir un tableau avec des users null

    public User? Update(User user);

    public bool Delete(User user);
}