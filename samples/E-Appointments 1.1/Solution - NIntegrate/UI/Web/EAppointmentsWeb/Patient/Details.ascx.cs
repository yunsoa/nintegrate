//===============================================================================
// Microsoft Aspiring Software Architects Program
// E-Appointments - Case Study Implementation
//===============================================================================
// Copyright  Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===============================================================================

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EAppointments.UI.ServiceAgents.DirectoryService;
using EAppointments.UI.Modules;
using EAppointments.UI.Modules.Views;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb.Interfaces;
using Microsoft.Practices.CompositeWeb;
using System.IO;

public partial class Patient_Details : UserControl, IPatientDetailsView
{
    private PatientDetailsViewPresenter _presenter;
    private Guid? _patientId;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            _presenter.OnViewInitialized();
        }
        BindData();
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        PatientDataSource.DataBind();
    }

    [CreateNew]
    public PatientDetailsViewPresenter Presenter
    {
        get { return _presenter; }
        set
        {
            _presenter = value;
            _presenter.View = this;
        }
    }

    public Guid? PatientId
    {
        get { return _patientId; }
        set { _patientId = value; }
    }

    public Patient CurrentPatient
    {
        set { PatientDataSource.DataSource = value; }
    }

    public void BindData()
    {
        if (_patientId.HasValue)
            _presenter.OnViewLoaded();
        PatientDetails.DataBind();
    }

    protected String SetPicturePath(Object patientId)
    {
        String filePath = String.Format("{0}/App_Themes/Default/Images/Pictures/{1}.jpg", Request.PhysicalApplicationPath, patientId);
        if (File.Exists(filePath))
            return String.Format("~/App_Themes/Default/Images/Pictures/{0}.jpg", patientId);
        else
            return "~/App_Themes/Default/Images/nophoto.jpg";
    }
}
