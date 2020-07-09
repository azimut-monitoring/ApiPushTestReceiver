using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;

namespace ApiPushTestReceiver.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PushController : ControllerBase
    {
        public static readonly ConcurrentQueue<string> Items = new ConcurrentQueue<string>();
        private readonly int _bufferSize;

        public PushController(IOptions<AppSettings> settings)
        {
            _bufferSize = settings.Value.BufferSize;
        }

        // POST apipush  
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var body = await (new StreamReader(HttpContext.Request.Body)).ReadToEndAsync();

            Items.Enqueue(body);
            if (Items.Count > _bufferSize)
            {
                Items.TryDequeue(out var _);
            }

            return Ok();
        }
    }
}
