using Abp.Configuration;
using Abp.Localization;
using Serendip.IK;
using System.Collections.Generic;

namespace Serendip.IK.Configuration
{
    public class AppSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {

            var userSettingGroup            = new SettingDefinitionGroup("notification",    new LocalizableString("notificationSettings",  CoreConsts.LocalizationSourceName));
            var tenantMailGroup             = new SettingDefinitionGroup("mail",            new LocalizableString("mailSettings",          CoreConsts.LocalizationSourceName));
            var tenantMailTemplateGroup     = new SettingDefinitionGroup("mail-templates",  new LocalizableString("mailTemplateSettings",  CoreConsts.LocalizationSourceName));
            var tenantQuoteTemplateGroup    = new SettingDefinitionGroup("quote-templates", new LocalizableString("quoteTemplateSettings", CoreConsts.LocalizationSourceName));
            var tenantSearchGroup           = new SettingDefinitionGroup("search",          new LocalizableString("searchSettings",        CoreConsts.LocalizationSourceName));
            var filesGroup                  = new SettingDefinitionGroup("files",           new LocalizableString("fileSettings",          CoreConsts.LocalizationSourceName));
            var opportunityGroup            = new SettingDefinitionGroup("opportunity",     new LocalizableString("OpportunitySettings",   CoreConsts.LocalizationSourceName));
            var userStartSettingGroup       = new SettingDefinitionGroup("start",           new LocalizableString("startSettings",         CoreConsts.LocalizationSourceName));
            var companySettingGroup         = new SettingDefinitionGroup("company",         new LocalizableString("companySettings",       CoreConsts.LocalizationSourceName));

            return new[]
            {
                 new SettingDefinition(AppSettingNames.UiTheme, "red", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, isVisibleToClients: true)
                ,new SettingDefinition("Serendip.IK.MailCount","50",scopes:SettingScopes.Tenant,isVisibleToClients:true)
                ,new SettingDefinition("Serendip.IK.WebMenuSettings", null, scopes:SettingScopes.Tenant, isVisibleToClients: false)
                ,new SettingDefinition("Serendip.IK.SendNewInvoiceNotification", null, scopes:SettingScopes.Tenant, isVisibleToClients: false)
                ,new SettingDefinition("Serendip.IK.DisableAuthorizationUser", null, scopes:SettingScopes.Tenant, isVisibleToClients: false)
                ,new SettingDefinition("Serendip.IK.DefaultAuthorizeLevel", Authorization.AuthorizeLevel.AuthorizedOnly.ToString(), scopes:SettingScopes.Tenant, isVisibleToClients: true,group:opportunityGroup,customData:new SettingType())
                ,new SettingDefinition("Serendip.IK.DefaultOpportunityCloseDays", 30.ToString(), scopes:SettingScopes.Tenant, isVisibleToClients: true,group:opportunityGroup,customData:new SettingType())
                ,new SettingDefinition("Serendip.IK.ShortDate", "dd.MM.yyyy", scopes:SettingScopes.Tenant, isVisibleToClients: true, customData:new SettingViewComponent("DateFormatSwitch","ShortDate"))
                ,new SettingDefinition("Serendip.IK.LongDate", "dd.MM.yyyy HH:mm", scopes:SettingScopes.Tenant, isVisibleToClients: true, customData:new SettingViewComponent("DateFormatSwitch","LongDate"))
                ,new SettingDefinition("Serendip.IK.Time", "HH:mm", scopes:SettingScopes.Tenant, isVisibleToClients: true, customData:new SettingViewComponent("DateFormatSwitch","Time"))
                ,new SettingDefinition("Serendip.IK.StartPage", "Activity", scopes:SettingScopes.User, isVisibleToClients: true, customData:new SettingTypeSelect(StartPageHelper.GetSettingSelectItems()), group:userStartSettingGroup)
                ,new SettingDefinition("Serendip.IK.MaxFileSize", (500*1024*1024).ToString(), scopes:SettingScopes.Tenant, isVisibleToClients: false)
                ,new SettingDefinition("Serendip.IK.MaxCustomerCount", 5.ToString(), scopes:SettingScopes.Tenant, isVisibleToClients: false)
                ,new SettingDefinition("Serendip.IK.UserDashboard", "", scopes:SettingScopes.User, isVisibleToClients: false)
                ,new SettingDefinition("Serendip.IK.TimezoneForUser", null, scopes:SettingScopes.User, isVisibleToClients: true)
                ,new SettingDefinition("Serendip.IK.DefaultFileType", "", scopes:SettingScopes.Tenant, isVisibleToClients: true,displayName:new LocalizableString("Serendip.IK.DefaultFileType", CoreConsts.LocalizationSourceName),customData:new SettingTypeTagSelect("fileTypes"))

                ,new SettingDefinition("Serendip.IK.Files.S3Url", "https://s3-eu-central-1.amazonaws.com", scopes:SettingScopes.Tenant, isVisibleToClients: false,displayName:new LocalizableString("Serendip.IK.Files.S3Url", CoreConsts.LocalizationSourceName),group:filesGroup)
                ,new SettingDefinition("Serendip.IK.Files.S3Host", "s3-eu-central-1.amazonaws.com", scopes:SettingScopes.Tenant, isVisibleToClients: false,displayName:new LocalizableString("Serendip.IK.Files.S3Url", CoreConsts.LocalizationSourceName),group:filesGroup)
                //,new SettingDefinition("Serendip.IK.Files.S3Bucket", "https://s3-eu-central-1.amazonaws.com", scopes:SettingScopes.Tenant, isVisibleToClients: false,displayName:new LocalizableString("Serendip.IK.Files.S3Bucket", CoreConsts.LocalizationSourceName),group:filehGroup)
                ,new SettingDefinition("Serendip.IK.Files.S3Bucket", "https://570614448131.signin.aws.amazon.com/console", scopes:SettingScopes.Tenant, isVisibleToClients: false,displayName:new LocalizableString("Serendip.IK.Files.S3Bucket", CoreConsts.LocalizationSourceName),group:filesGroup)
                ,new SettingDefinition("Serendip.IK.Files.UploadFolder", "files", scopes:SettingScopes.Tenant, isVisibleToClients: false,displayName:new LocalizableString("Serendip.IK.Files.UploadFolder", CoreConsts.LocalizationSourceName),group:filesGroup)
                ,new SettingDefinition("Serendip.IK.Files.S3AccessKeyId", "AKIAYJW2FQAB3YWSDX7D", scopes: SettingScopes.Tenant, isVisibleToClients: false,displayName:new LocalizableString("Serendip.IK.Files.S3AccessKeyId", CoreConsts.LocalizationSourceName),group:filesGroup)
                ,new SettingDefinition("Serendip.IK.Files.S3SecretAccessKey", "Q87pfJGSYJ4AHnjvz3FyxhDNwOOdxCrwbbvm83YY", scopes: SettingScopes.Tenant, isVisibleToClients: false,displayName:new LocalizableString("Serendip.IK.Files.S3SecretAccessKey", CoreConsts.LocalizationSourceName),group:filesGroup)

                ,new SettingDefinition("Serendip.IK.SendEmailOnNotification", "true", scopes:SettingScopes.User,customData:new SettingTypeBoolean(), isVisibleToClients: true,displayName:new LocalizableString("Serendip.IK.SendEmailOnNotification", CoreConsts.LocalizationSourceName),description:new LocalizableString("Serendip.IK.SendEmailOnNotificationDescription", CoreConsts.LocalizationSourceName),group:userSettingGroup)
                ,new SettingDefinition("Abp.Net.Mail.DefaultFromAddress", "noreply@fowapps.com",scopes:SettingScopes.Tenant,group:tenantMailGroup,isVisibleToClients:false)
                ,new SettingDefinition("Abp.Net.Mail.DefaultFromDisplayName", "FOW CRM",scopes:SettingScopes.Tenant,group:tenantMailGroup,isVisibleToClients:false)
                ,new SettingDefinition("Abp.Net.Mail.Smtp.Host", "email-smtp.eu-west-1.amazonaws.com",scopes:SettingScopes.Tenant,group:tenantMailGroup,isVisibleToClients:false)
                ,new SettingDefinition("Abp.Net.Mail.Smtp.Port", "587",scopes:SettingScopes.Tenant,group:tenantMailGroup,isVisibleToClients:false)
                ,new SettingDefinition("Abp.Net.Mail.Smtp.UserName", "AKIAYJW2FQABVCF5GHPC",scopes:SettingScopes.Tenant,group:tenantMailGroup,isVisibleToClients:false)
                ,new SettingDefinition("Abp.Net.Mail.Smtp.Password", "BN53wv8Mad+hs9CHzJG3Lgi5N7pg3qeZw5u1iHkw7QeY",scopes:SettingScopes.Tenant,group:tenantMailGroup,isVisibleToClients:false)
                //,new SettingDefinition("Abp.Net.Mail.Smtp.Domain", "red")
                ,new SettingDefinition("Abp.Net.Mail.Smtp.EnableSsl", "true",customData:new SettingTypeBoolean(),scopes:SettingScopes.Tenant,group:tenantMailGroup,isVisibleToClients:false)
                ,new SettingDefinition("Abp.Net.Mail.Smtp.UseDefaultCredentials", "false",customData:new SettingTypeBoolean(),scopes:SettingScopes.Tenant,group:tenantMailGroup,isVisibleToClients:false) 
                ,new SettingDefinition("mail.template.new-notification",@"<!doctype html>
<html xmlns='http://www.w3.org/1999/xhtml' xmlns:v='urn:schemas-microsoft-com:vml' xmlns:o='urn:schemas-microsoft-com:office:office'>
<head>
  <title></title>
  <!--[if !mso]><!-- -->
  <meta http-equiv='X-UA-Compatible' content='IE=edge'>
  <!--<![endif]-->
<meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>
<meta name='viewport' content='width=device-width, initial-scale=1.0'>
<style type='text/css'>
  #outlook a { padding: 0; }
  .ReadMsgBody { width: 100%; }
  .ExternalClass { width: 100%; }
  .ExternalClass * { line-height:100%; }
  body { margin: 0; padding: 0; -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; }
  table, td { border-collapse:collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; }
  img { border: 0; height: auto; line-height: 100%; outline: none; text-decoration: none; -ms-interpolation-mode: bicubic; }
  p { display: block; margin: 13px 0; }
</style>
<!--[if !mso]><!-->
<style type='text/css'>
  @media only screen and (max-width:480px) {
    @-ms-viewport { width:320px; }
    @viewport { width:320px; }
  }
</style>
<!--<![endif]-->
<!--[if mso]>
<xml>
  <o:OfficeDocumentSettings>
    <o:AllowPNG/>
    <o:PixelsPerInch>96</o:PixelsPerInch>
  </o:OfficeDocumentSettings>
</xml>
<![endif]-->
<!--[if lte mso 11]>
<style type='text/css'>
  .outlook-group-fix {
    width:100% !important;
  }
</style>
<![endif]-->
<!--[if !mso]><!-->
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:300,400,500,700' rel='stylesheet' type='text/css'>
    <style type='text/css'>
        @import url(https://fonts.googleapis.com/css?family=Open+Sans:300,400,500,700);
    </style>
  <!--<![endif]--><style type='text/css'>
  @media only screen and (min-width:480px) {
    .mj-column-per-100 { width:100%!important; }
  }
</style>
</head>
<body>
  
  <div class='mj-container'><!--[if mso | IE]>
      <table role='presentation' border='0' cellpadding='0' cellspacing='0' width='600' align='center' style='width:600px;'>
        <tr>
          <td style='line-height:0px;font-size:0px;mso-line-height-rule:exactly;'>
      <![endif]--><div style='margin:0px auto;max-width:600px;'><table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;' align='center' border='0'><tbody><tr><td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:20px 0px;padding-bottom:30px;padding-top:0px;'></td></tr></tbody></table></div><!--[if mso | IE]>
      </td></tr></table>
      <![endif]-->
      <!--[if mso | IE]>
      <table role='presentation' border='0' cellpadding='0' cellspacing='0' width='600' align='center' style='width:600px;'>
        <tr>
          <td style='line-height:0px;font-size:0px;mso-line-height-rule:exactly;'>
      <![endif]--><div style='margin:0px auto;max-width:600px;'><table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;' align='center' border='0'><tbody><tr><td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:20px 0px;padding-bottom:30px;'><!--[if mso | IE]>
      <table role='presentation' border='0' cellpadding='0' cellspacing='0'>
        <tr>
          <td style='vertical-align:undefined;width:244px;'>
      <![endif]--><table role='presentation' cellpadding='0' cellspacing='0' style='border-collapse:collapse;border-spacing:0px;' align='left' border='0'><tbody><tr><td style='width:244px;'></td></tr></tbody></table><!--[if mso | IE]>
      </td></tr></table>
      <![endif]--></td></tr></tbody></table></div><!--[if mso | IE]>
      </td></tr></table>
      <![endif]-->
      <!--[if mso | IE]>
      <table role='presentation' border='0' cellpadding='0' cellspacing='0' width='600' align='center' style='width:600px;'>
        <tr>
          <td style='line-height:0px;font-size:0px;mso-line-height-rule:exactly;'>
      <![endif]--><div style='margin:0px auto;border-radius:4px;max-width:600px;background:#fff;'><table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;border-radius:4px;background:#fff;' align='center' border='0'><tbody><tr><td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:1px;'><!--[if mso | IE]>
      <table role='presentation' border='0' cellpadding='0' cellspacing='0'>
        <tr>
          <td style='vertical-align:top;width:600px;'>
      <![endif]--><div class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;'><table role='presentation' cellpadding='0' cellspacing='0' style='background:white;' width='100%' border='0'><tbody><tr><td style='word-wrap:break-word;font-size:0px;padding:20px;padding-top:25px;' align='center'><div style='cursor:auto;color:#000000;font-family:Open Sans, Proxima Nova, Arial, Arial, Helvetica, sans-serif;font-size:20px;line-height:22px;text-align:center;'>1 Yeni Bildiriminiz Var IK Norm </div></td></tr><tr><td style='word-wrap:break-word;font-size:0px;padding:10px 25px;' align='center'><table role='presentation' cellpadding='0' cellspacing='0' style='border-collapse:collapse;border-spacing:0px;' align='center' border='0'><tbody><tr><td style='width:195px;'><img alt='' height='auto' src='https://suratkargo.com.tr/assets/images/iknorm/notification-mail.png' style='border:none;border-radius:0px;display:block;font-size:13px;outline:none;text-decoration:none;width:100%;height:auto;' width='195'></td></tr></tbody></table></td></tr><tr><td style='word-wrap:break-word;font-size:0px;padding:20px;' align='center'><div style='cursor:auto;color:#000000;font-family:Open Sans, Proxima Nova, Arial, Arial, Helvetica, sans-serif;font-size:14px;line-height:22px;text-align:center;'>{{scriban}}</div></td></tr><tr><td style='word-wrap:break-word;font-size:0px;padding:11px 20px;padding-bottom:30px;padding-right:30px;padding-left:30px;' align='center'><table role='presentation' cellpadding='0' cellspacing='0' style='border-collapse:separate;' align='center' border='0'><tbody><tr><td style='border:none;border-radius:3px;color:white;cursor:auto;padding:10px 25px;' align='center' valign='middle' bgcolor='#269af1'><a href='{{scriban}}' style='text-decoration:none;background:#269af1;color:white;font-family:Open Sans, Proxima Nova, Arial, Arial, Helvetica, sans-serif;font-size:14px;font-weight:400;line-height:120%;text-transform:none;margin:0px;' target='_blank'>Detayı Görüntüle</a></td></tr></tbody></table></td></tr></tbody></table></div><!--[if mso | IE]>
      </td></tr></table>
      <![endif]--></td></tr></tbody></table></div><!--[if mso | IE]>
      </td></tr></table>
      <![endif]-->
      <!--[if mso | IE]>
      <table role='presentation' border='0' cellpadding='0' cellspacing='0' width='600' align='center' style='width:600px;'>
        <tr>
          <td style='line-height:0px;font-size:0px;mso-line-height-rule:exactly;'>
      <![endif]--><div style='margin:0px auto;max-width:600px;'><table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;' align='center' border='0'><tbody><tr><td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 0px 0px;'><!--[if mso | IE]>
      <table role='presentation' border='0' cellpadding='0' cellspacing='0'>
        <tr>
          <td style='vertical-align:top;width:600px;'>
      <![endif]--><div class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;'><table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'><tbody><tr><td style='word-wrap:break-word;font-size:0px;padding:0px;' align='center'><div style='cursor:auto;color:#939daa;font-family:Open Sans, Proxima Nova, Arial, Arial, Helvetica, sans-serif;font-size:13px;line-height:22px;text-align:center;'></div></td></tr><tr><td style='word-wrap:break-word;font-size:0px;padding:0px;' align='center'></td></tr><tr><td style='word-wrap:break-word;font-size:0px;padding:0px;' align='center'><div style='cursor:auto;color:#939daa;font-family:Open Sans, Proxima Nova, Arial, Arial, Helvetica, sans-serif;font-size:13px;line-height:22px;text-align:center;'>Sürat Kargo - 2021 IK Norm</div></td></tr></tbody></table></div><!--[if mso | IE]>
      </td></tr></table>
      <![endif]--></td></tr></tbody></table></div><!--[if mso | IE]>
      </td></tr></table>
      <![endif]-->
      <!--[if mso | IE]>
      <table role='presentation' border='0' cellpadding='0' cellspacing='0' width='600' align='center' style='width:600px;'>
        <tr>
          <td style='line-height:0px;font-size:0px;mso-line-height-rule:exactly;'>
      <![endif]--><div style='margin:0px auto;max-width:600px;'><table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;' align='center' border='0'><tbody><tr><td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:20px 0px;padding-bottom:30px;padding-top:0px;'></td></tr></tbody></table></div><!--[if mso | IE]>
      </td></tr></table>
      <![endif]--></div>
</body>
</html>",scopes:SettingScopes.Tenant,group:tenantMailTemplateGroup,customData:new SettingTypeMailTeamplate(),isVisibleToClients:false)

                ,new SettingDefinition("quote.template.pdf","",scopes:SettingScopes.Tenant,group:tenantQuoteTemplateGroup,customData:new SettingTypeQuoteTeamplate(),isVisibleToClients:false)
                ,new SettingDefinition("search.elastic.enable", "false",scopes:SettingScopes.Tenant,group:tenantSearchGroup,customData:new SettingTypeBoolean(),isVisibleToClients:false)
                ,new SettingDefinition("search.elastic.url", "",scopes:SettingScopes.Tenant,group:tenantSearchGroup,isVisibleToClients:false)
                ,new SettingDefinition("search.elastic.index", "Serendip.IK",scopes:SettingScopes.Tenant,group:tenantSearchGroup,isVisibleToClients:false)
                
                //CompanySettings
                //,new SettingDefinition("Serendip.IK.Currency", "TRY", scopes:SettingScopes.Tenant, isVisibleToClients: true, group:companySettingGroup, customData:new SettingTypeSelect(CurrencyHelper.GetSettingSelectItems()))
                ,new SettingDefinition("Serendip.IK.Country", "", scopes:SettingScopes.Tenant, isVisibleToClients: true, group:companySettingGroup,customData:new SettingTypeBASelect("countries"))
                ,new SettingDefinition("Serendip.IK.TransferNotificationForUser", "true", scopes:SettingScopes.Tenant, isVisibleToClients: true, group:companySettingGroup,customData: new SettingTypeBoolean())
                ,new SettingDefinition("Serendip.IK.TransferNotificationForUserGroupManager", "true", scopes:SettingScopes.Tenant, isVisibleToClients: true, group:companySettingGroup,customData:new SettingTypeBoolean())
                ,new SettingDefinition("Serendip.IK.Timezone", "7614ea4d-d989-433f-94d7-e4a021d5f786", scopes:SettingScopes.Tenant, isVisibleToClients: true, group:companySettingGroup,customData:new SettingTypeBASelect("timezones"))
                ,new SettingDefinition("Serendip.IK.Currency", "1c7054f2-a7d2-4e1e-9946-c296ee2f97ef", scopes:SettingScopes.Tenant, isVisibleToClients: true, group:companySettingGroup,customData:new SettingTypeBASelect("currencies"))
                ,new SettingDefinition("Serendip.IK.PhoneNumberFormat", "", scopes:SettingScopes.Tenant, isVisibleToClients: true, group:companySettingGroup,customData:new SettingTypeInputTel())
                ,new SettingDefinition("Serendip.IK.FireBase", "", scopes:SettingScopes.User, isVisibleToClients: true, group:companySettingGroup,customData:new SettingTypeInputTel())
                 ,new SettingDefinition("Serendip.IK.QuoteIsRequired", "false", scopes:SettingScopes.Tenant, isVisibleToClients: true, group:companySettingGroup,customData:new SettingTypeInputTel())
        };
        }
    }
}

