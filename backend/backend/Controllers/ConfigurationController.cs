using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        public IConfiguration Configuration { get; }
        public ConfigurationController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // GET: api/<ConfigurationController>
        [HttpGet]
        public ConfigurationSettings Get()

        {
            return new ConfigurationSettings()
            {
                AcceptedTypes = Configuration["Configuration:AcceptedTypes"],
                MaxSize = int.Parse(Configuration["Configuration:MaxSize"])
            };
        }
    }
}
