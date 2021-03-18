using enovaGitHubAnalyser;
using Soneta.Business;
using Soneta.Business.UI;
using System;

[assembly: Worker(typeof(GitHubExtractorWorker), typeof(GitHubCommit))]

namespace enovaGitHubAnalyser
{
    public class GitHubExtractorWorker
    {


        [Action("GitHubExtractorWorker/UpdateGitHubData", Mode = ActionMode.SingleSession | ActionMode.SingleSession | ActionMode.Progress)]
        public MessageBoxInformation UpdateGitHubData()
        {

            return new MessageBoxInformation("Pobrać najnowsze dane ?")
            {
                Text = "Opis operacji",
                YesHandler = () => "Operacja została zakończona",
                NoHandler = () => "Operacja przerwana"
            };


        }
    }


}
