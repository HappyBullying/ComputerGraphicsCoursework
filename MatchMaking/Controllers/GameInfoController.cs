using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MatchMaking.Data;
using MatchMaking.Models;
using Microsoft.AspNetCore.Mvc;

namespace MatchMaking.Controllers
{
    [Route("ginf/[controller]")]
    [ApiController]
    public class GameInfoController : ControllerBase
    {
        private readonly AppDbContext db;

        public GameInfoController(AppDbContext _db)
        {
            this.db = _db;
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> RegisterServer([FromBody] Server serverInfo)
        {
            if (serverInfo == null)
                return BadRequest("Invalid server details supplied");


            serverInfo.ServerId = Guid.NewGuid().ToString("N");
            List<string> playersIds = new List<string>();
            foreach(Player player in serverInfo.OnlinePlayers)
            {
                player.PlayerId = Guid.NewGuid().ToString();
                playersIds.Add(player.PlayerId);
            }

            if (null == await db.FindAsync<Server>(serverInfo.ServerId))
            {
                await db.AddAsync<Server>(serverInfo);
                await db.AddRangeAsync(serverInfo.OnlinePlayers);
            }
            else
            {
                return BadRequest("Invalid details supplied");
            }

            object result = new
            {
                ServerId = serverInfo.ServerId,
                PlayersId = playersIds
            };

            await db.SaveChangesAsync();

            return new JsonResult(result);
        }



        [HttpPost("[action]")]
        public async Task<IActionResult> AddPlayerOnServer([FromBody] Player player)
        {
            if (player == null)
            {
                return BadRequest("Invalid details supplied");
            }

            Server srv = await db.FindAsync<Server>(player.ServerId);
            if (srv == null)
            {
                return BadRequest("Invalid server id supplied");
            }

            player.PlayerId = Guid.NewGuid().ToString();
            srv.FreeSlots -= 1;
            db.ActiveServers.Update(srv);
            await db.AddAsync(new Player 
            {
                PlayerId = player.PlayerId,
                ServerId = srv.ServerId,
                Username = player.Username,
                RemoteEndpoint = player.RemoteEndpoint
            });

            await db.SaveChangesAsync();

            return new JsonResult(player);
        }


        [HttpPost("[action]")]
        public async Task <IActionResult> DeletePlayerFromServer([FromBody] Player player)
        {
            if (player == null)
                return BadRequest("Invalid details supplied");

            Server srv = await db.ActiveServers.FindAsync(player.ServerId);
            if (srv == null)
            {
                return BadRequest("Invalid details suppleied");
            }
            srv.FreeSlots += 1;
            try
            {
                db.OnlinePlayers.Remove(player);
            }
            catch
            {
                return StatusCode(500);
            }

            return Ok("Information updated");
        }
    }
}