using System;
using System.Security.Claims;
using Shipping.Core.Services.contract;

namespace Shipping.Services.Handler
{
    /// <summary>
    /// Represents the user identifier provider.
    /// </summary>
    public class UserIdentifierProvider : IUserIdentifierProvider
    {
        public UserIdentifierProvider(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)
                                 ?? throw new ArgumentException("The user identifier claim is required.", nameof(httpContextAccessor));

        }

        public string UserId { get; }
    }
}