using FootballPlayerAPI.Data;
using FootballPlayerAPI.DTOs;
using FootballPlayerAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FootballPlayerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FootballPlayerController : ControllerBase
    {
        private readonly DataContext _context;

        public FootballPlayerController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FootballPlayer>>> GetFootballPlayers()
        {
            var footballPlayers = await _context.FootballPlayers.ToListAsync();

            return Ok(footballPlayers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FootballPlayer>> GetFootballPlayerById(int id)
        {
            var footballPlayer = await _context.FootballPlayers.FindAsync(id);

            if (footballPlayer is null)
            {
                return NotFound($"Could not find a footballplayer with id: {id}");
            }

            return Ok(footballPlayer);
        }

        [HttpPost]
        public async Task<IActionResult> AddFootballPlayer(FootballPlayerDTO footballPlayerDTO)
        {
            var footballPlayer = new FootballPlayer
            {
                FirstName = footballPlayerDTO.FirstName,
                LastName = footballPlayerDTO.LastName,
                Number = footballPlayerDTO.Number,
                CurrentTeam = footballPlayerDTO.CurrentTeam,
                CreatedAt = DateTime.UtcNow
            };

            await _context.FootballPlayers.AddAsync(footballPlayer);
            await _context.SaveChangesAsync();

            return Ok($"Successfully added {footballPlayer.FirstName} {footballPlayer.LastName} to database");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFootballPlayer(int id)
        {
            var playerToDelete = await _context.FootballPlayers.FindAsync(id);

            if (playerToDelete is null)
            {
                return NotFound($"Could not find a footballplayer with id: {id} to delete");
            }

            _context.FootballPlayers.Remove(playerToDelete);
            await _context.SaveChangesAsync();

            return Ok($"Removed {playerToDelete.FirstName} {playerToDelete.LastName} from the database");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFootballPlayer(int id, FootballPlayerDTO footballPlayerDTO)
        {
            var playerToUpdate = await _context.FootballPlayers.FindAsync(id);

            if (playerToUpdate is null)
            {
                return NotFound($"Could not find a footballplayer with id: {id} to update");
            }

            if (!string.IsNullOrEmpty(footballPlayerDTO.FirstName))
            {
                playerToUpdate.FirstName = footballPlayerDTO.FirstName;
            }

            if (!string.IsNullOrEmpty(footballPlayerDTO.LastName))
            {
                playerToUpdate.LastName = footballPlayerDTO.LastName;
            }

            if (footballPlayerDTO.Number.HasValue)
            {
                playerToUpdate.Number = footballPlayerDTO.Number;
            }

            if (!string.IsNullOrEmpty(footballPlayerDTO.CurrentTeam))
            {
                playerToUpdate.CurrentTeam = footballPlayerDTO.CurrentTeam;
            }

            playerToUpdate.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return Ok($"Successfully updated your player");
        }
    }
}