﻿using Microsoft.AspNetCore.Identity.UI.Services;


namespace MVC.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //logic to send email
            //test git branch//
            return Task.CompletedTask;
        }
    }
}