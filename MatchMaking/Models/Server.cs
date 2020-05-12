using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MatchMaking.Models
{
    public class Server
    {
        [Key]
        public string ServerId { get; set; }
        public string RemoteEndpoing { get; set; }
        public int ServerCapacity { get; set; }
        public int FreeSlots { get; set; }
        public IList<Player> OnlinePlayers { get; set; }
    }
}
