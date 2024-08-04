using Microsoft.AspNetCore.Mvc;
using EsportPortal.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EsportPortal.Controllers
{
    public class TeamController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public TeamController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient("EsportAPI");
            var response = await client.GetAsync("Teams");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var teams = JsonConvert.DeserializeObject<List<Team>>(json);
                return View(teams);
            }
            else
            {
                return View(new List<Team>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            var client = _clientFactory.CreateClient("EsportAPI");

            // Pobieranie drużyny
            var teamResponse = await client.GetAsync($"Teams/{id}");
            if (!teamResponse.IsSuccessStatusCode)
            {
                return NotFound();
            }
            var teamJson = await teamResponse.Content.ReadAsStringAsync();
            var team = JsonConvert.DeserializeObject<Team>(teamJson);

            // Pobieranie graczy dla drużyny
            var playersResponse = await client.GetAsync($"Teams/{id}/Players");
            if (playersResponse.IsSuccessStatusCode)
            {
                var playersJson = await playersResponse.Content.ReadAsStringAsync();
                var players = JsonConvert.DeserializeObject<List<Player>>(playersJson);
                team.Players = players;
            }

            return View(team);
        }
    }
}
