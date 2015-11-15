<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ajaxgetContracts.aspx.cs" Inherits="admin_resources_ajaxgetContracts" %>

 <script>
     $("#PeriodofPerformanceStart<%=cnt %>").datepicker();
     $("#PeriodofPerformanceEnd<%=cnt %>").datepicker();

 </script>

 <input type="hidden" id="contractCounts" name="contractCounts" value="<%=cnt %>" />
  <input type="hidden" id="AddContract<%=cnt %>" name="AddContract<%=cnt %>" value="True" />
<p>FY: <select id="FiscalYear<%=cnt %>" name="FiscalYear<%=cnt %>" >
<%=optionsFYYear %>
</select>
<p>
<label>Period of Performance</label>
<br />
<div class="date1">
Starts:   <input type="text" id="PeriodofPerformanceStart<%=cnt %>"  name="PeriodofPerformanceStart<%=cnt %>" />
</div>

<div class="date2">
Ends:  <input  type="text" id="PeriodofPerformanceEnd<%=cnt %>" name="PeriodofPerformanceEnd<%=cnt %>" />
</div>

</p>

<p>
<label>Requisition Number</label>
<br /> <input   type="text" id="RequisitionNumber<%=cnt %>"  name="RequisitionNumber<%=cnt %>" />

</p>

<p>
<label>Contract Number</label>
<br /> <input   type="text" id="ContractNumber<%=cnt %>" name="ContractNumber<%=cnt %>" />

</p>


<fieldset>
        
		<legend> How many licenses do we own?</legend>

        	<input  checked="checked"  type="radio" id="noneLicense<%=cnt %>" value="None" name="HowManyLicenses<%=cnt %>" /> <label class="inline-label" for="noneLicense<%=cnt %>">None</label> <br />
		<input  type="radio"  id="limitedToLicenses<%=cnt %>" value="limitedTo" name="HowManyLicenses<%=cnt %>" />  <label class="inline-label" for="limitedToLicenses<%=cnt %>">Limited To </label> 
       <input   style="width:50px;" type="text" id="limitedToLicensesCount<%=cnt %>" name="limitedToLicensesCount<%=cnt %>" class="validate[custom[onlyLetterNumber]]" />   Licenses <br/>
	 
        <input   type="radio" id="UnlimitedLicenses<%=cnt %>" value="Unlimited" name="HowManyLicenses<%=cnt %>" /> <label class="inline-label" for="UnlimitedLicenses<%=cnt %>">Unlimited</label>
 
        </fieldset>


</p>
<p>

<label>Annual Contract Cost</label>
<br />
$ <input  type="text" id="AnnualContractCost<%=cnt %>"   name="AnnualContractCost<%=cnt %>" class="validate[custom[onlyLetterNumber]]"  style="width:150px;" />
</p>
<p>
<label>Procurement Method</label><br />
<select onchange="procureMentChg('ProcurementMethod<%=cnt %>' , <%=cnt %>)"  id="ProcurementMethod<%=cnt %>" name="ProcurementMethod<%=cnt %>" >
<%=ProcurementMethodListOptions %>
</select>
</p>
<span id="ProcurementMethodOtherDiv<%=cnt %>" style="display:none">
<p>Please specify other Procurement Method <br /> <input  type="text" id="ProcurementMethodOther<%=cnt %>"   name="ProcurementMethodOther<%=cnt %>"  style="width:150px;" /> </p>
</span>
<p>
<label>Contract PDF</label><br />
<input type="file" id="ContractFileName<%=cnt %>" name="ContractFileName<%=cnt %>" />


<div id='cfStatus<%=cnt %>' >
<input type='hidden' name='deleteContractFile<%=cnt %>' id='deleteContractFile<%=cnt %>' value='true' />
</div>
</p>