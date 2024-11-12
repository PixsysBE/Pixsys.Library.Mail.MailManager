// -----------------------------------------------------------------------
// <copyright file="MailAttachmentHelper.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Net.Mime;

namespace Pixsys.Library.Mail.MailManager.Helpers
{
    /// <summary>
    /// Helper for Mail attachment.
    /// </summary>
    internal static class MailAttachmentHelper
    {
        /// <summary>
        /// Gets the media type depending of the file extension.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>The <see cref="MediaTypeNames"/> value.</returns>
        public static string GetMediaType(string filePath)
        {
            return Path.GetExtension(filePath).ToLower(System.Globalization.CultureInfo.CurrentCulture) switch
            {
                ".jpg" => MediaTypeNames.Image.Jpeg,
                ".tiff" => MediaTypeNames.Image.Tiff,
                ".gif" => MediaTypeNames.Image.Gif,
                ".rtf" => MediaTypeNames.Text.RichText,
                ".html" => MediaTypeNames.Text.Html,
                ".txt" => MediaTypeNames.Text.Plain,
                ".xml" => MediaTypeNames.Application.Xml,
                ".json" => MediaTypeNames.Application.Json,
                ".pdf" => MediaTypeNames.Application.Pdf,
                ".zip" => MediaTypeNames.Application.Zip,
                _ => MediaTypeNames.Application.Octet,
            };
        }
    }
}