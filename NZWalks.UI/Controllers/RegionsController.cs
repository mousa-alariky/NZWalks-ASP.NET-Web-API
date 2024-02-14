using Microsoft.AspNetCore.Mvc;
using NZWalks.UI.Models;
using NZWalks.UI.Models.DTO;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace NZWalks.UI.Controllers
{
    public class RegionsController : Controller
    {
        // Obtain the access token
        string accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJuZXdAZ21haWwuY29tIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiV3JpdGVyIiwiZXhwIjoxNzA5MTUzNzY5LCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MDAwLyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjcwMDAvIn0.Kca1kJo3NoCh_EiGUIalb6191nOu8FGsWq7XVYVNZR4";
        private readonly IHttpClientFactory httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RegionDto> response = new List<RegionDto>();
            try
            {
                // create http client 
                var client = httpClientFactory.CreateClient();

                // Add Authorization header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                // Send GET request to the API endpoint
                var httpResponseMessage = await client.GetAsync("https://localhost:7000/api/regions");

                // Check if the request was successful
                httpResponseMessage.EnsureSuccessStatusCode();

                // Read the response content and deserialize it to a list of RegionDto
                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());

            }
            catch (Exception)
            {

                // Log exception or handle it appropriately
            }

            return View(response);
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel model)
        {
            try
            {
                // Create HTTP client
                var client = httpClientFactory.CreateClient();

                // Add Authorization header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                // Create a POST request to add the new region
                var httpRequestMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://localhost:7000/api/regions"),
                    Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json"),
                };

                // Send the request
                var httpResponseMessage = await client.SendAsync(httpRequestMessage);
                httpResponseMessage.EnsureSuccessStatusCode();

                // Read the response and deserialize it
                var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

                if (response is not null)
                {
                    // Redirect to the index action if the addition was successful
                    return RedirectToAction("Index", "Regions");
                }
            }
            catch (Exception ex)
            {
                // Log exception or handle it appropriately

            }

            // Return a view indicating failure to add the new region
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            // Create HTTP client
            var client = httpClientFactory.CreateClient();

            // Add Authorization header
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            // Send GET request to retrieve the region for editing
            var response = await client.GetFromJsonAsync<RegionDto>($"https://localhost:7000/api/regions/{id}");
            if (response is not null)
            {
                return View(response);
            }

            return View(null);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(RegionDto request)
        {
            // Create HTTP client
            var client = httpClientFactory.CreateClient();

            // Add Authorization header
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            // Create a PUT request to update the region
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7000/api/regions/{request.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json"),
            };

            // Send the request
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            // Read the response and deserialize it
            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();


            if (response is not null)
            {
                // Redirect to the index action if the update was successful
                return RedirectToAction("Index", "Regions");
            }

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Delete(RegionDto request)
        {
            try
            {
                // Create HTTP client
                var client = httpClientFactory.CreateClient();

                // Add Authorization header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                // Send DELETE request to remove the region
                var httpResponseMessage = await client.DeleteAsync($"https://localhost:7000/api/regions/{request.Id}");

                // Ensure the request was successful
                httpResponseMessage.EnsureSuccessStatusCode();

                // Redirect to the index action if the deletion was successful
                return RedirectToAction("Index", "Regions");
            }
            catch (Exception ex)
            {
                // Log exception or handle it appropriately
            }

            // Return a view indicating failure to delete the region
            return View("Edit");
        }
    }

}
