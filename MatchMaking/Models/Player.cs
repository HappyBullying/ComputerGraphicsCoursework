using System.ComponentModel.DataAnnotations;

namespace MatchMaking.Models
{
    public class Player
    {
        [Key]
        public string PlayerId { get; set; }
        public string Username { get; set; }
        public string RemoteEndpoint { get; set; }
        public string ServerId { get; set; }
        public Server PerformingServer { get; set; }
    }
}
