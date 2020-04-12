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
                log.Info("All clients geted.");
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
        if (client == null) return NotFound();
        log.Info("Getted client with specific ID");
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
        return await _accountService.GetClientAccounts(id);
      } catch (System.Exception e) {

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
    public async Task<ActionResult<Client>> PutClient(int id, Client client) {
      try {
                log.Info("The client with the given ID has been updated.");
        return await _clientService.Update(id, client);
      } catch (System.Exception e) {
        return BadRequest(new { error = new { message = e.Message } });
      }
    }

    // POST: api/Clients
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    [HttpPost]
    public async Task<ActionResult<Client>> PostClient(Client client) {
      try {
                log.Info("The client has been created.");
        return await _clientService.Create(client);
      } catch (Exception e) {
        return BadRequest(new { error = new { message = e.Message } });
      }
    }

    // DELETE: api/Clients/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Client>> DeleteClient(int id) {
      try {
                log.Info("The client has been deleted.");
                return await _clientService.Remove(id);
      } catch (Exception e) {
        return BadRequest(new { error = new { message = e.Message } });
      }
    }
  }
}
