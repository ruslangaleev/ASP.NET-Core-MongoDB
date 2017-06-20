using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service;
using CustomAttribute;
using Model.Models;
using System.IO;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ApiController : Controller
    {
        private readonly IFileService _fileService;

        // Controller
        public ApiController(IFileService fileService)
        {
            _fileService = fileService;
        }

        // GET api/upload/5
        [HttpGet("{id}")]
        public async Task<FileModel> Get(string id)
        {
            return await _fileService.GetFile(id);
        }

        // DELETE api/upload/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _fileService.RemoveFile(id);
        }

        // POST api/upload
        [HttpPost]
        [RequestFormSizeLimit(100000, Order = 1)]
        public async void Post()
        {
            foreach (var file in Request.Form.Files)
            {
                var fileName = file.FileName;

                using (var fileStream = file.OpenReadStream())
                {
                    using (var memory = new MemoryStream())
                    {
                        await fileStream.CopyToAsync(memory);

                        await _fileService.AddFile(new FileModel()
                        {
                            Source = memory.ToArray(),
                            FileName = file.FileName
                        });
                    }
                }
            }
        }
    }
}
