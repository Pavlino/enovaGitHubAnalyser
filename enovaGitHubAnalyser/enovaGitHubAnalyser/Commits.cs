using Soneta.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// rejestracja obiektu dla przycisku "Nowy" na tym ViewInfo
[assembly: NewRow(typeof(enovaGitHubAnalyser.GitHubCommit))]

namespace enovaGitHubAnalyser
{

    public class Commits : GitHubModule.GitHubCommitTable
    {

        public bool CommitExists(string sha)
        {
            foreach (Commit commit in this.Rows)
            {
                if (commit.SHA == sha)
                {
                    return true;
                }
            }

            return false;
        }

        public void RemoveInvalidCommits(List<string> shas)
        {
            List<Commit> commitsToDelete = new List<Commit>();
            foreach (Commit commit in this.Rows)
            {
                if (!shas.Contains(commit.SHA))
                {
                    commitsToDelete.Add(commit);
                }
            }

            commitsToDelete.ForEach(c => c.Delete());
        }
    }
}
