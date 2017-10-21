using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using SendGridMail;
using SendGridMail.Transport;

namespace VTSWebService.Emailing
{
    public static class Emailer
    {
        private static readonly  List<string> AdministratorPrivateEmails = 
            new List<string>()
                {
                    "shukanov.artyom@gmail.com"
                };

        private const string SystemFromEmail = "vtsautomonitoring@gmail.com";
        private static readonly NetworkCredential SendGridCredentials = 
            new NetworkCredential(
                @"azure_aafb6b3d18053b3edeab648c4a9e89a7@azure.com",
                "vmgy03ui");

        public static void NotifyAdminAboutUserRegistration(string username, string userEmail)
        {
            SendGrid message = SendGrid.GetInstance();
            foreach (string email in AdministratorPrivateEmails)
            {
                message.AddTo(email);
            }
            message.From = new MailAddress(SystemFromEmail);
            message.Subject = "VTS Automonitoring - new User has registered";
            message.Html = String.Format("<p>Username: {0}</p> \r\n User email: {1}", username, userEmail);
            SMTP transport = SMTP.GetInstance(SendGridCredentials);
            transport.Deliver(message);
        }

        public static void NotifyAdminAboutVehicleRegistration(string vin, string modelName)
        {
            SendGrid message = SendGrid.GetInstance();
            foreach (string email in AdministratorPrivateEmails)
            {
                message.AddTo(email);
            }
            message.From = new MailAddress(SystemFromEmail);
            message.Subject = "VTS Automonitoring - new Vehicle has registered";
            message.Html = String.Format("<p>Model: {1}</p> \r\n Vin: {0}", vin, modelName);
            SMTP transport = SMTP.GetInstance(SendGridCredentials);
            transport.Deliver(message);
        }

        public static void NotifyAdminAboutVehicleAssociationWithUser(
            string modelName, string vin, string userLogin, long userId)
        {
            SendGrid message = SendGrid.GetInstance();
            foreach (string email in AdministratorPrivateEmails)
            {
                message.AddTo(email);
            }
            message.From = new MailAddress(SystemFromEmail);
            message.Subject = "VTS Automonitoring - Vehicle has been associated to User";
            message.Html = String.Format("<p>Model: {1}</p> \r\n Vin: {0} \r\n User Login: {2} \r\n User ID: {3}",
                vin, modelName, userLogin, userId);
            SMTP transport = SMTP.GetInstance(SendGridCredentials);
            transport.Deliver(message);
        }

        public static void NotifySystemAboutUnrecognizedData(Stream dataStream)
        {
            SendGrid message = SendGrid.GetInstance();
            foreach (string email in AdministratorPrivateEmails)
            {
                message.AddTo(email);
            }
            message.From = new MailAddress(SystemFromEmail);
            message.Subject = "VTS Automonitoring - Unrecognized data uploaded";
            message.Html = String.Format("<p>Unrecognized data has been uploaded by someone.</p>");
            message.AddAttachment(dataStream, "UnrecognizedData.uvts");
            SMTP transport = SMTP.GetInstance(SendGridCredentials);
            transport.Deliver(message);
        }

        public static void NotifyAdminAboutNewUnrecognizedVin(string vin)
        {
            SendGrid message = SendGrid.GetInstance();
            foreach (string email in AdministratorPrivateEmails)
            {
                message.AddTo(email);
            }
            message.From = new MailAddress(SystemFromEmail);
            message.Subject = "VTS Automonitoring - new Unrecognized Vin";
            message.Html = String.Format("<p>Unrecognized VIN has been supplied by someone: {0}</p>", vin);
            SMTP transport = SMTP.GetInstance(SendGridCredentials);
            transport.Deliver(message);
        }

        public static void NotifyClientAboutRegistration(string email, 
            string newClientLogin, string newClientPassword)
        {
            string registrationMessage = String.Format(
                EmailingStringsProvider.GetString("ClientRegistrationNotification"),
                newClientLogin, newClientPassword);
            SendGrid message = SendGrid.GetInstance();
            message.AddTo(email);
            foreach (string admEmail in AdministratorPrivateEmails)
            {
                message.AddBcc(admEmail);
            }
            message.From = new MailAddress(SystemFromEmail);
            message.Subject = "VTS Automonitoring - Доступ к системе";
            message.Html = registrationMessage;
            SMTP transport = SMTP.GetInstance(SendGridCredentials);
            transport.Deliver(message);
        }

        public static void NotifyClientAboutVehicleAssociation(
            string clientEmail, 
            string vehicleManufacturer,
            string vehicleModel,
            string vehicleVin)
        {
            string associationMessage = String.Format(
                EmailingStringsProvider.GetString("ClientVehicleAssociationNotification"),
                vehicleManufacturer, vehicleModel, vehicleVin);
            SendGrid message = SendGrid.GetInstance();
            message.AddTo(clientEmail);
            foreach (string admEmail in AdministratorPrivateEmails)
            {
                message.AddBcc(admEmail);
            }
            message.From = new MailAddress(SystemFromEmail);
            message.Subject = "VTS Automonitoring - данные по машине";
            message.Html = associationMessage;
            SMTP transport = SMTP.GetInstance(SendGridCredentials);
            transport.Deliver(message);
        }
    }
}
