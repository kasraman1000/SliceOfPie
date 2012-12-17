using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SliceOfPie
{
    public static class Controller
    {
        public static List<Project> GetAllProjectsForUser(User user)
        {
            // Gets all projects currently on the filesystem.
            List<Project> allProjects = Storage.GetAllProjects();
            List<Project> projectsToReturn = new List<Project>();
            // Adds the projecs the user either owns or that are shared with him.
            foreach (Project p in allProjects)
            {
                if (String.Compare(p.Owner.ToString().ToLower(), user.ToString().ToLower()) == 0)
                    projectsToReturn.Add(p);
                else
                {
                    foreach (User u in p.SharedWith)
                    {
                        if (String.Compare(u.ToString().ToLower(), user.ToString().ToLower()) == 0)
                            projectsToReturn.Add(p);
                    }
                }
            }
            return projectsToReturn;
        }

        public static void SaveDocument(Project proj, Document doc, User user, bool fromServer = false)
        {
            // If the document exists in the storage, merge old version with newer verion.
            // Otherwise just save it to the storage.
            Document docInStorage = Storage.ReadFromFile(proj.Id, doc.Id);
            if (docInStorage == null)
                Storage.WriteToFile(proj, doc, fromServer);
            else
            {
                docInStorage.MergeWith(doc, user);
                Storage.WriteToFile(proj, docInStorage, fromServer);
            }
        }

        public static void DeleteDocument(string pid, string id)
        {
            Storage.DeleteDocument(pid, id);
        }

        public static void DeleteProject(string pid)
        {
            Storage.DeleteProject(pid);
        }
        public static void DeletePicture(string projectId, Picture picture)
        {
            Storage.DeletePicture(projectId, picture);
        }

        public static Document OpenDocument(string pid, string did)
        {
            return Storage.ReadFromFile(pid, did);
        }

        public static void CreateDocument(User user, string path, Project proj, string title)
        {
            // Save the document to the storage.
            Document newDocument = new Document("Insert your text here.", title, path, user);
            SaveDocument(proj, newDocument, user);
        }


        public static void CreateProject(string title, User owner, List<User> sharedWith)
        {
            Project project = new Project(title, owner, sharedWith);
            UpdateProject(project);
        }

        /**
         * Saves an existing project
         */
        public static void UpdateProject(Project p)
        {
            Storage.SaveProjectToFile(p);
        }

        


    }

}



