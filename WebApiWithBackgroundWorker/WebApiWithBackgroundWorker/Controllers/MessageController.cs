using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiWithBackgroundWorker.Domain.Interface;

namespace WebApiWithBackgroundWorker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {

        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
        }

        // GET api/Message/GetMessage
        [HttpGet]
        [Route("GetMessage")]
        public IActionResult Get()
        {
            var messages = _messageService.GetMessages();
            return Ok(messages);
        }


    }
}
