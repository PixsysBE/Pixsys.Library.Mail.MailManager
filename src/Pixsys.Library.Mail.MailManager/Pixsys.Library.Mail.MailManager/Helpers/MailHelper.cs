// -----------------------------------------------------------------------
// <copyright file="MailHelper.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Pixsys.Library.Mail.MailManager.Constants;
using Pixsys.Library.Mail.MailManager.Extensions;
using Pixsys.Library.Mail.MailManager.Models;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace Pixsys.Library.Mail.MailManager.Helpers
{
    /// <summary>
    /// Helper for mail.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1010:Opening square brackets should be spaced correctly", Justification = "Reviewed.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1000:Keywords should be spaced correctly", Justification = "Reviewed.")]
    internal static partial class MailHelper
    {
        /// <summary>
        /// The string mail list separator.
        /// </summary>
        internal static readonly string[] StringMailListSeparator = [";", ","];

        private const string ErrorMessage = "An error occured when converting the config values";

        /// <summary>
        /// Validates and transform string emails to email addresses.
        /// </summary>
        /// <remarks>The MailAddress class uses a BNF parser to validate the address in full accordance with RFC822.</remarks>
        /// <param name="emails">The list of emails to validate.</param>
        /// <returns>The validated mail addresses report.</returns>
        public static ValidatedMailAddressesReport ValidateMailAddresses(string? emails)
        {
            ValidatedMailAddressesReport report = new() { Addresses = [], NonValidEntries = [] };
            if (string.IsNullOrWhiteSpace(emails))
            {
                return report;
            }

            foreach (string uniqueMail in emails.Split(StringMailListSeparator, StringSplitOptions.RemoveEmptyEntries))
            {
                try
                {
                    report.Addresses.Add(new MailAddress(uniqueMail));
                }
                catch
                {
                    report.NonValidEntries.Add(uniqueMail);
                }
            }

            if (report.Addresses.Count != 0)
            {
                report.Addresses = report.Addresses.Distinct().ToList();
            }

            if (report.NonValidEntries.Count != 0)
            {
                report.NonValidEntries = report.NonValidEntries.Distinct().ToList();
            }

            return report;
        }

        /// <summary>
        /// Gets the template zones tags.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>The list of zone tags.</returns>
        internal static List<string> GetTemplateZonesTags(string content)
        {
            List<string> zonesTags = [];

            // Detect Zones
            Regex regex = ZoneTagRegex();
            foreach (Match match in regex.Matches(content).Cast<Match>())
            {
                foreach (Capture capture in match.Captures.Cast<Capture>())
                {
                    if (capture.Value.StartsWith(ZoneTagConstants.Prefix + "ZONE_", StringComparison.InvariantCultureIgnoreCase) && capture.Value.EndsWith(ZoneTagConstants.Suffix, StringComparison.InvariantCultureIgnoreCase))
                    {
                        zonesTags.Add(capture.Value);
                    }
                }
            }

            return zonesTags;
        }

        /// <summary>
        /// Loads the HTML template.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="templateFolderName">Name of the template folder.</param>
        /// <param name="templateHtmlPageName">Name of the template HTML page.</param>
        /// <returns>The loaded template.</returns>
        internal static string LoadHtmlTemplate(MailManagerSettings settings, string templateFolderName, string templateHtmlPageName)
        {
            string output = string.Empty;
            if (settings.TemplatesFolder != null)
            {
                string path = Path.Combine(settings.TemplatesFolder.FullName, templateFolderName, templateHtmlPageName) + ".html";
                using StreamReader reader = new(path, Encoding.GetEncoding("iso-8859-1"));
                output = reader.ReadToEnd();
            }

            return output;
        }

        /// <summary>
        /// Builds the content.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="p">The mail body parameters.</param>
        /// <param name="zone">The zone.</param>
        /// <returns>The content.</returns>
        internal static string BuildContent(MailManagerSettings settings, MailBodyParameters p, string zone)
        {
            ArgumentNullException.ThrowIfNull(settings);

            // Build the template path
            string fullPath = string.Empty;
            string contentPathWithLanguage = string.IsNullOrWhiteSpace(p.Language) ? string.Empty : Path.Combine(settings.ContentFolder.FullName, p.ContentFolderName, p.Language.ToUpper(System.Globalization.CultureInfo.CurrentCulture), p.ContentZonePrefix + "." + zone + ".html");
            string contentPathDefault = Path.Combine(settings.ContentFolder.FullName, p.ContentFolderName, p.ContentZonePrefix + "." + zone + ".html");
            string templatePathWithLanguage = string.IsNullOrWhiteSpace(p.Language) ? string.Empty : Path.Combine(settings.TemplatesFolder.FullName, p.TemplateFolderName, p.Language.ToUpper(System.Globalization.CultureInfo.CurrentCulture), p.TemplateHtmlPageName + "." + zone + ".html");
            string templatePathDefault = Path.Combine(settings.TemplatesFolder.FullName, p.TemplateFolderName, p.TemplateHtmlPageName + "." + zone + ".html");

            /*
             * Check and load the first existing content from this list:
             * 1 - content Path With Language
             * 2 - content Path Default
             * 3 - template Path With Language
             * 4 - template Path Default
             */

            foreach (string tpl in new List<string> { contentPathWithLanguage, contentPathDefault, templatePathWithLanguage, templatePathDefault })
            {
                if (string.IsNullOrWhiteSpace(fullPath) && !string.IsNullOrWhiteSpace(tpl) && File.Exists(tpl))
                {
                    fullPath = tpl;
                    break;
                }
            }

            if (string.IsNullOrWhiteSpace(fullPath))
            {
                return fullPath; // No template were found : return empty
            }

            // Load the template
            string content = string.Empty;
            using (StreamReader reader = new(fullPath, Encoding.GetEncoding("iso-8859-1")))
            {
                content = reader.ReadToEnd();
            }

            return content;
        }

        /// <summary>
        /// Creates the MailMessage object that will be sent.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="p">The mail parameters.</param>
        /// <returns>The Sent Mail report.</returns>
        internal static SentMailReport Prepare(MailManagerSettings settings, MailParameters p)
        {
            ArgumentNullException.ThrowIfNull(settings);
            SentMailReport report = new()
            {
                MailMessage = new MailMessage
                {
                    From = settings.EmailFrom,
                    Subject = p.Subject,
                    Body = p.Body,
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8,
                    DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure,
                },
                Warnings = [],
                Errors = [],
            };

            ValidatedMailAddressesReport to = ValidateMailAddresses(p.To);

            if (to?.Addresses != null && to.Addresses.Count != 0)
            {
                foreach (MailAddress recipient in to.Addresses)
                {
                    report.MailMessage.To.Add(recipient);
                }
            }

            if (to?.NonValidEntries != null && to.NonValidEntries.Count != 0)
            {
                report.Warnings.AddRange(to.NonValidEntries.Select(x => $"Email Address [{x}] is not recognized as a valid email address"));
            }

            ValidatedMailAddressesReport cc = ValidateMailAddresses(p.Cc);
            if (cc?.Addresses != null && cc.Addresses.Count != 0)
            {
                foreach (MailAddress? recipient in cc.Addresses.Where(x => !report.MailMessage.To.Equals(x)).ToList())
                {
                    report.MailMessage.CC.Add(recipient);
                }
            }

            if (cc?.NonValidEntries != null && cc.NonValidEntries.Count != 0)
            {
                report.Warnings.AddRange(cc.NonValidEntries.Select(x => $"Cc Email Address [{x}] is not recognized as a valid email address"));
            }

            ValidatedMailAddressesReport bcc = ValidateMailAddresses(p.Bcc);
            if (bcc?.Addresses != null && bcc.Addresses.Count != 0)
            {
                foreach (MailAddress? recipient in bcc.Addresses.Where(x => !report.MailMessage.To.Equals(x) && !report.MailMessage.CC.Equals(x)).ToList())
                {
                    report.MailMessage.CC.Add(recipient);
                }
            }

            if (bcc?.NonValidEntries != null && bcc.NonValidEntries.Count != 0)
            {
                report.Warnings.AddRange(bcc.NonValidEntries.Select(x => $"Bcc Email Address [{x}] is not recognized as a valid email address"));
            }

            if (!string.IsNullOrEmpty(settings.DispositionNotificationTo))
            {
                report.MailMessage.Headers.Add("Disposition-Notification-To", settings.DispositionNotificationTo);
            }

            if (!string.IsNullOrEmpty(settings.ReturnReceiptTo))
            {
                report.MailMessage.Headers.Add("Return-Receipt-To", settings.ReturnReceiptTo);
            }

            if (p?.Attachments != null && p.Attachments.Count != 0)
            {
                foreach (MailAttachment attachment in p.Attachments)
                {
                    report.MailMessage.AddAttachment(attachment);
                }
            }

            return report;
        }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>The mail manager settings.</returns>
        /// <exception cref="ArgumentNullException">Settings do not exist.</exception>
        /// <exception cref="InvalidOperationException">Cannot transform app settings to instance settings.</exception>
        internal static MailManagerSettings GetSettings(MailManagerAppSettings settings)
        {
            ArgumentNullException.ThrowIfNull(settings);
            MailManagerSettings? mailManagerSettings;
            try
            {
                mailManagerSettings = new MailManagerSettings
                {
                    SmtpClient = new SmtpClient
                    {
                        Host = settings.Host,
                        Port = settings.Port,
                        Timeout = settings.Timeout,
                        EnableSsl = settings.EnableSsl,
                        Credentials = new NetworkCredential(settings.NetworkCredentialUserName, settings.NetworkCredentialPassword),
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                    },
                    EmailFrom = new MailAddress(settings.EmailFromAddress, settings.EmailFromDisplayName),
                    TemplatesFolder = Directory.CreateDirectory(settings.TemplatesFolder),
                    ContentFolder = Directory.CreateDirectory(settings.ContentFolder),
                    DispositionNotificationTo = settings.DispositionNotificationTo,
                    ReturnReceiptTo = settings.ReturnReceiptTo,
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ErrorMessage, ex);
            }

            return mailManagerSettings;
        }

        [GeneratedRegex("{%(.*?)%}")]
        private static partial Regex ZoneTagRegex();
    }
}