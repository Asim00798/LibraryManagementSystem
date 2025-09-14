#region Usings
using LibraryManagement.Domain.Interfaces;
using LibraryManagementSystem.Application.DTOs.Request;
using LibraryManagementSystem.Application.Features.Interfaces;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Entities.Security;
using LibraryManagementSystem.Domain.Enums;
using LibraryManagementSystem.Domain.Interfaces;
using System;
using System.Threading.Tasks;
#endregion
namespace LibraryManagementSystem.Application.Features.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubscriptionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> SubscribeAsync(SubscriptionRequestDto request)
        {
            // Ensure user exists
            var user = await GetUserAsync(request.User.UserName);

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                // Build subscription with optional branch and payment log
                var subscription = await BuildSubscriptionAsync(user, request);

                // Save subscription
                await _unitOfWork.Subscriptions.AddAsync(subscription);

                // Commit transaction
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();

                return "Subscription created successfully.";
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        #region Helper methods
        private async Task<User> GetUserAsync(string userName)
        {
            var user = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            if (user == null)
                throw new InvalidOperationException("User does not exist.");
            return user;
        }

        private async Task<Subscription> BuildSubscriptionAsync(User user, SubscriptionRequestDto request)
        {
            var membership = await GetMembershipAsync(request.MembershipType);
            var branch = await GetBranchAsync(request.BranchName);

            var subscription = new Subscription
            {
                UserId = user.Id,
                MembershipId = membership.Id,
                BranchId = branch?.Id,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Status = SubscriptionStatus.Active
            };

            var paymentLog = BuildPaymentLog(request.PaymentlogRequest);
            subscription.PaymentLogs.Add(paymentLog);

            return subscription;
        }

        private async Task<Membership> GetMembershipAsync(MembershipType type)
        {
            var membership = await _unitOfWork.Memberships
                .FirstOrDefaultAsync(m => m.MembershipType == type);

            if (membership == null)
                throw new InvalidOperationException("Membership type not found.");

            return membership;
        }

        private async Task<Branch?> GetBranchAsync(string branchName)
        {
            if (string.IsNullOrEmpty(branchName)) return null;

            return await _unitOfWork.Branches
                .FirstOrDefaultAsync(b => b.Name == branchName);
        }

        private PaymentLog BuildPaymentLog(PaymentlogRequestDto request)
        {
            return new PaymentLog
            {
                Amount = request.Amount,
                PaymentDate = request.PaymentDate,
                PaymentType = request.PaymentType,
                PaymentMethod = request.PaymentMethod,
                PaymentStatus = request.PaymentStatus,
                Currency = request.Currency,
                TransactionReference = request.TransactionReference,
                ReceiptNumber = request.ReceiptNumber,
                InvoiceNumber = request.InvoiceNumber,
                Notes = request.Notes,
                FineReason = request.FineReason
            };
        }

        #endregion

    }
}
