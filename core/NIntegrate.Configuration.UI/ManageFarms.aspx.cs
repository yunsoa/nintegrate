using System;
using System.Web.UI.WebControls;
using NIntegrate.Configuration.UI.Code.Criterias;
using System.Data;
using NIntegrate.Query;
using NIntegrate.Query.Command;

namespace NIntegrate.Configuration.UI
{
    public partial class ManageFarms : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var farmCriteria = new FarmCriteria();
                dsFarms.Criteria = farmCriteria.AddSortBy(farmCriteria.Farm_id, false);
                var envCriteria = new EnvironmentCriteria();
                dsEnvironments.Criteria = envCriteria.AddSortBy(envCriteria.Environment_id, false);
            }
        }

        protected void btnShowAddNewPanel_Click(object sender, EventArgs e)
        {
            gvFarms.SelectedIndex = -1;
            panelBottom.Controls.Hide();
            dvAddFarm.Visible = true;
        }

        protected void gvFarms_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            gvFarms.SelectedIndex = -1;
            gvServers.EditIndex = -1;
            panelBottom.Controls.Hide();
            panelAddServer.Visible = false;

            if (e.CommandName == "Select")
            {
                panelBottom.Style["display"] = "block";

                var parameters = e.CommandArgument.ToString().Split('|');

                if (parameters.Length == 2)
                {
                    hidSelectedFarm_id.Value = parameters[1];
                    panelBottom.Controls.Hide();
                    if (parameters[0] == "ViewServers")
                    {
                        var serverCriteria = new ServerCriteria();
                        dsServers.Criteria = serverCriteria.AddSortBy(serverCriteria.ServerName, false)
                            .And(serverCriteria.Farm_id == int.Parse(parameters[1]));
                        gvServers.DataBind();
                        panelServers.Visible = true;
                    }
                    else if (parameters[0] == "EditFarmAccess")
                    {
                        gvFarmAccess.DataBind();
                        gvFarmAccess.Visible = true;
                    }
                }
            }
        }

        protected void dvAddFarm_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            gvFarms.DataBind();
        }

        protected void dvAddFarm_ItemCommand(object sender, DetailsViewCommandEventArgs e)
        {
            if (e.CommandName == "Cancel")
            {
                gvFarms.SelectedIndex = -1;
                panelBottom.Controls.Hide();
            }
        }

        protected void btnShowSubAddNewPanel_Click(object sender, EventArgs e)
        {
            gvServers.SelectedIndex = -1;
            panelAddServer.Visible = true;
        }

        protected void gvServers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            gvServers.SelectedIndex = -1;
            panelAddServer.Visible = false;
        }

        protected void dvAddServer_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            gvServers.DataBind();
        }

        protected void dvAddServer_ItemCommand(object sender, DetailsViewCommandEventArgs e)
        {
            if (e.CommandName == "Cancel")
            {
                gvServers.SelectedIndex = -1;
                panelAddServer.Visible = false;
            }
        }

        protected void dvAddServer_DataBound(object sender, EventArgs e)
        {
            var ddlFarms = dvAddServer.FindControl("ddlFarms") as DropDownList;
            for (var i = 0; i < ddlFarms.Items.Count; ++i)
            {
                if (ddlFarms.Items[i].Value == hidSelectedFarm_id.Value)
                {
                    ddlFarms.SelectedIndex = i;
                    break;
                }
            }
        }

        private DataTable GetFarmAccessTable(int farm_id)
        {
            var service = new QueryService();
            var criteria = GetFarmAccessCriteria(farm_id);
            return service.Select(criteria.ToBaseCriteria());
        }

        private FarmAccessibilityCriteria GetFarmAccessCriteria(int farm_id)
        {
            var criteria = new FarmAccessibilityCriteria();
            criteria.And(criteria.ClientFarm_id == farm_id);
            return criteria;
        }

        protected void gvFarmAccess_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var farmAccessTable = GetFarmAccessTable(int.Parse(hidSelectedFarm_id.Value));
            if (bool.Parse(hidFarmAccessItemChecked.Value))
            {
                //do insert
                var isAlreadyExists = false;
                foreach (DataRow row in farmAccessTable.Rows)
                {
                    if (row["ServerFarm_id"].Equals(int.Parse(e.CommandArgument.ToString())) &&
                        ((ChannelType)row["ChannelType_id"]).ToString() == e.CommandName)
                    {
                        isAlreadyExists = true;
                        break;
                    }
                }
                if (!isAlreadyExists)
                {
                    var newRow = farmAccessTable.NewRow();
                    newRow["ClientFarm_id"] = int.Parse(hidSelectedFarm_id.Value);
                    newRow["ServerFarm_id"] = int.Parse(e.CommandArgument.ToString());
                    newRow["ChannelType_id"] = Enum.Parse(typeof (ChannelType), e.CommandName);
                    farmAccessTable.Rows.Add(newRow);
                }
            }
            else
            {
                //do delete
                foreach (DataRow row in farmAccessTable.Rows)
                {
                    if (row["ServerFarm_id"].Equals(int.Parse(e.CommandArgument.ToString())) &&
                        ((ChannelType)row["ChannelType_id"]).ToString() == e.CommandName)
                    {
                        row.Delete();
                        break;
                    }
                }
            }

            var service = new QueryService();
            service.Update(GetFarmAccessCriteria(int.Parse(hidSelectedFarm_id.Value)).ToBaseCriteria(), farmAccessTable, ConflictOption.OverwriteChanges);

            gvFarmAccess.DataBind();
        }

        protected void gvFarmAccess_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var cbMSMQ = e.Row.FindControl("cbMSMQ") as CheckBox;
                var btnMSMQ = e.Row.FindControl("btnMSMQ") as Button;
                var cbHTTP = e.Row.FindControl("cbHTTP") as CheckBox;
                var btnHTTP = e.Row.FindControl("btnHTTP") as Button;
                var cbTCP = e.Row.FindControl("cbTCP") as CheckBox;
                var btnTCP = e.Row.FindControl("btnTCP") as Button;
                var cbIPC = e.Row.FindControl("cbIPC") as CheckBox;
                var btnIPC = e.Row.FindControl("btnIPC") as Button;

                //load accessibility info
                var cbMSMQChecked = false;
                var cbHTTPChecked = false;
                var cbTCPChecked = false;
                var cbIPCChecked = false;
                var farmAccessTable = GetFarmAccessTable(int.Parse(hidSelectedFarm_id.Value));
                foreach (DataRow row in farmAccessTable.Rows)
                {
                    if (row["ServerFarm_id"].Equals((e.Row.DataItem as DataRowView)["Farm_id"]))
                    {
                        if (((ChannelType)row["ChannelType_id"]) == ChannelType.MSMQ)
                            cbMSMQChecked = true;
                        if (((ChannelType)row["ChannelType_id"]) == ChannelType.HTTP)
                            cbHTTPChecked = true;
                        if (((ChannelType)row["ChannelType_id"]) == ChannelType.TCP)
                            cbTCPChecked = true;
                        if (((ChannelType)row["ChannelType_id"]) == ChannelType.IPC)
                            cbIPCChecked = true;
                    }
                }

                cbMSMQ.Checked = cbMSMQChecked;
                cbHTTP.Checked = cbHTTPChecked;
                cbTCP.Checked = cbTCPChecked;
                cbIPC.Checked = cbIPCChecked;

                cbMSMQ.Attributes["onclick"] = string.Format(
                    "document.getElementById('{0}').value='{1}';document.getElementById('{2}').click()", hidFarmAccessItemChecked.ClientID, !cbMSMQChecked, btnMSMQ.ClientID);
                cbHTTP.Attributes["onclick"] = string.Format(
                    "document.getElementById('{0}').value='{1}';document.getElementById('{2}').click()", hidFarmAccessItemChecked.ClientID, !cbHTTPChecked, btnHTTP.ClientID);
                cbTCP.Attributes["onclick"] = string.Format(
                    "document.getElementById('{0}').value='{1}';document.getElementById('{2}').click()", hidFarmAccessItemChecked.ClientID, !cbTCPChecked, btnTCP.ClientID);
                cbIPC.Attributes["onclick"] = string.Format(
                    "document.getElementById('{0}').value='{1}';document.getElementById('{2}').click()", hidFarmAccessItemChecked.ClientID, !cbIPCChecked, btnIPC.ClientID);

                if (hidSelectedFarm_id.Value != (e.Row.DataItem as DataRowView)["Farm_id"].ToString())
                {
                    cbIPC.Visible = false;
                    btnIPC.Visible = false;
                }
            }
        }
    }
}
