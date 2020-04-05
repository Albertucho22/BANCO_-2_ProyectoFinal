using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Services
{
  public class ClientService
  {
    private readonly Core2DbContext _context;

    public ClientService(Core2DbContext context)
    {
      _context = context;
    }

    public async Task<List<Client>> Get() =>
      await _context.Clients.ToListAsync();

    public async Task<Client> Get(int id)
    {
      var client = await _context.Clients.FindAsync(id);
      if (client == null) throw new Exception("No Client found with given Id.");

      return client;
    }

    public async Task<Client> Update(int id, Client client) {

      if (id != client.Id) throw new Exception("URL Id is not equal to given Client Id.");

      _context.Clients.Update(client);

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (Exception)
      {
        if (!ClientExists(id)) throw new Exception("No client found with given Id.");
        throw;
      }

      return await Get(id);
    }

    private bool ClientExists(int id) =>
      _context.Clients.Any(e => e.Id == id);

        public bool ClientExists(string UserName) =>
      _context.Clients.Any(e => e.UserName == UserName);

        public async Task<Client> Create(Client client) {
      _context.Clients.Add(client);
      await _context.SaveChangesAsync();
      return client;
    }

    public async Task<Client> Remove(int id) {
      var client = await _context.Clients.FindAsync(id);
      if (client == null) throw new Exception("No Client found with given Id.");

      try {
        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
        return client;
      } catch(Exception) {
        throw;
      }
    }
  }
}