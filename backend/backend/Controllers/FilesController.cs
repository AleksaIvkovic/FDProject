using backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private IConfiguration Configuration { get; set; }
        public FilesController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpGet]
        public IEnumerable<FileGroup> Get()
        {
            string pathToReadFrom = Path.Combine(Directory.GetCurrentDirectory(), "Documents");

            string[] files = Directory.GetFiles(pathToReadFrom);
            List<FileInfo> fileInfos = new List<FileInfo>();

            foreach (var file in files)
            {   
                fileInfos.Add(new System.IO.FileInfo(file));
            }

            var filesGroupedByType = fileInfos.GroupBy(fi => Path.GetExtension(fi.Name.ToLower()));
            List<FileGroup> result = new List<FileGroup>();

            foreach(var fileGroup in filesGroupedByType)
            {
                var tempList = new List<FileData>();
                foreach(var file in fileGroup)
                {
                    tempList.Add(new FileData()
                    {
                        CreationDate = file.CreationTimeUtc,
                        Name = file.Name,
                        Size = file.Length,
                    });
                }
                result.Add(new FileGroup()
                {
                    Type = fileGroup.Key,
                    Files = tempList
                });
            }

            return result;
        }

        // POST api/<FilesController>
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            try
            {
                var files = Request.Form.Files;
                var folderName = "Documents";
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (files.Any(f => f.Length == 0))
                {
                    return StatusCode(400, "No files were sent");
                }
                foreach(var file in files)
                {
                    if (!(Configuration["Configuration:AcceptedTypes"].Split(',').Select(item => item.Trim())).Contains(Path.GetExtension(file.FileName).ToLower()))
                    {
                        return StatusCode(400, "Not all files are the right type");
                    }
                }
                foreach (var file in files)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName); //you can add this path to a list and then return all dbPaths to the client if require
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                return Ok("All the files are successfully uploaded.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
