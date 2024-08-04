using Microsoft.AspNetCore.Mvc;
using EsportPortal.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Linq; // Dodaj to, aby używać LINQ

namespace EsportPortal.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public PlayerController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient("EsportAPI");
            var response = await client.GetAsync("Players");

            if (response.IsSuccessStatusCode)
            {
                var players = await response.Content.ReadFromJsonAsync<IEnumerable<Player>>();
                var sortedPlayers = players.OrderBy(p => p.Nickname).ToList(); // Sortowanie po Nickname
                return View(sortedPlayers);
            }
            else
            {
                return View(new List<Player>());
            }
        }

        public IActionResult Create()
        {
            return View(new Player());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Player player)
        {
            var client = _clientFactory.CreateClient("EsportAPI");
            var response = await client.PostAsJsonAsync("Players", player);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(player);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var client = _clientFactory.CreateClient("EsportAPI");
            var response = await client.GetAsync($"Players/{id}");

            if (response.IsSuccessStatusCode)
            {
                var player = await response.Content.ReadFromJsonAsync<Player>();
                return View(player);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Player player)
        {
            var client = _clientFactory.CreateClient("EsportAPI");
            var response = await client.PutAsJsonAsync($"Players/{player.Id}", player);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(player);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var client = _clientFactory.CreateClient("EsportAPI");
            var response = await client.GetAsync($"Players/{id}");

            if (response.IsSuccessStatusCode)
            {
                var player = await response.Content.ReadFromJsonAsync<Player>();
                return View(player);
            }

            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = _clientFactory.CreateClient("EsportAPI");
            var response = await client.DeleteAsync($"Players/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }
    }
}
