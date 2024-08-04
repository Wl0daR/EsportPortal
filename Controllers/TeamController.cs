using Microsoft.AspNetCore.Mvc;
using EsportPortal.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

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
                var teams = await response.Content.ReadFromJsonAsync<IEnumerable<Team>>();
                return View(teams);
            }
            else
            {
                return View(new List<Team>());
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Team team)
        {
            var client = _clientFactory.CreateClient("EsportAPI");
            var response = await client.PostAsJsonAsync("Teams", team);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(team);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var client = _clientFactory.CreateClient("EsportAPI");
            var response = await client.GetAsync($"Teams/{id}");

            if (response.IsSuccessStatusCode)
            {
                var team = await response.Content.ReadFromJsonAsync<Team>();
                return View(team);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Team team)
        {
            var client = _clientFactory.CreateClient("EsportAPI");
            var response = await client.PutAsJsonAsync($"Teams/{team.Id}", team);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(team);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var client = _clientFactory.CreateClient("EsportAPI");
            var response = await client.GetAsync($"Teams/{id}");

            if (response.IsSuccessStatusCode)
            {
                var team = await response.Content.ReadFromJsonAsync<Team>();
                return View(team);
            }

            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = _clientFactory.CreateClient("EsportAPI");
            var response = await client.DeleteAsync($"Teams/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }
    }
}
