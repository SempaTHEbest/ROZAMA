using Rozetka.Business.Interfaces;
using Rozetka.Core.Interfaces;
using Rozetka.Core.Models;

namespace Rozetka.Business.Services;

public class ClientService :  IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<IEnumerable<Client>> GetAll()
    {
        return await _clientRepository.GetAll();
    }

    public async Task<Client> GetById(Guid id)
    {
        return await _clientRepository.GetById(id);
    }

    public async Task<Client> Create(string firstName, string lastName, string address)
    {
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
        {
            throw new ArgumentException("First and last name are required");
        }

        var client = new Client
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            Address = address
        };
        
        await _clientRepository.Add(client);
        return client;
    }

    public async Task Update(Guid id, string firstName, string lastName, string address)
    {
        var existingClient = await _clientRepository.GetById(id);
        if (existingClient == null)
        {
            throw new KeyNotFoundException($"Client with id {id} not found");
        }

        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
        {
            throw new ArgumentException("First and last name are required");
        }
        
        existingClient.FirstName = firstName;
        existingClient.LastName = lastName;
        existingClient.Address = address;
        
        await _clientRepository.Update(existingClient);
    }

    public async Task Delete(Guid id)
    {
        await _clientRepository.Delete(id);
    }
}