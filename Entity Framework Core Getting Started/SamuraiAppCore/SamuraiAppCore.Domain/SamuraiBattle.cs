using System.ComponentModel.DataAnnotations;

namespace SamuraiAppCore.Domain
{
  public class SamuraiBattle:ClientChangeTracker
  {
        [Key]
    public int SamuraiId { get; set; }
    public Samurai Samurai { get; set; }
    public int BattleId { get; set; }
    public Battle Battle { get; set; }
   
  }
}