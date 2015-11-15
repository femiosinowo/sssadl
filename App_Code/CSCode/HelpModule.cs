using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IO;

/// <summary>
/// Summary description for HelpModule
/// </summary>
public class HelpModule : IHttpModule
{
    public HelpModule()
    { }
    public void Dispose()
    { }
    public void Init(HttpApplication context)
    {
        context.EndRequest += new EventHandler(context_EndRequest);
    }

    void context_EndRequest(object sender, EventArgs e)
    {
        if (HttpContext.Current.Request.PhysicalPath.ToLower().IndexOf("\\workarea\\help\\") >= 0)
        {
            string helpDomainPrefix = ConfigurationManager.AppSettings["ek_helpDomainPrefix"];
            if (string.IsNullOrEmpty(helpDomainPrefix))
            {
                if (HttpContext.Current != null && HttpContext.Current.Response.StatusCode != 200
              && HttpContext.Current.Response.StatusCode != 304
              && HttpContext.Current.Response.StatusCode != 301
              && HttpContext.Current.Response.StatusCode != 302)
                {
                    if (!Directory.Exists(HttpContext.Current.Request.PhysicalPath))
                    {
                        this.Redirect();
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }
        else
        {
            return;
        }
    }

    private void Redirect()
    {
        Ektron.Cms.ContentAPI capi = new Ektron.Cms.ContentAPI();
        HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host + "/" + capi.RequestInformationRef.ApplicationPath + "/helpmessage.aspx", true);
    }
}