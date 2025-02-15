﻿using AuthenticationService.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService
{
    public class JwtMiddleWare
    {
        private readonly RequestDelegate _next;

        public JwtMiddleWare (RequestDelegate next) { _next = next; }

        public async Task Invoke(HttpContext context, [FromServices] TokenManager _tokenManager)
        {

            var headers = context.Request.Headers["Authorization"].FirstOrDefault();
            var token = headers?.Split(" ").Last();
            if (token is not null)
            {
                var user = _tokenManager.ValidateToken(token);
                if (user is not null)
                {
                    context.Items["User"] = user;
                }
            }

            await _next(context);
        }
    }
}

