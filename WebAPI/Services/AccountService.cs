using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;
using System.Linq;

namespace WebAPI.Services
{
  public class AccountService
  {
    private readonly Core2DbContext _context;

    public AccountService(Core2DbContext context)
    {
      _context = context;
    }

    public async Task<List<Account>> Get() =>
       await _context.Accounts.ToListAsync();

    public async Task<Account> Get(int id)
    {
      var account = await _context.Accounts.FindAsync(id);

      if (account == null) throw new Exception("No Account found with given Id.");

      return account;
    }

    public async Task<List<Account>> GetClientAccounts(int clientId)  {
      return await _context.Accounts.Where(acc => acc.ClientId == clientId).ToListAsync();
    }

    public async Task<Account> Update(int id, Account account)
    {
      if (id != account.Id) throw new Exception("URL Id is not equal to given Account Id.");

      _context.Accounts.Update(account);

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (System.Exception)
      {
        if (!AccountExists(id)) throw new Exception("No account found with given Id.");
        throw;
      }

      return await Get(id);
    }

    private bool AccountExists(int id) =>
      _context.Accounts.Any(e => e.Id == id);

    public async Task<Account> Create(Account account) {
      // var client = await _clientService.Get(account.ClientId);

      // if (client == null) throw new Exception("A Client with the give Id does not exists.");

      _context.Accounts.Add(account);
      await _context.SaveChangesAsync();
      return account;
    }

    public async Task<Account> Remove(int id)
    {
      var account = await _context.Accounts.FindAsync(id);
      if (account == null) throw new Exception("No Account found with given Id.");

      try
      {
        _context.Accounts.Remove(account);
        await _context.SaveChangesAsync();

        return account;
      }
      catch (Exception)
      {
        throw;
      }
    }
  }
}