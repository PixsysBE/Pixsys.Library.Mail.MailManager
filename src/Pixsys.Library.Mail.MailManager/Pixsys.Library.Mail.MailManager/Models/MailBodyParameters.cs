// -----------------------------------------------------------------------
// <copyright file="MailBodyParameters.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Pixsys.Library.Mail.MailManager.Models
{
    /// <summary>
    /// The mail body parameters model.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1206:Declaration keywords should follow order", Justification = "Reviewed.")]
    public class MailBodyParameters
    {
        /// <summary>
        /// Gets or sets the template folder name located under the settings templates folder.
        /// </summary>
        public required string TemplateFolderName { get; set; }

        /// <summary>
        /// Gets or sets the name of the template page without its extension (.html).
        /// </summary>
        public required string TemplateHtmlPageName { get; set; }

        /// <summary>
        /// Gets or sets the content folder name located under the settings content folder.
        /// </summary>
        public required string ContentFolderName { get; set; }

        /// <summary>
        /// Gets or sets the name of the content text file up to the first dot (.).
        /// </summary>
        /// <remarks>Example : value is "Welcome" for "Welcome.content.txt".</remarks>
        public required string ContentZonePrefix { get; set; }

        /// <summary>
        /// Gets or sets the language. [Optional].
        /// </summary>
        public string? Language { get; set; }
    }
}