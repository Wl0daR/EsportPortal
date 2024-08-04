using Microsoft.AspNetCore.Mvc;
using EsportPortal.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EsportPortal.Controllers
{
    public class TournamentController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public TournamentController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient("EsportAPI");
            var response = await client.GetAsync("Tournaments");

            if (response.IsSuccessStatusCode)
            {
                var tournaments = await response.Content.ReadFromJsonAsync<IEnumerable<Tournament>>();
                return View(tournaments);
            }
            else
            {
                return View(new List<Tournament>());
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Tournament tournament)
        {
            var client = _clientFactory.CreateClient("EsportAPI");
            var response = await client.PostAsJsonAsync("Tournaments", tournament);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(tournament);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var client = _clientFactory.CreateClient("EsportAPI");
            var response = await client.GetAsync($"Tournaments/{id}");

            if (response.IsSuccessStatusCode)
            {
                var tournament = await response.Content.ReadFromJsonAsync<Tournament>();
                return View(tournament);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Tournament tournament)
        {
            var client = _clientFactory.CreateClient("EsportAPI");
            var response = await client.PutAsJsonAsync($"Tournaments/{tournament.Id}", tournament);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(tournament);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var client = _clientFactory.CreateClient("EsportAPI");
            var response = await client.GetAsync($"Tournaments/{id}");

            if (response.IsSuccessStatusCode)
            {
                var tournament = await response.Content.ReadFromJsonAsync<Tournament>();
                return View(tournament);
            }

            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = _clientFactory.CreateClient("EsportAPI");
            var response = await client.DeleteAsync($"Tournaments/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }
    }
}