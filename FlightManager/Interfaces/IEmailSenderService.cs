﻿namespace FlightManager.Interfaces
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(string toEmail, string subject, string content);
    }
}
