﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using CovidSafe.DAL.Services;
using CovidSafe.Entities.Protos;
using CovidSafe.Entities.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CovidSafe.API.Controllers
{
    /// <summary>
    /// Handles <see cref="MatchMessage"/> CRUD operations
    /// </summary>
    [ApiController]
    [ApiVersion("2020-04-14", Deprecated = true)]
    [ApiVersion("2020-04-15")]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        /// <summary>
        /// <see cref="MatchMessage"/> service layer
        /// </summary>
        private IMessageService _messageService;

        /// <summary>
        /// Creates a new <see cref="MessageController"/> instance
        /// </summary>
        /// <param name="messageService"><see cref="MatchMessage"/> service layer</param>
        public MessageController(IMessageService messageService)
        {
            // Assign local values
            this._messageService = messageService;
        }

        /// <summary>
        /// Get <see cref="MatchMessage"/> objects matching the provided identifiers
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/Message&amp;api-version={current_version}
        ///     {
        ///         "RequestedQueries": [{
        ///             "messageId": "baa0ebe1-e6dd-447d-8d82-507644991e07",
        ///             "messageTimestamp": 1586199635012
        ///         }]
        ///     }
        ///     
        /// </remarks>
        /// <param name="request"><see cref="MessageRequest"/> parameters</param>
        /// <param name="cancellationToken">Cancellation token (not required in API call)</param>
        /// <response code="200">Successful request with results</response>
        /// <response code="400">Malformed or invalid request provided</response>
        /// <returns>Collection of <see cref="MatchMessage"/> objects matching request parameters</returns>
        [HttpPost]
        [Consumes("application/x-protobuf", "application/json")]
        [Produces("application/x-protobuf", "application/json")]
        [ProducesResponseType(typeof(IEnumerable<MatchMessage>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RequestValidationResult), StatusCodes.Status400BadRequest)]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public async Task<ActionResult<IEnumerable<MatchMessage>>> PostAsync([FromBody] MessageRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                // Fetch and return results
                return Ok(
                    await this._messageService.GetByIdsAsync(
                        request.RequestedQueries.Select(r => r.MessageId), cancellationToken
                    )
                );
            }
            catch(RequestValidationFailedException ex)
            {
                // Only return validation results
                return BadRequest(ex.ValidationResult);
            }
            catch(ArgumentNullException)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Service status request endpoint, used mostly by Azure services to determine if 
        /// an endpoint is alive
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     HEAD /api/Message
        /// 
        /// </remarks>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <response code="200">Successful request</response>
        [ApiExplorerSettings(IgnoreApi = true)]
        [ApiVersionNeutral]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public Task<OkResult> HeadAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Ok());
        }
    }
}