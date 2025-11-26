using Rozetka.Business.Interfaces;
using Rozetka.Core.Interfaces;
using Rozetka.Core.Models;

namespace Rozetka.Business.Services;

public class UserService :  IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> Create(string firstName, string lastName, string email)
    {
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Name is required");
            
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required");

        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            Email = email
        };
        await _userRepository.Add(user);
        return user;
    }

    public async Task Update(Guid id, string firstName, string lastName, string email)
    {
        var existingUser = await _userRepository.GetById(id);
        if (existingUser == null)
        {
            throw new KeyNotFoundException($"User with id {id} not found");
        }

        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
        {
            throw new ArgumentException("First name is required");
        }
        
        existingUser.FirstName = firstName;
        existingUser.LastName = lastName;
        existingUser.Email = email;
        
        await _userRepository.Update(existingUser);
    }

    public async Task Delete(Guid id)
    {
        await _userRepository.Delete(id);
    }

    public async Task<User?> GetById(Guid id)
    {
        return await _userRepository.GetById(id);
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await _userRepository.GetAll();
    }
}