using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SecureWebhook;

namespace OlmedDataBus.Webhooks.TestHost.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WebhookController : ControllerBase
    {
        private readonly SecureWebhookHelper _helper;

        public WebhookController(IConfiguration configuration)
        {
            var encryptionKey = configuration["OlmedDataBus:WebhookKeys:EncryptionKey"] ?? string.Empty;
            var hmacKey = configuration["OlmedDataBus:WebhookKeys:HmacKey"] ?? string.Empty;
            _helper = new SecureWebhookHelper(encryptionKey, hmacKey);
        }

        [HttpPost]
        public IActionResult Receive([FromBody] WebhookPayload payload)
        {
            if (_helper.TryDecryptAndVerify(payload.Data, payload.IV, payload.Signature, out var json))
            {
                // Mo¿esz zdeserializowaæ json do obiektu, np. dynamic lub konkretnej klasy
                return Ok(new { Success = true, Decrypted = json });
            }
            return BadRequest("Invalid signature or decryption failed.");
        }
    }

    public class WebhookPayload
    {
        public string Data { get; set; } = string.Empty;
        public string IV { get; set; } = string.Empty;
        public string Signature { get; set; } = string.Empty;
    }
}
