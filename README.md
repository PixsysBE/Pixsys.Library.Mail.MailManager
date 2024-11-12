
# Mail Manager

This manager is using templates and content to create the html body on-the-fly. Language-specific content may be added on template and/or content level if needed.

It will will first go to the template folder and check for zones. Then it will check and load the first existing content, based on this list order:
* 1 - content with language
* 2 - content default
* 3 - template with language
* 4 - template default

Both templates and content folders need to be set with the following structure:

```
├── Templates
│   ├── Template 1
│   │   ├── template_name.html
│   │   ├── template_name.zone1_name.html
│   │   ├── template_name.zone2_name.html
│   │   ├── EN
│   │   │   ├── template_name.zone1_name.html
│   │   ├── FR
│   │   │   ├── template_name.zone2_name.html
│   ├── Template 2
│   │   ├── template_name.html
├── Content
│   ├── Content 1
│   │   ├── content_name.zone1_name.html
│   │   ├── content_name.zone2_name.html
│   │   ├── EN
│   │   │   ├── content_name.zone1_name.html
│   │   │   ├── content_name.zone2_name.html
│   │   ├── FR
│   │   │   ├── content_name.zone2_name.html
│   ├── Content 2
│   │   ├── content_name.zone2_name.html
```

## 1. Usage

### 1.1 Register your settings in `appsettings.json`

```json
  "MailSettings": {
    "Host": "<HOST>",
    "Port": "<PORT>",
    "EnableSsl": "<SSL_ENABLED>",
    "EmailFromAddress": "<EMAIL>",
    "EmailFromDisplayName": "<DISPLAY NAME>",
    "TemplatesFolder": "<TEMPLATES_FOLDER>",
    "ContentFolder": "<CONTENT_FOLDER>"
  },
```

### 1.2 Register the service in `Program.cs`

```csharp
using Pixsys.Library.Mail.MailManager;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
_ = builder.AddMailManager();

```
### 1.2 Usage

#### 1.2.1 Inject the service into your controller

```csharp
using Pixsys.Library.Mail.MailManager.Interfaces;

private readonly IMailManager _mailManager;

public MyController(IMailManager mailManager)
{
    _mailManager = mailManager;
}
```

#### 1.2.2 Methods

```csharp
var mailreport = mailManager.Send(new Pixsys.Library.Models.Mail.PrepareMailParameters
{
    Subject = <SUBJECT>,
    To = <TO>,
    Cc = <CC>,
    Bcc = <BCC>,
    Body = mailManager.GetBody(new Pixsys.Library.Models.Mail.GetEmailBodyParameters
    {
        TemplateFolderName = <MAIL_TEMPLATE_FOLDER_NAME>,
        TemplateHtmlPageName = <MAIL_TEMPLATE_HTML_PAGENAME>,
        ContentFolderName = <MAIL_CONTENT_FOLDER_NAME>,
        ContentZonePrefix = <MAIL_CONTENT_ZONE_PREFIX>
    })
});
```

Here is a basic example:
```csharp
var mailreport = mailManager.Send(new Pixsys.Library.Mail.MailManager.Models.MailParameters
{
    Subject = "Hello World!",
    To = "test@example.com",
    Body = mailManager.GetBody(new Pixsys.Library.Mail.MailManager.Models.MailBodyParameters
    {
        TemplateFolderName = "Template 1",
        TemplateHtmlPageName = "template_name",
        ContentFolderName = "Content 2",
        ContentZonePrefix = "content_name",
    }),
});
```

#### 1.2.3 Mail Report
Sending an email generates a report that may contain errors and/or warnings (i.e wrongly formatted e-mail addresses).

Some basic code to display the output:

```csharp
var sb = new StringBuilder();
sb.Append($"IsSuccessful: {mailreport.IsSuccessful}");
sb.Append("<br>Warnings:<br>");
foreach (var warning in mailreport.Warnings)
{
    sb.Append($"{warning}<br>");
}
sb.Append("<br>Errors:<br>");
foreach (var error in mailreport.Errors)
{
    sb.Append($"{error}<br>");
}
```

#### 1.2.4 Server Certificate Validation Callback

The ServerCertificateValidationCallback method can be overridden to meet your specific requirements:

```csharp
    public class CustomMailManager : MailManager
    {
        public CustomMailManager(MailManagerSettings settings) : base(settings)
        {
        }

        public override bool ServerCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
            {
                return true;
            }

            // For HTTPS requests to this specific host, we expect this specific certificate.
            // In practice, you'd want this to be configurable and allow for multiple certificates per host, to enable
            // seamless certificate rotations.
            return sender is HttpWebRequest httpWebRequest
                && httpWebRequest.RequestUri.Host == "localhost"
                && certificate is X509Certificate2 x509Certificate2
                && x509Certificate2.Thumbprint == "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"
                && sslPolicyErrors == SslPolicyErrors.RemoteCertificateChainErrors;
        }
    }
```

You can then register your custom manager instead of the default one :

```csharp
builder.AddMailManager<CustomMailManager>();
```


## 2. Resources

A basic "Welcome" email folder structure example :

```
├── Templates
│   ├── Default
│   │   ├── default.html
├── Content
│   ├── Welcome
│   │   ├── welcome2023.content.html
│   │   ├── FR
│   │   │   ├── welcome2023.title.html
``` 

Basic template html 

``` 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" name="viewport">
<title></title>
    {%ZONE_CSS%}
</head>
<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" yahoo="fix" style="font-family:Arial, Helvetica, sans-serif;">
<table width="100%" border="0" cellpadding="0" cellspacing="0" style="table-layout: fixed; margin: 0 auto;">
<tr>
	<td align="center">
        {%ZONE_WARNING%}
        {%ZONE_HEAD%}
        {%ZONE_TITLE%}	 
        <table width="700" border="0" cellpadding="0" cellspacing="0" align="center" class="deviceWidth" style="min-width:700px;">
	        <tbody>
	        <tr>
	            <td width="30" class="noDisplay"></td>
	            <td style="font-family: Arial, sans-serif; font-size: 14px; color: #333333;">
	                &nbsp;<br><br>
	                {%ZONE_CONTENT%}
	            </td>
	            <td width="30" class="noDisplay"></td>
	        </tr>
	        <tr>
                <td width="30" class="noDisplay"></td>
	            <td style="font-family: Arial, sans-serif; font-size: 14px; color: #333333;">
	                {%ZONE_SIGNATURE%}
	            </td>
                <td width="30" class="noDisplay"></td>
	        </tr>
	        </tbody>
        </table>
        {%ZONE_FOOTER%}
	</td>
</tr>
</table>
</body>
</html>
``` 