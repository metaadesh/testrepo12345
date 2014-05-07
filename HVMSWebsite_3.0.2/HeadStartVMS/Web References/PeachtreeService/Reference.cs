﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.5446
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 2.0.50727.5446.
// 
#pragma warning disable 1591

namespace METAOPTION.PeachtreeService {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    using System.Data;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="HeadStartVMSPtreeSoap", Namespace="http://tempuri.org/HeadStartVMSPeachtree")]
    public partial class HeadStartVMSPtree : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback GetVendorListWithExpenseCodeOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetVendorListOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetVendorNameOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetPaymentListOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetPaymentCountByCheckOperationCompleted;
        
        private System.Threading.SendOrPostCallback ImportPaymentXMLOperationCompleted;
        
        private System.Threading.SendOrPostCallback ImportPaymentDataTablesOperationCompleted;
        
        private System.Threading.SendOrPostCallback ImportPaymentDataTablesWithExpenseCodeOperationCompleted;
        
        private System.Threading.SendOrPostCallback ImportPaymentDataTablesWithExpenseCodeAndReturnPaymentIDOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public HeadStartVMSPtree() {
            this.Url = global::METAOPTION.Properties.Settings.Default.HeadStartVMS_PeachtreeService_HeadStartVMSPtree;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event GetVendorListWithExpenseCodeCompletedEventHandler GetVendorListWithExpenseCodeCompleted;
        
        /// <remarks/>
        public event GetVendorListCompletedEventHandler GetVendorListCompleted;
        
        /// <remarks/>
        public event GetVendorNameCompletedEventHandler GetVendorNameCompleted;
        
        /// <remarks/>
        public event GetPaymentListCompletedEventHandler GetPaymentListCompleted;
        
        /// <remarks/>
        public event GetPaymentCountByCheckCompletedEventHandler GetPaymentCountByCheckCompleted;
        
        /// <remarks/>
        public event ImportPaymentXMLCompletedEventHandler ImportPaymentXMLCompleted;
        
        /// <remarks/>
        public event ImportPaymentDataTablesCompletedEventHandler ImportPaymentDataTablesCompleted;
        
        /// <remarks/>
        public event ImportPaymentDataTablesWithExpenseCodeCompletedEventHandler ImportPaymentDataTablesWithExpenseCodeCompleted;
        
        /// <remarks/>
        public event ImportPaymentDataTablesWithExpenseCodeAndReturnPaymentIDCompletedEventHandler ImportPaymentDataTablesWithExpenseCodeAndReturnPaymentIDCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/HeadStartVMSPeachtree/GetVendorListWithExpenseCode", RequestNamespace="http://tempuri.org/HeadStartVMSPeachtree", ResponseNamespace="http://tempuri.org/HeadStartVMSPeachtree", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetVendorListWithExpenseCode(string securtiyCode, bool boolTestMode) {
            object[] results = this.Invoke("GetVendorListWithExpenseCode", new object[] {
                        securtiyCode,
                        boolTestMode});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetVendorListWithExpenseCodeAsync(string securtiyCode, bool boolTestMode) {
            this.GetVendorListWithExpenseCodeAsync(securtiyCode, boolTestMode, null);
        }
        
        /// <remarks/>
        public void GetVendorListWithExpenseCodeAsync(string securtiyCode, bool boolTestMode, object userState) {
            if ((this.GetVendorListWithExpenseCodeOperationCompleted == null)) {
                this.GetVendorListWithExpenseCodeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetVendorListWithExpenseCodeOperationCompleted);
            }
            this.InvokeAsync("GetVendorListWithExpenseCode", new object[] {
                        securtiyCode,
                        boolTestMode}, this.GetVendorListWithExpenseCodeOperationCompleted, userState);
        }
        
        private void OnGetVendorListWithExpenseCodeOperationCompleted(object arg) {
            if ((this.GetVendorListWithExpenseCodeCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetVendorListWithExpenseCodeCompleted(this, new GetVendorListWithExpenseCodeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/HeadStartVMSPeachtree/GetVendorList", RequestNamespace="http://tempuri.org/HeadStartVMSPeachtree", ResponseNamespace="http://tempuri.org/HeadStartVMSPeachtree", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool GetVendorList(string securtiyCode, bool boolTestMode, out string vendorListXML, out string errorMessage) {
            object[] results = this.Invoke("GetVendorList", new object[] {
                        securtiyCode,
                        boolTestMode});
            vendorListXML = ((string)(results[1]));
            errorMessage = ((string)(results[2]));
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void GetVendorListAsync(string securtiyCode, bool boolTestMode) {
            this.GetVendorListAsync(securtiyCode, boolTestMode, null);
        }
        
        /// <remarks/>
        public void GetVendorListAsync(string securtiyCode, bool boolTestMode, object userState) {
            if ((this.GetVendorListOperationCompleted == null)) {
                this.GetVendorListOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetVendorListOperationCompleted);
            }
            this.InvokeAsync("GetVendorList", new object[] {
                        securtiyCode,
                        boolTestMode}, this.GetVendorListOperationCompleted, userState);
        }
        
        private void OnGetVendorListOperationCompleted(object arg) {
            if ((this.GetVendorListCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetVendorListCompleted(this, new GetVendorListCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/HeadStartVMSPeachtree/GetVendorName", RequestNamespace="http://tempuri.org/HeadStartVMSPeachtree", ResponseNamespace="http://tempuri.org/HeadStartVMSPeachtree", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetVendorName(string VendorID, string VendorGUID, bool boolTestMode) {
            object[] results = this.Invoke("GetVendorName", new object[] {
                        VendorID,
                        VendorGUID,
                        boolTestMode});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetVendorNameAsync(string VendorID, string VendorGUID, bool boolTestMode) {
            this.GetVendorNameAsync(VendorID, VendorGUID, boolTestMode, null);
        }
        
        /// <remarks/>
        public void GetVendorNameAsync(string VendorID, string VendorGUID, bool boolTestMode, object userState) {
            if ((this.GetVendorNameOperationCompleted == null)) {
                this.GetVendorNameOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetVendorNameOperationCompleted);
            }
            this.InvokeAsync("GetVendorName", new object[] {
                        VendorID,
                        VendorGUID,
                        boolTestMode}, this.GetVendorNameOperationCompleted, userState);
        }
        
        private void OnGetVendorNameOperationCompleted(object arg) {
            if ((this.GetVendorNameCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetVendorNameCompleted(this, new GetVendorNameCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/HeadStartVMSPeachtree/GetPaymentList", RequestNamespace="http://tempuri.org/HeadStartVMSPeachtree", ResponseNamespace="http://tempuri.org/HeadStartVMSPeachtree", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetPaymentList(string securtiyCode, bool boolTestMode) {
            object[] results = this.Invoke("GetPaymentList", new object[] {
                        securtiyCode,
                        boolTestMode});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetPaymentListAsync(string securtiyCode, bool boolTestMode) {
            this.GetPaymentListAsync(securtiyCode, boolTestMode, null);
        }
        
        /// <remarks/>
        public void GetPaymentListAsync(string securtiyCode, bool boolTestMode, object userState) {
            if ((this.GetPaymentListOperationCompleted == null)) {
                this.GetPaymentListOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetPaymentListOperationCompleted);
            }
            this.InvokeAsync("GetPaymentList", new object[] {
                        securtiyCode,
                        boolTestMode}, this.GetPaymentListOperationCompleted, userState);
        }
        
        private void OnGetPaymentListOperationCompleted(object arg) {
            if ((this.GetPaymentListCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetPaymentListCompleted(this, new GetPaymentListCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/HeadStartVMSPeachtree/GetPaymentCountByCheck", RequestNamespace="http://tempuri.org/HeadStartVMSPeachtree", ResponseNamespace="http://tempuri.org/HeadStartVMSPeachtree", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int GetPaymentCountByCheck(string CheckNumber, bool boolTestMode) {
            object[] results = this.Invoke("GetPaymentCountByCheck", new object[] {
                        CheckNumber,
                        boolTestMode});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void GetPaymentCountByCheckAsync(string CheckNumber, bool boolTestMode) {
            this.GetPaymentCountByCheckAsync(CheckNumber, boolTestMode, null);
        }
        
        /// <remarks/>
        public void GetPaymentCountByCheckAsync(string CheckNumber, bool boolTestMode, object userState) {
            if ((this.GetPaymentCountByCheckOperationCompleted == null)) {
                this.GetPaymentCountByCheckOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetPaymentCountByCheckOperationCompleted);
            }
            this.InvokeAsync("GetPaymentCountByCheck", new object[] {
                        CheckNumber,
                        boolTestMode}, this.GetPaymentCountByCheckOperationCompleted, userState);
        }
        
        private void OnGetPaymentCountByCheckOperationCompleted(object arg) {
            if ((this.GetPaymentCountByCheckCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetPaymentCountByCheckCompleted(this, new GetPaymentCountByCheckCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/HeadStartVMSPeachtree/ImportPaymentXML", RequestNamespace="http://tempuri.org/HeadStartVMSPeachtree", ResponseNamespace="http://tempuri.org/HeadStartVMSPeachtree", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool ImportPaymentXML(string xmlPayment, string securtiyCode, bool boolTestMode, out string paymentGuid, out string errorMessage) {
            object[] results = this.Invoke("ImportPaymentXML", new object[] {
                        xmlPayment,
                        securtiyCode,
                        boolTestMode});
            paymentGuid = ((string)(results[1]));
            errorMessage = ((string)(results[2]));
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void ImportPaymentXMLAsync(string xmlPayment, string securtiyCode, bool boolTestMode) {
            this.ImportPaymentXMLAsync(xmlPayment, securtiyCode, boolTestMode, null);
        }
        
        /// <remarks/>
        public void ImportPaymentXMLAsync(string xmlPayment, string securtiyCode, bool boolTestMode, object userState) {
            if ((this.ImportPaymentXMLOperationCompleted == null)) {
                this.ImportPaymentXMLOperationCompleted = new System.Threading.SendOrPostCallback(this.OnImportPaymentXMLOperationCompleted);
            }
            this.InvokeAsync("ImportPaymentXML", new object[] {
                        xmlPayment,
                        securtiyCode,
                        boolTestMode}, this.ImportPaymentXMLOperationCompleted, userState);
        }
        
        private void OnImportPaymentXMLOperationCompleted(object arg) {
            if ((this.ImportPaymentXMLCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ImportPaymentXMLCompleted(this, new ImportPaymentXMLCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/HeadStartVMSPeachtree/ImportPaymentDataTables", RequestNamespace="http://tempuri.org/HeadStartVMSPeachtree", ResponseNamespace="http://tempuri.org/HeadStartVMSPeachtree", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool ImportPaymentDataTables(string securtiyCode, bool boolTestMode, System.Data.DataTable dtPayment, System.Data.DataTable dtExpenses, out string paymentGuid, out string errorMessage) {
            object[] results = this.Invoke("ImportPaymentDataTables", new object[] {
                        securtiyCode,
                        boolTestMode,
                        dtPayment,
                        dtExpenses});
            paymentGuid = ((string)(results[1]));
            errorMessage = ((string)(results[2]));
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void ImportPaymentDataTablesAsync(string securtiyCode, bool boolTestMode, System.Data.DataTable dtPayment, System.Data.DataTable dtExpenses) {
            this.ImportPaymentDataTablesAsync(securtiyCode, boolTestMode, dtPayment, dtExpenses, null);
        }
        
        /// <remarks/>
        public void ImportPaymentDataTablesAsync(string securtiyCode, bool boolTestMode, System.Data.DataTable dtPayment, System.Data.DataTable dtExpenses, object userState) {
            if ((this.ImportPaymentDataTablesOperationCompleted == null)) {
                this.ImportPaymentDataTablesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnImportPaymentDataTablesOperationCompleted);
            }
            this.InvokeAsync("ImportPaymentDataTables", new object[] {
                        securtiyCode,
                        boolTestMode,
                        dtPayment,
                        dtExpenses}, this.ImportPaymentDataTablesOperationCompleted, userState);
        }
        
        private void OnImportPaymentDataTablesOperationCompleted(object arg) {
            if ((this.ImportPaymentDataTablesCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ImportPaymentDataTablesCompleted(this, new ImportPaymentDataTablesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/HeadStartVMSPeachtree/ImportPaymentDataTablesWithExpenseCode", RequestNamespace="http://tempuri.org/HeadStartVMSPeachtree", ResponseNamespace="http://tempuri.org/HeadStartVMSPeachtree", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool ImportPaymentDataTablesWithExpenseCode(string securtiyCode, string GLPAGUID, string GLPA, bool boolTestMode, System.Data.DataTable dtPayment, System.Data.DataTable dtExpenses, out string paymentGuid, out string errorMessage) {
            object[] results = this.Invoke("ImportPaymentDataTablesWithExpenseCode", new object[] {
                        securtiyCode,
                        GLPAGUID,
                        GLPA,
                        boolTestMode,
                        dtPayment,
                        dtExpenses});
            paymentGuid = ((string)(results[1]));
            errorMessage = ((string)(results[2]));
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void ImportPaymentDataTablesWithExpenseCodeAsync(string securtiyCode, string GLPAGUID, string GLPA, bool boolTestMode, System.Data.DataTable dtPayment, System.Data.DataTable dtExpenses) {
            this.ImportPaymentDataTablesWithExpenseCodeAsync(securtiyCode, GLPAGUID, GLPA, boolTestMode, dtPayment, dtExpenses, null);
        }
        
        /// <remarks/>
        public void ImportPaymentDataTablesWithExpenseCodeAsync(string securtiyCode, string GLPAGUID, string GLPA, bool boolTestMode, System.Data.DataTable dtPayment, System.Data.DataTable dtExpenses, object userState) {
            if ((this.ImportPaymentDataTablesWithExpenseCodeOperationCompleted == null)) {
                this.ImportPaymentDataTablesWithExpenseCodeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnImportPaymentDataTablesWithExpenseCodeOperationCompleted);
            }
            this.InvokeAsync("ImportPaymentDataTablesWithExpenseCode", new object[] {
                        securtiyCode,
                        GLPAGUID,
                        GLPA,
                        boolTestMode,
                        dtPayment,
                        dtExpenses}, this.ImportPaymentDataTablesWithExpenseCodeOperationCompleted, userState);
        }
        
        private void OnImportPaymentDataTablesWithExpenseCodeOperationCompleted(object arg) {
            if ((this.ImportPaymentDataTablesWithExpenseCodeCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ImportPaymentDataTablesWithExpenseCodeCompleted(this, new ImportPaymentDataTablesWithExpenseCodeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/HeadStartVMSPeachtree/ImportPaymentDataTablesWithExpenseCodeAn" +
            "dReturnPaymentID", RequestNamespace="http://tempuri.org/HeadStartVMSPeachtree", ResponseNamespace="http://tempuri.org/HeadStartVMSPeachtree", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool ImportPaymentDataTablesWithExpenseCodeAndReturnPaymentID(string securtiyCode, string GLPAGUID, string GLPA, bool boolTestMode, System.Data.DataTable dtPayment, System.Data.DataTable dtExpenses, out string paymentGuid, out string errorMessage, out string paymentID) {
            object[] results = this.Invoke("ImportPaymentDataTablesWithExpenseCodeAndReturnPaymentID", new object[] {
                        securtiyCode,
                        GLPAGUID,
                        GLPA,
                        boolTestMode,
                        dtPayment,
                        dtExpenses});
            paymentGuid = ((string)(results[1]));
            errorMessage = ((string)(results[2]));
            paymentID = ((string)(results[3]));
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void ImportPaymentDataTablesWithExpenseCodeAndReturnPaymentIDAsync(string securtiyCode, string GLPAGUID, string GLPA, bool boolTestMode, System.Data.DataTable dtPayment, System.Data.DataTable dtExpenses) {
            this.ImportPaymentDataTablesWithExpenseCodeAndReturnPaymentIDAsync(securtiyCode, GLPAGUID, GLPA, boolTestMode, dtPayment, dtExpenses, null);
        }
        
        /// <remarks/>
        public void ImportPaymentDataTablesWithExpenseCodeAndReturnPaymentIDAsync(string securtiyCode, string GLPAGUID, string GLPA, bool boolTestMode, System.Data.DataTable dtPayment, System.Data.DataTable dtExpenses, object userState) {
            if ((this.ImportPaymentDataTablesWithExpenseCodeAndReturnPaymentIDOperationCompleted == null)) {
                this.ImportPaymentDataTablesWithExpenseCodeAndReturnPaymentIDOperationCompleted = new System.Threading.SendOrPostCallback(this.OnImportPaymentDataTablesWithExpenseCodeAndReturnPaymentIDOperationCompleted);
            }
            this.InvokeAsync("ImportPaymentDataTablesWithExpenseCodeAndReturnPaymentID", new object[] {
                        securtiyCode,
                        GLPAGUID,
                        GLPA,
                        boolTestMode,
                        dtPayment,
                        dtExpenses}, this.ImportPaymentDataTablesWithExpenseCodeAndReturnPaymentIDOperationCompleted, userState);
        }
        
        private void OnImportPaymentDataTablesWithExpenseCodeAndReturnPaymentIDOperationCompleted(object arg) {
            if ((this.ImportPaymentDataTablesWithExpenseCodeAndReturnPaymentIDCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ImportPaymentDataTablesWithExpenseCodeAndReturnPaymentIDCompleted(this, new ImportPaymentDataTablesWithExpenseCodeAndReturnPaymentIDCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    public delegate void GetVendorListWithExpenseCodeCompletedEventHandler(object sender, GetVendorListWithExpenseCodeCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetVendorListWithExpenseCodeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetVendorListWithExpenseCodeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    public delegate void GetVendorListCompletedEventHandler(object sender, GetVendorListCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetVendorListCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetVendorListCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public string vendorListXML {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[1]));
            }
        }
        
        /// <remarks/>
        public string errorMessage {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[2]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    public delegate void GetVendorNameCompletedEventHandler(object sender, GetVendorNameCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetVendorNameCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetVendorNameCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    public delegate void GetPaymentListCompletedEventHandler(object sender, GetPaymentListCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetPaymentListCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetPaymentListCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    public delegate void GetPaymentCountByCheckCompletedEventHandler(object sender, GetPaymentCountByCheckCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetPaymentCountByCheckCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetPaymentCountByCheckCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    public delegate void ImportPaymentXMLCompletedEventHandler(object sender, ImportPaymentXMLCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ImportPaymentXMLCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ImportPaymentXMLCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public string paymentGuid {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[1]));
            }
        }
        
        /// <remarks/>
        public string errorMessage {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[2]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    public delegate void ImportPaymentDataTablesCompletedEventHandler(object sender, ImportPaymentDataTablesCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ImportPaymentDataTablesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ImportPaymentDataTablesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public string paymentGuid {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[1]));
            }
        }
        
        /// <remarks/>
        public string errorMessage {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[2]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    public delegate void ImportPaymentDataTablesWithExpenseCodeCompletedEventHandler(object sender, ImportPaymentDataTablesWithExpenseCodeCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ImportPaymentDataTablesWithExpenseCodeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ImportPaymentDataTablesWithExpenseCodeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public string paymentGuid {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[1]));
            }
        }
        
        /// <remarks/>
        public string errorMessage {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[2]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    public delegate void ImportPaymentDataTablesWithExpenseCodeAndReturnPaymentIDCompletedEventHandler(object sender, ImportPaymentDataTablesWithExpenseCodeAndReturnPaymentIDCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ImportPaymentDataTablesWithExpenseCodeAndReturnPaymentIDCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ImportPaymentDataTablesWithExpenseCodeAndReturnPaymentIDCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public string paymentGuid {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[1]));
            }
        }
        
        /// <remarks/>
        public string errorMessage {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[2]));
            }
        }
        
        /// <remarks/>
        public string paymentID {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[3]));
            }
        }
    }
}

#pragma warning restore 1591