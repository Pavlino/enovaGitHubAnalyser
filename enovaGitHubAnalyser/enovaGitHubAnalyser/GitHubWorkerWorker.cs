using enovaGitHubAnalyser;
using Soneta.Business;
using Soneta.Business.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[assembly: Worker(typeof(GitHubWorkerWorker), typeof(Commity))]

namespace enovaGitHubAnalyser
{
    public class GitHubWorkerWorker
    {

        [Context]
        public GitHubWorkerWorkerParams @params
        {
            get;
            set;
        }

        [Action("GitHub/Pobierz najnowsze dane",
            Mode = ActionMode.SingleSession | ActionMode.ConfirmSave | ActionMode.Progress,
            Target = ActionTarget.ToolbarWithText)]
        public object PobierzDane()
        {
            return Task.Run(AktualizacjaDanychAsync).Result;
        }
            

        private async System.Threading.Tasks.Task<object> AktualizacjaDanychAsync()
        {
            var commity = @params.Context[typeof(Commit[]), false] as Commit[];

            try
            {
                var github = new Octokit.GitHubClient(new Octokit.ProductHeaderValue("sonetaGitHubConnector"));
                var commits = await github.Repository.Commit.GetAll("Pavlino", "enovaGitHubAnalyser");
                
                using (var t = @params.Session.Logout(true))
                {
                    foreach (Octokit.GitHubCommit c in commits)
                    {
                        if (!commity.Any(commitWithSha => commitWithSha.SHA == c.Sha))
                        {
                            Commit commit = new Commit
                            {
                                SHA = c.Sha,
                                Autor = c.Commit.Author.Name,
                                Data = c.Commit.Author.Date.UtcDateTime
                            };

                            @params.Session.AddRow(commit);
                        }
                    }

                    t.Commit();
                } 

                return "Operacja zakończona pomyślnie";

            }
            catch(Exception e)
            {
                return "Błąd poczdas operacji: " + e.Message;
            }
            

        }

        private object DodajNaSztywno()
        {
            using (var t = @params.Session.Logout(true))
            {
                //List<Commit> lstCommits = Commity.ToList();
                Commit commit = new Commit
                {
                    SHA = "test",
                    Autor = "tester",
                    Data = DateTime.UtcNow
                };
                @params.Session.AddRow(commit);
                //.lstCommits.Add(commit);
                //Commity = lstCommits.ToArray();
                t.Commit();
            }

            return "Operacja została zakończona";

        }


        public class GitHubWorkerWorkerParams : ContextBase
        {
            public GitHubWorkerWorkerParams(Context context) : base(context)
            {
            }

            public string pWlasciciel { get; set; }

            public string pNazwa { get; set; }
        }

    }
}
