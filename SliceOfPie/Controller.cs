using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SliceOfPie
{
    public static class Controller
    {
        /* Never used?
        User activeUser;
        Folder rootFolder;
        */

        public static List<Project> GetAllProjectsForUser(User user)
        {
            List <Project> allProjects = Storage.GetAllProjects();
            List <Project> projectsToReturn = new List<Project>();
            foreach (Project p in allProjects)
            {
                if (String.Compare(p.Owner.ToString(),user.ToString())==0)          
                    projectsToReturn.Add(p);
                else
                {
                    foreach (User u in p.SharedWith)
                    {
                        if (String.Compare(u.ToString(),user.ToString())==0)
                            projectsToReturn.Add(p);
                    }
                }
            }
            return projectsToReturn; 
        }

        public static void SaveDocument(Project proj, Document doc, User user)
        {
            Document docInStorage = Storage.ReadFromFile(proj.Id, doc.Id);
            if (docInStorage == null)
                Storage.WriteToFile(proj, doc);
            else
            {
                docInStorage.MergeWith(doc, user);
                Storage.WriteToFile(proj, docInStorage);
            }
        }

        public static void DeleteDocument(string pid, string id)
        {
            Storage.DeleteFile(pid,id);
        }

        public static Document OpenDocument(string pid, string did)
        {
            return Storage.ReadFromFile(pid,did);    
        }

        public static void CreateDocument(User user, string path, Project proj, string title)
        {
            Document newDocument = new Document("Insxert tet here.", title, path, user);
            SaveDocument(proj, newDocument, user);
        }

        public static void CreateProject(string title, User owner, List<User> sharedWith)
        {
            UpdateProject(title, owner, sharedWith);
        }

        public static void UpdateProject(string title, User owner, List<User> sharedWith)
        {
            Project project = new Project(title, owner, sharedWith);
            Storage.SaveProjectToFile(project);
        }

        public static void SyncWithServer()
        {
            Console.WriteLine("yay, the syncbutton was pressed!");
            //TODO DO THEMOTHERFUCKING EVERYTHING HERE :<
            using (SliceOfPieServer.SliceOfPieServiceClient serviceClient = new SliceOfPieServer.SliceOfPieServiceClient())
            {
                // Actually calling the service here
                serviceClient.SyncAll(null);
            }



        }

        /*
        public static void Main(string[] args)
        {   
            /*
            string doc1text = "Some of the things that are always hard, when doing usability tests on random persons, are to not make them feel stupid. If we give them a task which wouldn’t be that easy to solve as the first task, or a task which our program handles badly the test-person might feel stupid, but what we really want them to think is that the problem is the program and not their effort, that doesn’t get the job done.\n"+
                              "That is why we have encouraged all our test-persons to read the user manual thoroughly, as well as test out the program a bit before starting with the task list.\n"+
                              "We picked our test-persons, by going around on the university and talk to different kind of people at different stages of their education, to get some variation. As well as the students we found at the university we also tried to talk some of our family members which are of an entirely different target market, again to give us larger variation.";
            string doc2text = "Some of the things that are always hard, when doing usability tests on random persons, are to not make them feel stupid. If we give them a task which wouldn’t be that easy to solve as the first task, or a task which our program handles badly the test-person might feel stupid, but what we really want them to think is that the problem is the program and not their effort, that doesn’t get the job done.\n"+
                              "We picked our test-persons, by going around on the university and talk to different kind of people at different stages of their education, to get some variation. As well as the students we found at the university we also tried to talk some of our family members which are of an entirely different target market, again to give us larger variation.";


            Document doc1 = new Document(doc1text, "Kewins dokument", new User("Kewin"));
            Document doc2 = new Document(doc2text, "Kewins dokument", new User("Kewin"));

            List<string> changes = doc1.MergeWith(doc2);

            foreach (string s in changes)
            {
                Console.WriteLine(s);
            }
            


            //TEST AF MERGE WITH'S "CHANGES"
            string doc1text = "Jeg vil gerne snart spille computer.\n"+
                              "Fordi jeg er snart godt træt af det her work-all-day.";
            string doc2text = "Jeg vil gerne MEGET snart spille computer.\n" +
                              "Det måtte gerne være et teamspil hvis det er. \n" +
                              "Fordi jeg er snart godt træt af det her work-all-day.";

            Document doc1 = new Document(doc1text, "Kewins dokument", new User("Kewin"));
            Document doc2 = new Document(doc2text, "Kewins dokument", new User("Kewin"));

            List<string> changes = doc1.MergeWith(doc2);

            foreach (string s in changes)
            {
                Console.WriteLine(s);
            }
            */
            /*
            string doc1text = "Der var engang en dreng der hed Birger.\n" +
                               "Birger var en rigtig dum dreng og blev drillet i skolen.\n" +
                               "Det var der også god grund til fordi han var nemlig født med et mentalt handicap.\n" +
                               "Birger døde desværre i en meget voldelig sneboldskamp i skolen.\n" +
                               "Men det var nok for det bedre :)";
            string doc2text = "Der var engang en lille dreng der hed Kewin.\n" +
                              "Birger var en rigtig rigtig dum dreng og blev drillet i skolen.\n" +
                              "Det var der også god grund til fordi han var nemlig født med et mentalt handicap og lav højde.\n" +
                              "Kewin døde desværre i en meget voldelig sneboldskamp i skolen.\n" +
                              "Men det var nok for det bedre :)";

            Document doc1 = new Document(doc1text, "Kewins dokument", new User("Kewin"));
            Document doc2 = new Document(doc2text, "Kewins dokument", new User("Kewin"));

            List<string> changes = doc1.MergeWith(doc2);

            foreach (string s in changes)
            {
                Console.WriteLine(s);
            }
            */

            /*
            Document doc = new Document("Hej\nMed\ndig", "Kewins dokument", new User("Kewin", 1));
            Document.DocumentLog log = doc.GetLog();
            Document.DocumentLog.Entry e = log.GetNewestEntry();
            Console.Out.WriteLine(e.ToString());



             Test af MergeWith

            string doc1text = "Dette er et dokument.\nDokumentet indeholder linjer.\nSenere kan det også indeholde billeder.";
            string doc2text = "Dette er et dokument.\nDokumentet indeholder linjer.\nHer tester jeg om en indsat linje kommer med.\nSenere kan det også indeholde billeder.";

            Document doc1 = new Document(doc1text, "Kewins dokument", new User("Kewin", 1));
            Document doc2 = new Document(doc2text, "Kewins dokument", new User("Kewin", 1));

            doc2.MergeWith(doc1);

            Console.Out.WriteLine(doc1.GetText());


            Test af createTextArray

            string s = doc.GetText();
            Console.Out.WriteLine(s);
            string[] testArray = doc.CreateTextArray();

            for (int i = 0; i < testArray.Length; i++)
            {
                Console.Out.WriteLine("array[" + i + "] : " + testArray[i]);
            }
            Console.Out.WriteLine(doc.CreateTextArray());
        */
        }

    }



