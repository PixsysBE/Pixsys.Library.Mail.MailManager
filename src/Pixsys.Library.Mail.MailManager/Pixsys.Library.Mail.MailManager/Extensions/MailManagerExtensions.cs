// -----------------------------------------------------------------------
// <copyright file="MailManagerExtensions.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Pixsys.Library.Mail.MailManager.Helpers;
using Pixsys.Library.Mail.MailManager.Interfaces;
using Pixsys.Library.Mail.MailManager.Models;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Pixsys.Library.Mail.MailManager
#pragma warning restore IDE0130 // Namespace does not match folder structure
{
    /// <summary>
    /// Mail Manager extensions.
    /// </summary>
    public static class MailManagerExtensions
    {
        /// <summary>
        /// Adds the mail manager.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The updated application.</returns>
        /// <exception cref="InvalidOperationException">An error occured when converting the config values.</exception>
        public static WebApplicationBuilder AddMailManager(this WebApplicationBuilder builder)
        {
            if (!builder.Services.Any(x => x.ServiceType == typeof(IMailManager)))
            {
                MailManagerAppSettings? settings = builder.Configuration.GetSection("MailSettings").Get<MailManagerAppSettings>();
                ArgumentNullException.ThrowIfNull(settings);
                MailManagerSettings? mailManagerSettings = MailHelper.GetSettings(settings);
                builder.Services.TryAddSingleton<IMailManager>(new MailManager(mailManagerSettings));
            }

            return builder;
        }

        /// <summary>
        /// Adds the mail manager.
        /// </summary>
        /// <typeparam name="T">The custom instance generic type.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns>The updated builder.</returns>
        public static WebApplicationBuilder AddMailManager<T>(this WebApplicationBuilder builder)
        {
            if (!builder.Services.Any(x => x.ServiceType == typeof(IMailManager)))
            {
                MailManagerAppSettings? settings = builder.Configuration.GetSection("MailSettings").Get<MailManagerAppSettings>();
                ArgumentNullException.ThrowIfNull(settings);
                MailManagerSettings? mailManagerSettings = MailHelper.GetSettings(settings);

                T? customInstance = (T?)Activator.CreateInstance(typeof(T), new object[] { mailManagerSettings });
                if (!EqualityComparer<T?>.Default.Equals(customInstance, default))
                {
#pragma warning disable CS8604 // Possible null reference argument.
                    builder.Services.TryAddSingleton<IMailManager>(customInstance as MailManager);
#pragma warning restore CS8604 // Possible null reference argument.
                }
                else
                {
                    builder.Services.TryAddSingleton<IMailManager>(new MailManager(mailManagerSettings));
                }
            }

            return builder;
        }
    }
}