using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using EsportPortal.Models;
using EsportPortal.Data;

namespace EsportPortal.Controllers
{
    public class TournamentsController : Controller
    {
        private readonly EsportContext _context;

        public TournamentsController(EsportContext context)
        {
            _context = context;
        }

        // Metoda Index zwracająca listę wszystkich turniejów
        public async Task<IActionResult> Index()
        {
            var tournaments = await _context.Tournaments
                .Select(t => new TournamentDetailsDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Date = t.Date,
                    Location = t.Location
                })
                .ToListAsync();

            return View(tournaments);
        }

        // Metoda Details zwracająca szczegóły turnieju wraz z drużynami
        public async Task<IActionResult> Details(int id)
        {
            var tournament = await _context.Tournaments
                .Include(t => t.TeamTournamentHistories)
                    .ThenInclude(tth => tth.Team)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tournament == null)
            {
                return NotFound();
            }

            var tournamentDetails = new TournamentDetailsDto
            {
                TournamentName = tournament.Name,
                Teams = tournament.TeamTournamentHistories.Select(tth => new TeamDto
                {
                    Id = tth.Team.Id,
                    Name = tth.Team.Name,
                    LogoUrl = tth.Team.LogoUrl // Zakładam, że masz pole LogoUrl w modelu Team
                }).ToList()
            };

            return View(tournamentDetails);
        }
    }
}
