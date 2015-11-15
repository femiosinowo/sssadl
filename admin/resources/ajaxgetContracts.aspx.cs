using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSADL.CMS;

public partial class admin_resources_ajaxgetContracts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string selectedFYear = "";
        int currentYear = DateTime.Now.Year;
        int startYear = DateTime.Now.Year - 1;
        int endYear = DateTime.Now.Year + 5;
         
        for (int i = startYear; i <= endYear; i++)
        {
            if (i.ToString() == currentYear.ToString())
            {
                selectedFYear = "selected='true'";
            }
            else
            {
                selectedFYear = "";
            }
            optionsFYYear += "<option " + selectedFYear + ">" + i.ToString() + "</option>";

        }

          cnt = Request.QueryString["count"].ToString();


          ProcurementMethodListOptions += "<option selected = 'true'  value='' >Select One</option>";
          foreach (var pmLO in ProcumentMethodList)
          {
             
              
              ProcurementMethodListOptions += "<option   value='" + pmLO.Key + "' >" + pmLO.Value + "</option>";
          }


    }

    public string optionsFYYear { get; set; }

    public string cnt { get; set; }

    public virtual Dictionary<string, string> ProcumentMethodList
    {
        get
        {
            if (ViewState["ProcumentMethodList"] == null)
            {
                ViewState["ProcumentMethodList"] = AdminFunc.GetProcumentMethodList();
            }
            return (Dictionary<string, string>)ViewState["ProcumentMethodList"];
        }
        set
        {
            ViewState["ProcumentMethodList"] = value;
        }
    }

    public string ProcurementMethodListOptions { get; set; }
}