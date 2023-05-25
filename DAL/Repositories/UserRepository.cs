using DAL.Context;
using DAL.Interfaces;
using DAL.Models;
using DAL.Models.DTO;
using DAL.Models.ViewModel;

namespace DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AuthDbContext _context;

    public UserRepository(AuthDbContext authDbContext)
    {
        _context = authDbContext;
    }

    public User? Create(User user)
    {
        try
        {
            _context.Add(user);
            _context.SaveChanges();
            return user;

        }
        catch (Exception e)
        {
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.WriteLine(e);
            Console.ResetColor();
            return null;
        }
    }

    public bool EmailAlreadyUsed(string email)
    {
        return _context.Users.Any(u => u.Email == email);
        // Est-ce que un User à déjà cette Email ? 
    }

    public User? GetByEmail(string email)
    {
        try
        {
            return _context.Users.First(u => u.Email == email);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return (null);
        }
    }

    

    public User? GetById(int id)
    {
        try
        {
            return _context.Users.First(u => u.Id == id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return (null);
        }
    }

    public IEnumerable<User> GetAll()
    {
        return _context.Users.ToList();
    }

    public User? Update(User user)
    {
        try
        {
            _context.Update(user);
            _context.SaveChanges();
            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public bool Delete(User user)
    {
        try
        {
            _context.Remove(user);
            _context.SaveChanges();
            return true;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
            
        }
        
    }
}