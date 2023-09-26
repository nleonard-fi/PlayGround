using Microsoft.AspNetCore.Mvc;
using DocumentAnonymization.Models;
using DocumentAnonymization.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DocumentAnonymization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnonymizationRequestController : ControllerBase
    {
        private readonly IAnonymizationRepository _anonymizationRepository;

        public AnonymizationRequestController(IAnonymizationRepository anonymizationRepository)
        {
            _anonymizationRepository = anonymizationRepository;
        }

        // POST api/<AnonymizationRequestController>
        [HttpPost]
        [Consumes("application/json")]
        public async Task<ActionResult<AnonymizationRequest>> Post(AnonymizationRequest anonymizationRequest)
        {
            /*We can update the database here and anonymize stuff.
             Then we can call sharepoint and delete all the documents.

            Questions Here: Do we want this one request to anonymize the notes, meta data, and delete the documents, or do we need a call for each of those tasks?
            
             OR
            
             We can insert an anonymization request record into the database, which will then be processed by a separate service. If we do this, I would make this
            a minimal api without a controller. This would literally be the smallest application for entering into the database, one action method.*/
            return await Task.FromResult(anonymizationRequest);
        }
    }
}
