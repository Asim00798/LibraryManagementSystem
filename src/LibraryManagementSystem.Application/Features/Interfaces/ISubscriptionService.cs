using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.DTOs.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.Features.Interfaces
{
    /// <summary>
    /// Service for managing subscriptions.
    /// </summary>
    public interface ISubscriptionService
    {
        /// <summary>
        /// Creates a new subscription.
        /// </summary>
        /// <param name="request">Subscription request data.</param>
        /// <returns>Result message.</returns>
        Task<string> SubscribeAsync(SubscriptionRequestDto request);
    }
}
