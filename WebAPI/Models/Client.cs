using System;

namespace WebAPI.Models
{
  public class Client
  {
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    // public string FullName { get { return FirstName + " " + LastName;} }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string NationalId { get; set; }
    public DateTime CreatedAt { get; private set; }

    public Client()
    {
      CreatedAt = DateTime.Now;
    }
  }
}