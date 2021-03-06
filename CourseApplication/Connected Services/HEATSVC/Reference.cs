﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     //
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HEATSVC
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "0.5.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="HEATSVC.ICourseApplication")]
    public interface ICourseApplication
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICourseApplication/CreateHEATTicket", ReplyAction="http://tempuri.org/ICourseApplication/CreateHEATTicketResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<HEATSVC.CreateHEATTicketResponse> CreateHEATTicketAsync(HEATSVC.CreateHEATTicketRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICourseApplication/UpdateHEATTicketStatus", ReplyAction="http://tempuri.org/ICourseApplication/UpdateHEATTicketStatusResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<HEATSVC.UpdateHEATTicketStatusResponse> UpdateHEATTicketStatusAsync(HEATSVC.UpdateHEATTicketStatusRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "0.5.0.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="Util.ResultObject", Namespace="http://schemas.datacontract.org/2004/07/BKIWCFService")]
    public partial class UtilResultObject
    {
        
        private UtilResultObjectDT dtField;
        
        private string errorMessageField;
        
        private string exceptionMessageField;
        
        private string innerExceptionMessageField;
        
        private string messageField;
        
        private string nameField;
        
        private object objField;
        
        private bool statusField;
        
        private bool statusFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=0)]
        public UtilResultObjectDT Dt
        {
            get
            {
                return this.dtField;
            }
            set
            {
                this.dtField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=1)]
        public string ErrorMessage
        {
            get
            {
                return this.errorMessageField;
            }
            set
            {
                this.errorMessageField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=2)]
        public string ExceptionMessage
        {
            get
            {
                return this.exceptionMessageField;
            }
            set
            {
                this.exceptionMessageField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=3)]
        public string InnerExceptionMessage
        {
            get
            {
                return this.innerExceptionMessageField;
            }
            set
            {
                this.innerExceptionMessageField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=4)]
        public string Message
        {
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=5)]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=6)]
        public object Obj
        {
            get
            {
                return this.objField;
            }
            set
            {
                this.objField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=7)]
        public bool Status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool StatusSpecified
        {
            get
            {
                return this.statusFieldSpecified;
            }
            set
            {
                this.statusFieldSpecified = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "0.5.0.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://schemas.datacontract.org/2004/07/BKIWCFService")]
    public partial class UtilResultObjectDT
    {
        
        private System.Xml.Linq.XElement[] anyField;
        
        private System.Xml.Linq.XElement any1Field;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute(Namespace="http://www.w3.org/2001/XMLSchema", Order=0)]
        public System.Xml.Linq.XElement[] Any
        {
            get
            {
                return this.anyField;
            }
            set
            {
                this.anyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute(Namespace="urn:schemas-microsoft-com:xml-diffgram-v1", Order=1)]
        public System.Xml.Linq.XElement Any1
        {
            get
            {
                return this.any1Field;
            }
            set
            {
                this.any1Field = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "0.5.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="CreateHEATTicket", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class CreateHEATTicketRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string wsClientUsername;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string wsClientPassword;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=2)]
        [System.Xml.Serialization.XmlArrayAttribute(IsNullable=true)]
        [System.Xml.Serialization.XmlArrayItemAttribute("ArrayOfstring", Namespace="http://schemas.microsoft.com/2003/10/Serialization/Arrays")]
        [System.Xml.Serialization.XmlArrayItemAttribute(Namespace="http://schemas.microsoft.com/2003/10/Serialization/Arrays", NestingLevel=1)]
        public string[][] OEArray;
        
        public CreateHEATTicketRequest()
        {
        }
        
        public CreateHEATTicketRequest(string wsClientUsername, string wsClientPassword, string[][] OEArray)
        {
            this.wsClientUsername = wsClientUsername;
            this.wsClientPassword = wsClientPassword;
            this.OEArray = OEArray;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "0.5.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="CreateHEATTicketResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class CreateHEATTicketResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public HEATSVC.UtilResultObject CreateHEATTicketResult;
        
        public CreateHEATTicketResponse()
        {
        }
        
        public CreateHEATTicketResponse(HEATSVC.UtilResultObject CreateHEATTicketResult)
        {
            this.CreateHEATTicketResult = CreateHEATTicketResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "0.5.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="UpdateHEATTicketStatus", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class UpdateHEATTicketStatusRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string wsClientUsername;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string wsClientPassword;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=2)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string ServiceReqNumb;
        
        public UpdateHEATTicketStatusRequest()
        {
        }
        
        public UpdateHEATTicketStatusRequest(string wsClientUsername, string wsClientPassword, string ServiceReqNumb)
        {
            this.wsClientUsername = wsClientUsername;
            this.wsClientPassword = wsClientPassword;
            this.ServiceReqNumb = ServiceReqNumb;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "0.5.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="UpdateHEATTicketStatusResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class UpdateHEATTicketStatusResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public HEATSVC.UtilResultObject UpdateHEATTicketStatusResult;
        
        public UpdateHEATTicketStatusResponse()
        {
        }
        
        public UpdateHEATTicketStatusResponse(HEATSVC.UtilResultObject UpdateHEATTicketStatusResult)
        {
            this.UpdateHEATTicketStatusResult = UpdateHEATTicketStatusResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "0.5.0.0")]
    public interface ICourseApplicationChannel : HEATSVC.ICourseApplication, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "0.5.0.0")]
    public partial class CourseApplicationClient : System.ServiceModel.ClientBase<HEATSVC.ICourseApplication>, HEATSVC.ICourseApplication
    {
        
    /// <summary>
    /// Implement this partial method to configure the service endpoint.
    /// </summary>
    /// <param name="serviceEndpoint">The endpoint to configure</param>
    /// <param name="clientCredentials">The client credentials</param>
    static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public CourseApplicationClient() : 
                base(CourseApplicationClient.GetDefaultBinding(), CourseApplicationClient.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.BasicHttpBinding_ICourseApplication.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public CourseApplicationClient(EndpointConfiguration endpointConfiguration) : 
                base(CourseApplicationClient.GetBindingForEndpoint(endpointConfiguration), CourseApplicationClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public CourseApplicationClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(CourseApplicationClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public CourseApplicationClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(CourseApplicationClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public CourseApplicationClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<HEATSVC.CreateHEATTicketResponse> HEATSVC.ICourseApplication.CreateHEATTicketAsync(HEATSVC.CreateHEATTicketRequest request)
        {
            return base.Channel.CreateHEATTicketAsync(request);
        }
        
        public System.Threading.Tasks.Task<HEATSVC.CreateHEATTicketResponse> CreateHEATTicketAsync(string wsClientUsername, string wsClientPassword, string[][] OEArray)
        {
            HEATSVC.CreateHEATTicketRequest inValue = new HEATSVC.CreateHEATTicketRequest();
            inValue.wsClientUsername = wsClientUsername;
            inValue.wsClientPassword = wsClientPassword;
            inValue.OEArray = OEArray;
            return ((HEATSVC.ICourseApplication)(this)).CreateHEATTicketAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<HEATSVC.UpdateHEATTicketStatusResponse> HEATSVC.ICourseApplication.UpdateHEATTicketStatusAsync(HEATSVC.UpdateHEATTicketStatusRequest request)
        {
            return base.Channel.UpdateHEATTicketStatusAsync(request);
        }
        
        public System.Threading.Tasks.Task<HEATSVC.UpdateHEATTicketStatusResponse> UpdateHEATTicketStatusAsync(string wsClientUsername, string wsClientPassword, string ServiceReqNumb)
        {
            HEATSVC.UpdateHEATTicketStatusRequest inValue = new HEATSVC.UpdateHEATTicketStatusRequest();
            inValue.wsClientUsername = wsClientUsername;
            inValue.wsClientPassword = wsClientPassword;
            inValue.ServiceReqNumb = ServiceReqNumb;
            return ((HEATSVC.ICourseApplication)(this)).UpdateHEATTicketStatusAsync(inValue);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_ICourseApplication))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_ICourseApplication))
            {
                return new System.ServiceModel.EndpointAddress("http://webapp01d-doc/wcfSAM/CourseApplication.svc");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return CourseApplicationClient.GetBindingForEndpoint(EndpointConfiguration.BasicHttpBinding_ICourseApplication);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return CourseApplicationClient.GetEndpointAddress(EndpointConfiguration.BasicHttpBinding_ICourseApplication);
        }
        
        public enum EndpointConfiguration
        {
            
            BasicHttpBinding_ICourseApplication,
        }
    }
}
