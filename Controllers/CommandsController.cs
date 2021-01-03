using System.Text;
using System.Net.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Client.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Client.Controllers
{
    public class CommandsController : Controller
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();
        Command _oCommand = new Command();
        List<Command> _oCommands = new List<Command>();

        public CommandsController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender,cert,chain, SslPolicyErrors) => { return true;};
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<List<Command>> GetAllCommands()
        {
            _oCommands = new List<Command>();

            using (var httpClient = new HttpClient(_clientHandler))
            {
                using(var response=await httpClient.GetAsync("https://localhost:5001/api/commands/"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oCommands = JsonConvert.DeserializeObject<List<Command>>(apiResponse);
                    
                }
            }

            return _oCommands;
        }

        [HttpGet]
        public async Task<Command> GetById(int commandId)
        {
            _oCommand = new Command();

            using (var httpClient = new HttpClient(_clientHandler))
            {
                using(var response=await httpClient.GetAsync("https://localhost:5001/api/commands/" + commandId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oCommand = JsonConvert.DeserializeObject<Command>(apiResponse);
                }
            }

            return _oCommand;
        }

        [HttpPost]
        public async Task<Command> AddUpdateCommand(Command command)
        {
            _oCommand = new Command();

            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");
                using(var response=await httpClient.PostAsync("https://localhost:5001/api/commands", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oCommand = JsonConvert.DeserializeObject<Command>(apiResponse);
                }
            }

            return _oCommand;
        }

        [HttpDelete]
        public async Task<string> Delete(int commandId)
        {
            string message = "";
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using(var response=await httpClient.DeleteAsync("https://localhost:5001/api/commands/" + commandId))
                {
                    message = await response.Content.ReadAsStringAsync();
                    
                }
            }

            return message;
        }

        [HttpPut]
        public async Task<string> Put(int commandId, Command command)
        {
            string message = "";
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");
                using(var response=await httpClient.PutAsync("https://localhost:5001/api/commands/" + commandId, content))
                {
                    message = await response.Content.ReadAsStringAsync();
                    
                }
            }
            return message;
        }

        [HttpPatch]
        public async Task<string> Patch(int commandId, Patch command)
        {
            string message = "";
            string a = JsonConvert.SerializeObject(command);
            string b = "[" + a + "]";
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(b, Encoding.UTF8, "application/json");
                using(var response=await httpClient.PatchAsync("https://localhost:5001/api/commands/" + commandId, content))
                {
                    message = await response.Content.ReadAsStringAsync();
                    
                }
            }
            return message;
        }

       
    }
}