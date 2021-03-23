using enovaGitHubAnalyser.UI;
using Soneta.Business;
using Soneta.Business.UI;
using System;
using System.Linq;
using System.Threading;

// Główny folder dodatku, umieszczony w głównym widoku bazy danych
[assembly: FolderView("GitHub", // wymagane: to jest tekst na kaflu
    Priority = 0, // opcjonalne: Priority = 0 umieszcza kafel blisko lewej górnej strony widoku kafli
    Description = "Przegląd systemu GitHub", // opcjonalne: opis poniżej tytułu kafla
    BrickColor = FolderViewAttribute.BlueBrick, // opcjonalne: Kolor kafla
    Icon = "TableFolder.ico" // opcjonalne: Ikona wyświetlana na kaflu
                             // Więcej nie ma potrzeby definiować bo jest to kafel "organizacyjny" - przechodzący do widoku innych kafli
)]

[assembly: FolderView("GitHub/Przegląd commitów",
    Priority = 100,
    Description = "Przegląd comitów",
    TableName = "Commity",
    ViewType = typeof(PrzegladCommitowViewInfo)
)]

namespace enovaGitHubAnalyser.UI
{
    public class PrzegladCommitowViewInfo : ViewInfo
    {
        public PrzegladCommitowViewInfo()
        {
            // View wiążemy z odpowiednią definicją viewform.xml poprzez property ResourceName
            ResourceName = "PrzegladCommitow";

            // Inicjowanie contextu
            InitContext += PrzegladCommitowViewInfo_InitContext;

            // Tworzenie view zawierającego konkretne dane
            CreateView += PrzegladCommitowViewInfo_CreateView;
        }

        void PrzegladCommitowViewInfo_InitContext(object sender, ContextEventArgs args)
        {
            /*args.Context.Remove(typeof(WParams));
            args.Context.TryAdd(() => new WParams(args.Context));*/
        }

        void PrzegladCommitowViewInfo_CreateView(object sender, CreateViewEventArgs args)
        {
            //PrzegladCommitowViewInfo.WParams parameters;
            //if (!args.Context.Get(out parameters))
            //    return;
            //args.View = ViewCreate(parameters);
            args.View = GitHubExtension.GitHubExtensionModule.GetInstance(args.Session).Commity.CreateView();
        }

        public class WParams : ContextBase
        {
            public WParams(Context context) : base(context)
            {
            }
        }

        protected View ViewCreate(WParams pars)
        {
            View view = GitHubExtension.GitHubExtensionModule.GetInstance(pars.Session).Commity.CreateView();
            return view;
        }

    }
}
