﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLBV.ServiceReference_stt {
    using System.Data;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference_stt.Vssoft_serviceSoap")]
    public interface Vssoft_serviceSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/LayDanhSachBN", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet LayDanhSachBN(string server, string database, string acc, string pass, string Ma_Phong, int SL_BGhi);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface Vssoft_serviceSoapChannel : QLBV.ServiceReference_stt.Vssoft_serviceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Vssoft_serviceSoapClient : System.ServiceModel.ClientBase<QLBV.ServiceReference_stt.Vssoft_serviceSoap>, QLBV.ServiceReference_stt.Vssoft_serviceSoap {
        
        public Vssoft_serviceSoapClient() {
        }
        
        public Vssoft_serviceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Vssoft_serviceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Vssoft_serviceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Vssoft_serviceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Data.DataSet LayDanhSachBN(string server, string database, string acc, string pass, string Ma_Phong, int SL_BGhi) {
            return base.Channel.LayDanhSachBN(server, database, acc, pass, Ma_Phong, SL_BGhi);
        }
    }
}
