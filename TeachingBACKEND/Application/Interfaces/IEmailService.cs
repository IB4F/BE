namespace TeachingBACKEND.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(string to, string subject, string htmlBody);
        Task SendManualPaymentInstructionsAsync(string to, string reference, long amountCents, string currency, string bankName, string iban, string accountHolder);
        Task SendManualPaymentConfirmedAsync(string to, string packageName);
        Task SendManualPaymentReminderAsync(string to, string reference, long amountCents, string currency);
    }
}
