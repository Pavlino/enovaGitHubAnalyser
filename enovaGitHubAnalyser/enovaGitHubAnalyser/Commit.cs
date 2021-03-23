using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace enovaGitHubAnalyser
{
    public class Commit : GitHubExtension.GitHubExtensionModule.CommitRow
    {
        public override string ToString()
        {
            return Autor + " , data: " + Data;
        }
    }
}
