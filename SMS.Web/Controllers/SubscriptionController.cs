using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Project1.CustomExceptions;
using SMS.Web.Models;

namespace SMS.Web.Controllers
{
    [ApiController]
    [Route ( "api/[controller]" )]
    public class SubscriptionController : Controller
    {

        private readonly ILogger<SubscriptionController> _logger;
        private readonly IMapper _mapper;
        private readonly ISubscriberService _subscriberService;

        public SubscriptionController (IMapper mapper, ILogger<SubscriptionController> logger  , ISubscriberService subscriberService)
        {
            _logger = logger;
            _subscriberService = subscriberService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> ProccessMessage ( [FromBody] Message message )
        {
            try
            {
                 _subscriberService.ProccessMessage ( _mapper.Map<MessageModelRequest> ( message ) );
                return Ok ();
            }
            catch(DatabaseException dbException)
            {
                return BadRequest (dbException.Message);
            }
        }
    }
}
