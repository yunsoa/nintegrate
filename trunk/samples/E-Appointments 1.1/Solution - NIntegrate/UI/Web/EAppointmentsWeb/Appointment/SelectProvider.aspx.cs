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
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using AjaxControlToolkit;
using System.Collections.Generic;
using EAppointments.UI.ServiceAgents.DirectoryService;
using System.Collections.Specialized;
using EAppointments.UI.Modules.Views;
using Microsoft.Practices.ObjectBuilder;
using EAppointments.UI.Modules;
using System.Web.Script.Services;

public partial class Appointment_SelectProvider : Page, ISelectProviderView
{
    private SelectProviderViewPresenter _presenter;
    private Guid? _providerId;
    private bool _isNew;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            _presenter.OnViewInitialized();
        }
        _presenter.OnViewLoaded();
        SetVisibility();
    }

    private void SetVisibility()
    {
        Save.Visible = false;
        Edit.Visible = false;
        if (_providerId.HasValue)
        {
            pnlSelectProvider.Visible = false;
            pnlProviderDetails.Visible = true;
            if (_isNew)
            {
                Edit.Visible = true;
                Save.Visible = true;
            }
            providerDetails.ProviderId = _providerId;            
        }
        else
        {
            pnlSelectProvider.Visible = true;
            pnlProviderDetails.Visible = false;
            SetSearchParameters();
            Save.Visible = true;
        }  
    }

    [CreateNew]
    public SelectProviderViewPresenter Presenter
    {
        get { return _presenter; }
        set
        {
            _presenter = value;
            _presenter.View = this;
        }
    }

    public Guid? ProviderId
    {
        get { return _providerId; }
        set { _providerId = value; }
    }

    public Guid ClinicTypeId
    {
        get { return new Guid(ClinicTypeList.SelectedValue); }
    }

    public bool IsNew
    {
        set { _isNew = value; }
    }

    protected void Search_Click(object sender, ImageClickEventArgs e)
    {
        providerList.Visible = true;
        pnlMap.Visible = true;

        SetSearchParameters();
               
        providerList.BindData();
    }
     
    protected void Back_Click(object sender, ImageClickEventArgs e)
    {
        _presenter.OnBack();
    }

    protected void Next_Click(object sender, ImageClickEventArgs e)
    {
        if (_isNew && !_providerId.HasValue)
        {
            _providerId = providerList.SelectedProviderId;
        }
        if (!_providerId.HasValue)
            return;
        _presenter.OnNext();
    }

    protected void Edit_Click(object sender, ImageClickEventArgs e)
    {
        _presenter.OnEdit();
    }

    protected void Save_Click(object sender, ImageClickEventArgs e)
    {
        if (_isNew && !_providerId.HasValue)
        {
            _providerId = providerList.SelectedProviderId;
        }
        if (!_providerId.HasValue)
           return;

        _presenter.OnSave();
    }

    private void SetSearchParameters()
    {
        if (providerList.Visible)
        {
            providerList.ClinicTypeId = new Guid(ClinicTypeList.SelectedValue);
            providerList.Keywords = Keywords.Text;

            Double miles = 0d;
            if (Double.TryParse(Miles.Text, out miles))
            {
                providerList.WithinMiles = miles;
                providerList.ZipCode = ZipCode.Text;
            }

            // For the map control
            ClientScript.RegisterArrayDeclaration("providerSearchValues", String.Format("'{0}','{1}','{2}','{3}'", ClinicTypeList.SelectedValue, Keywords.Text, Miles.Text, ZipCode.Text));
        }
    }
    
    [WebMethod]
    [ScriptMethod]
    public static CascadingDropDownNameValue[] GetDropDownContentsPageMethod(string knownCategoryValues, string category)
    {
        List<CascadingDropDownNameValue> nameValues = new List<CascadingDropDownNameValue>();
        if (category.ToLower() == "specialty")
        {
            Specialty[] specialties = DirectoryServicesController.FindSpecialties();
            foreach (Specialty spec in specialties)
            {
                nameValues.Add(new CascadingDropDownNameValue(spec.Name, spec.Id.ToString()));
            }
        }
        else
        {
            StringDictionary knownCategoryValuesDictionary = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
            ClinicType[] clinicTypes = DirectoryServicesController.FindClinicTypes(new Guid(knownCategoryValuesDictionary["Specialty"]));
            foreach (ClinicType cType in clinicTypes)
            {
                nameValues.Add(new CascadingDropDownNameValue(cType.Name, cType.Id.ToString()));
            }
        }

        return nameValues.ToArray();
    }

    [WebMethod]
    [ScriptMethod]
    public static EAppointments.UI.ServiceAgents.ProviderService.Provider[] GetProviderList(Guid clinicTypeId, String keyWords, double? withinMiles, String zipCode)
    {
        return ProviderController.FindProvider(clinicTypeId, keyWords, withinMiles, zipCode);
    }
}
