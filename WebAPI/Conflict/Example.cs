using System;

namespace WebAPI.Conflict {
  public class Example {
    public int Id { get; set; }
    public string Information { get; set; }
    public DateTime CreatedAt { get; private set; }

    public Example() {
      this.CreatedAt = DateTime.Now;
    }
  }
}