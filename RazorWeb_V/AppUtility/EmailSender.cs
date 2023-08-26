using Microsoft.AspNetCore.Identity.UI.Services;

namespace AppUtility;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        // return Task is Completed
        return Task.CompletedTask;
    }
}
