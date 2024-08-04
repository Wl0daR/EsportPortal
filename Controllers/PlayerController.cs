using Microsoft.AspNetCore.Mvc;
using EsportPortal.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;

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
            var response = await client.GetAsync("Player");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var players = JsonConvert.DeserializeObject<IEnumerable<PlayerDto>>(jsonString);
                var sortedPlayers = players.OrderBy(p => p.Nickname).ToList();
                return View(sortedPlayers);
            }
            else
            {
                return View(new List<PlayerDto>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            var client = _clientFactory.CreateClient("EsportAPI");
            var response = await client.GetAsync($"Player/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var player = JsonConvert.DeserializeObject<PlayerDto>(jsonString);
                return View(player);
            }

            return NotFound();
        }

        public IActionResult Create()
        {
            return View(new PlayerDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create(PlayerDto player, IFormFile photo)
        {
            if (photo != null && photo.Length > 0)
            {
                var filePath = Path.Combine("wwwroot/images", photo.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }

                player.PhotoUrl = $"/images/{photo.FileName}";
            }

            var client = _clientFactory.CreateClient("EsportAPI");
            var response = await client.PostAsJsonAsync("Player", player);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(player);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var client = _clientFactory.CreateClient("EsportAPI");
            var response = await client.GetAsync($"Player/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var player = JsonConvert.DeserializeObject<PlayerDto>(jsonString);
                return View(player);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PlayerDto player, IFormFile photo)
        {
            if (photo != null && photo.Length > 0)
            {
                var filePath = Path.Combine("wwwroot/images", photo.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }

                player.PhotoUrl = $"/images/{photo.FileName}";
            }

            var client = _clientFactory.CreateClient("EsportAPI");
            var response = await client.PutAsJsonAsync($"Player/{player.Id}", player);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(player);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var client = _clientFactory.CreateClient("EsportAPI");
            var response = await client.GetAsync($"Player/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var player = JsonConvert.DeserializeObject<PlayerDto>(jsonString);
                return View(player);
            }

            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = _clientFactory.CreateClient("EsportAPI");
            var response = await client.DeleteAsync($"Player/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }
    }
}
