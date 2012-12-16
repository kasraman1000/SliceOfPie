using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SliceOfPie;

namespace SliceOfPieServiceLibrary
{
    /**
     * This is where you can define all the operations the server should provide.
     * Add a new method to the interface and add the [OperationContract] attribute
     * to that method. The implementation of the method can be written in
     * SliceOfPieService.cs
     */
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface ISliceOfPieService
    {

        // Offline synchronization methods
        [OperationContract(IsInitiating = true)]
        bool StartSync(User user, string projectId);

        [OperationContract(IsInitiating = false)]
        Project UpdateProject(Project Project);

        [OperationContract(IsInitiating = false)]
        void SendDocument(Document doc);

        [OperationContract(IsInitiating = false)]
        Document GetUpdatedDocument();

        [OperationContract(IsInitiating = false, IsTerminating = true)]
        void StopSync();


        // Web interface methods
        [OperationContract]
        void DeleteDocument(string projectId, string documentId);

        [OperationContract]
        List<Project> GetAllProjectsOnServer();
        
        [OperationContract]
        Document OpenDocumentOnServer(string projectId, string documentId);
        
        [OperationContract]
        void SaveProjectOnServer(Project p);
        
        [OperationContract]
        void SaveDocumentOnServer(Project p, Document d);

        [OperationContract]
        void DeleteProject(string projectId);
    }
}
