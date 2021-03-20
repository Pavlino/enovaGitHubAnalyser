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
    }
}
