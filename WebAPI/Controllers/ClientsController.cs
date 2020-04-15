using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Services;
using WebAPI.Models;

namespace WebAPI.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class ClientsController : ControllerBase {
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    private readonly ClientService _clientService;

    public ClientsController(ClientService clientService) {
      _clientService = clientService;
    }

    // GET: api/Clients
    [HttpGet]
    public async Task<ActionResult<List<Client>>> GetClients() {
      try {
        List<Client> clients = await _clientService.Get();
        log.Info($"Get {clients.Count} clients");
        return Ok(clients);
      } catch (System.Exception e) {
        log.Error(e);
        return BadRequest(new {
          error = new {
            message = e.Message
          }
        });
      }
    }

    // GET: api/Clients/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Client>> GetClient(int id) {
      try {
        var client = await _clientService.Get(id);
        if (client == null) {
          log.Warn($"Client {id} not found");
          return NotFound();
        }
        log.Info($"Get client {id}");
        return client;
      } catch (System.Exception e) {
        log.Error(e);
        return BadRequest(new {
          error = new {
            message = e.Message
          }
        });
      }
    }

    // GET: api/Clients/5/Accounts
    [HttpGet("{id}/Accounts")]
    public async Task<ActionResult<List<Account>>> GetAccountsByClient(int id, [FromServices] AccountService _accountService) {
      try {
        var accounts = await _accountService.GetClientAccounts(id);
        log.Info($"Get {accounts.Count} accounts tied to Client {id}");
        return accounts;
      } catch (System.Exception e) {
        log.Error(e);
        return BadRequest(new {
          error = new {
            message = e.Message
          }
        });
      }
    }

    // PUT: api/Clients/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    [HttpPut("{id}")]
    public async Task<ActionResult<Client>> PutClient(int id, ClientUpdateModel clientModel) {
      try {
        var updatedClient = await _clientService.Update(id, clientModel);
        log.Info($"Client {id} has been updated.");
        return updatedClient;
      } catch (System.Exception e) {
        log.Error(e);
        return BadRequest(new { error = new { message = e.Message } });
      }
    }

    // POST: api/Clients
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    [HttpPost]
    public async Task<ActionResult<Client>> PostClient(Client client) {
      try {
        var createdClient = await _clientService.Create(client);
        log.Info($"Client {createdClient.Id} has been created.");
        return createdClient;
      } catch (Exception e) {
        log.Error(e);
        return BadRequest(new { error = new { message = e.Message } });
      }
    }

    // DELETE: api/Clients/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Client>> DeleteClient(int id) {
      try {
        var deletedClient = await _clientService.Remove(id);
        log.Info($"Client {id} has been deleted.");
        return deletedClient;
      } catch (Exception e) {
        log.Error(e);
        return BadRequest(new { error = new { message = e.Message } });
      }
    }
  }
}
