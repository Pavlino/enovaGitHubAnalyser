using enovaGitHubAnalyser;
<<<<<<< HEAD
=======
using enovaGitHubAnalyser.GitHubExtension;
>>>>>>> release
using Soneta.Business;
using Soneta.Business.UI;
using Soneta.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
<<<<<<< HEAD
using enovaGitHubAnalyser.GitHubExtension;
=======
>>>>>>> release

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

<<<<<<< HEAD
        
        private async Task<object> AktualizacjaDanychAsync()
        {

=======

        private async System.Threading.Tasks.Task<object> AktualizacjaDanychAsync()
        {
>>>>>>> release
            var commity = @params.Session.GetGitHubExtension().Commity;

            try
            {
                var github = new Octokit.GitHubClient(new Octokit.ProductHeaderValue("sonetaGitHubConnector"));
                var branches = await github.Repository.Branch.GetAll(@params.pWlasciciel, @params.pNazwa);
                //var commits = await github.Repository.Commit.GetAll("Pavlino", "enovaGitHubAnalyser");

                List<Octokit.GitHubCommit> commits = new List<Octokit.GitHubCommit>();
                foreach (var branch in branches)
                {
                    var commitRequest = new Octokit.CommitRequest();
                    commitRequest.Sha = branch.Name;
                    var branchCommits = await github.Repository.Commit.GetAll(@params.pWlasciciel, @params.pNazwa, commitRequest);

                    commits.AddRange(branchCommits);
                }

                if (commits.Count > 0)
                {
                    using (var t = @params.Session.Logout(true))
                    {
                        List<string> shas = new List<string>();
                        foreach (Octokit.GitHubCommit c in commits)
                        {
                            if (!shas.Contains(c.Sha))
                            {
                                shas.Add(c.Sha);
                                if (!commity.CommitExists(c.Sha))
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
                        }

                        commity.RemoveInvalidCommits(shas);

                        t.Commit();
                    }
                }

                return "Operacja zakończona pomyślnie";

            }
            catch (Exception e)
            {
                return "Błąd poczdas operacji: " + e.Message;
            }
<<<<<<< HEAD
            
=======

>>>>>>> release

        }

        public class GitHubWorkerWorkerParams : ContextBase
        {
            public GitHubWorkerWorkerParams(Context context) : base(context)
            {
            }

            [Caption("Właściciel repozytorium")]
            public string pWlasciciel { get; set; }

            [Caption("Nazwa repozytorium")]
            public string pNazwa { get; set; }
        }

    }
}
