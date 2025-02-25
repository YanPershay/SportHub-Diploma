﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportHub.API.JwtMiddlewareTest;
using SportHub.Application.Commands;
using SportHub.Application.Queries;
using SportHub.Application.Responses;
using SportHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportHub.API.Controllers
{
    public class UserController : ApiController
    {
        public readonly IMediator _mediator;
        private IUserService _userService;

        public UserController(IMediator mediator, IUserService userService)
        {
            _mediator = mediator;
            _userService = userService;
        }

        //[HttpPost("twofactauthEmail")]
        //public IActionResult TwoFactorAuthEmail(AuthenticateRequest model)
        //{
        //    var response = _twoFactorService.SendEmail(model);

        //    if (response == null)
        //        return BadRequest(new { message = "Username or password is incorrect" });

        //    return Ok(response);
        //}

        //[HttpPost("twofactauth")]
        //public async Task<ActionResult<bool>> TwoFactorAuth(TwoFactor model)
        //{
        //    var response = await _twoFactorService.LoginTwoStep(model);

        //    if (!response)
        //        return BadRequest(new { message = "Username or password is incorrect" });

        //    return Ok(response);
        //}

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        //[JwtAuthorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserResponse>> GetUserByGuid(Guid guid)
        {
            var query = new GetUserByGuidQuery(guid);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("usernameCheck")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> IsUsernameBusy(string username)
        {
            var query = new IsUsernameBusyQuery(username);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("searchUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserResponse>>> SearchUser(string searchString)
        {
            var query = new SearchUserQuery(searchString);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserResponse>> CreateUser([FromBody] CreateUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [JwtAuthorize]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserResponse>> UpdateUser([FromBody] UpdateUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
