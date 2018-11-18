using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CardGameAPI.Models
{
  public class Player
  {
    [Key]
    public int Id { get; set; }
    public string UserName { get; set; }
    public DateTime LastActivity { get; set; }
  }
}
