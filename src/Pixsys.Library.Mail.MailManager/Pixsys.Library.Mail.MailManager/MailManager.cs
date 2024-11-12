// -----------------------------------------------------------------------
// <copyright file="MailManager.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Pixsys.Library.Mail.MailManager.Constants;
using Pixsys.Library.Mail.MailManager.Helpers;
using Pixsys.Library.Mail.MailManager.Interfaces;
using Pixsys.Library.Mail.MailManager.Models;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Pixsys.Library.Mail.MailManager
{
    /// <summary>
    /// The Mail Manager.
    /// </summary>
    /// <seealso cref="IMailManager" />
    /// <remarks>
    /// Initializes a new instance of the <see cref="MailManager"/> class.
    /// </remarks>
    /// <param name="settings">The mail manager settings.</param>
    public class MailManager(MailManagerSettings settings) : IMailManager
    {
        private readonly MailManagerSettings settings = settings ?? throw new ArgumentNullException(nameof(settings));

        /// <inheritdoc />
        public string GetBody(MailBodyParameters p)
        {
            string output = string.Empty;
            if (!string.IsNullOrWhiteSpace(p.TemplateFolderName) && !string.IsNullOrWhiteSpace(p.TemplateHtmlPageName))
            {
                output = MailHelper.LoadHtmlTemplate(settings, p.TemplateFolderName, p.TemplateHtmlPageName);

                foreach (string zoneTag in MailHelper.GetTemplateZonesTags(output))
                {
                    // Get content from template or mail
                    string zone = zoneTag.Replace(ZoneTagConstants.Prefix, string.Empty).Replace(ZoneTagConstants.Suffix, string.Empty).Replace("ZONE_", string.Empty).ToLower(System.Globalization.CultureInfo.CurrentCulture);
                    output = output.Replace(zoneTag, MailHelper.BuildContent(settings, p, zone));
                }
            }

            return output;
        }

        /// <inheritdoc />
        public SentMailReport? Send(MailParameters p)
        {
            SentMailReport mail = MailHelper.Prepare(settings, p);

            if (settings?.SmtpClient != null && mail?.MailMessage != null && mail?.Errors != null)
            {
                try
                {
#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
                    ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ServerCertificateValidationCallback);
#pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
                    settings.SmtpClient.Send(mail.MailMessage);
                    mail.IsSuccessful = true;
                }
                catch (Exception ex)
                {
                    mail.Errors.Add(ex.Message);
                }
            }

            return mail;
        }

        /// <inheritdoc />
        public virtual bool ServerCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return sslPolicyErrors == SslPolicyErrors.None
                    || (sender is HttpWebRequest
                    && certificate is X509Certificate2
                    && sslPolicyErrors == SslPolicyErrors.RemoteCertificateChainErrors);
        }
    }
}