// -----------------------------------------------------------------------
// <copyright file="MailExtensions.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Pixsys.Library.Mail.MailManager.Helpers;
using Pixsys.Library.Mail.MailManager.Models;
using System.Net.Mail;

namespace Pixsys.Library.Mail.MailManager.Extensions
{
    /// <summary>
    /// Mail extensions.
    /// </summary>
    internal static class MailExtensions
    {
        /// <summary>
        /// Adds an attachment to the mail.
        /// </summary>
        /// <param name="mm">The mail message.</param>
        /// <param name="attachment">The attachment.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1000:Keywords should be spaced correctly", Justification = "Reviewed.")]
        public static void AddAttachment(this MailMessage mm, MailAttachment attachment)
        {
            if (File.Exists(attachment.FilePath))
            {
                Attachment attach = new(attachment.FilePath, MailAttachmentHelper.GetMediaType(attachment.FilePath));
                System.Net.Mime.ContentDisposition? disposition = attach.ContentDisposition;
                if (disposition != null)
                {
                    disposition.CreationDate = File.GetCreationTime(attachment.FilePath);
                    disposition.ModificationDate = File.GetLastWriteTime(attachment.FilePath);
                    disposition.ReadDate = File.GetLastAccessTime(attachment.FilePath);
                    attach.Name = attachment.Name;
                    mm.Attachments.Add(attach);
                }
            }
        }
    }
}