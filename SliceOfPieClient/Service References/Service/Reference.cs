﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SliceOfPieClient.Service {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Service.ISliceOfPieService", SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface ISliceOfPieService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISliceOfPieService/StartSync", ReplyAction="http://tempuri.org/ISliceOfPieService/StartSyncResponse")]
        bool StartSync(SliceOfPie.User user, string projectId);
        
        [System.ServiceModel.OperationContractAttribute(IsInitiating=false, Action="http://tempuri.org/ISliceOfPieService/UpdateProject", ReplyAction="http://tempuri.org/ISliceOfPieService/UpdateProjectResponse")]
        SliceOfPie.Project UpdateProject(SliceOfPie.Project Project);
        
        [System.ServiceModel.OperationContractAttribute(IsInitiating=false, Action="http://tempuri.org/ISliceOfPieService/SendDocument", ReplyAction="http://tempuri.org/ISliceOfPieService/SendDocumentResponse")]
        void SendDocument(SliceOfPie.Document doc);
        
        [System.ServiceModel.OperationContractAttribute(IsInitiating=false, Action="http://tempuri.org/ISliceOfPieService/GetUpdatedDocument", ReplyAction="http://tempuri.org/ISliceOfPieService/GetUpdatedDocumentResponse")]
        SliceOfPie.Document GetUpdatedDocument();
        
        [System.ServiceModel.OperationContractAttribute(IsTerminating=true, IsInitiating=false, Action="http://tempuri.org/ISliceOfPieService/StopSync", ReplyAction="http://tempuri.org/ISliceOfPieService/StopSyncResponse")]
        void StopSync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISliceOfPieService/DeleteDocument", ReplyAction="http://tempuri.org/ISliceOfPieService/DeleteDocumentResponse")]
        void DeleteDocument(string projectId, string documentId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISliceOfPieService/GetAllProjectsOnServer", ReplyAction="http://tempuri.org/ISliceOfPieService/GetAllProjectsOnServerResponse")]
        System.Collections.Generic.List<SliceOfPie.Project> GetAllProjectsOnServer();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISliceOfPieService/OpenDocumentOnServer", ReplyAction="http://tempuri.org/ISliceOfPieService/OpenDocumentOnServerResponse")]
        SliceOfPie.Document OpenDocumentOnServer(string projectId, string documentId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISliceOfPieService/SaveProjectOnServer", ReplyAction="http://tempuri.org/ISliceOfPieService/SaveProjectOnServerResponse")]
        void SaveProjectOnServer(SliceOfPie.Project p);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISliceOfPieService/SaveDocumentOnServer", ReplyAction="http://tempuri.org/ISliceOfPieService/SaveDocumentOnServerResponse")]
        void SaveDocumentOnServer(SliceOfPie.Project p, SliceOfPie.Document d);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISliceOfPieService/DeleteProject", ReplyAction="http://tempuri.org/ISliceOfPieService/DeleteProjectResponse")]
        void DeleteProject(string projectId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISliceOfPieServiceChannel : SliceOfPieClient.Service.ISliceOfPieService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SliceOfPieServiceClient : System.ServiceModel.ClientBase<SliceOfPieClient.Service.ISliceOfPieService>, SliceOfPieClient.Service.ISliceOfPieService {
        
        public SliceOfPieServiceClient() {
        }
        
        public SliceOfPieServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SliceOfPieServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SliceOfPieServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SliceOfPieServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool StartSync(SliceOfPie.User user, string projectId) {
            return base.Channel.StartSync(user, projectId);
        }
        
        public SliceOfPie.Project UpdateProject(SliceOfPie.Project Project) {
            return base.Channel.UpdateProject(Project);
        }
        
        public void SendDocument(SliceOfPie.Document doc) {
            base.Channel.SendDocument(doc);
        }
        
        public SliceOfPie.Document GetUpdatedDocument() {
            return base.Channel.GetUpdatedDocument();
        }
        
        public void StopSync() {
            base.Channel.StopSync();
        }
        
        public void DeleteDocument(string projectId, string documentId) {
            base.Channel.DeleteDocument(projectId, documentId);
        }
        
        public System.Collections.Generic.List<SliceOfPie.Project> GetAllProjectsOnServer() {
            return base.Channel.GetAllProjectsOnServer();
        }
        
        public SliceOfPie.Document OpenDocumentOnServer(string projectId, string documentId) {
            return base.Channel.OpenDocumentOnServer(projectId, documentId);
        }
        
        public void SaveProjectOnServer(SliceOfPie.Project p) {
            base.Channel.SaveProjectOnServer(p);
        }
        
        public void SaveDocumentOnServer(SliceOfPie.Project p, SliceOfPie.Document d) {
            base.Channel.SaveDocumentOnServer(p, d);
        }
        
        public void DeleteProject(string projectId) {
            base.Channel.DeleteProject(projectId);
        }
    }
}
