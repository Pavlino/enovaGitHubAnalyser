using enovaGitHubAnalyser.GitHubExtension;
using enovaGitHubAnalyser.UI;
using Soneta.Business;
using Soneta.Business.UI;
using System;
using System.Linq;

// Główny folder dodatku, umieszczony w głównym widoku bazy danych
[assembly: FolderView("GitHub", // wymagane: to jest tekst na kaflu
    Priority = 0, // opcjonalne: Priority = 0 umieszcza kafel blisko lewej górnej strony widoku kafli
    BrickColor = FolderViewAttribute.GreyBreek, // opcjonalne: Kolor kafla
    Icon = "TableFolder.ico" // opcjonalne: Ikona wyświetlana na kaflu
                             // Więcej nie ma potrzeby definiować bo jest to kafel "organizacyjny" - przechodzący do widoku innych kafli
)]

[assembly: FolderView("GitHub/Przegląd",
    Priority = 10000,
    Description = "",
    TableName = "Commity",
    ViewType = typeof(PrzegladCommitowViewInfoViewInfo)
)]

namespace enovaGitHubAnalyser.UI
{
    public class PrzegladCommitowViewInfoViewInfo : ViewInfo
    {
        public PrzegladCommitowViewInfoViewInfo()
        {
            // View wiążemy z odpowiednią definicją viewform.xml poprzez property ResourceName
            ResourceName = "PrzegladCommitow";

            // Inicjowanie contextu
            InitContext += PrzegladCommitowViewInfoViewInfo_InitContext;

            // Tworzenie view zawierającego konkretne dane
            CreateView += PrzegladCommitowViewInfoViewInfo_CreateView;
        }

        void PrzegladCommitowViewInfoViewInfo_InitContext(object sender, ContextEventArgs args)
        {
            args.Context.Remove(typeof(WParams));
            args.Context.TryAdd(() => new WParams(args.Context));
        }

        void PrzegladCommitowViewInfoViewInfo_CreateView(object sender, CreateViewEventArgs args)
        {
            PrzegladCommitowViewInfoViewInfo.WParams parameters;
            if (!args.Context.Get(out parameters))
                return;
            args.View = ViewCreate(parameters);
        }

        public class WParams : ContextBase
        {
            public WParams(Context context) : base(context)
            {
            }
        }

        protected View ViewCreate(WParams pars)
        {
            View view = GitHubExtensionModule.GetInstance(pars.Session).Commity.CreateView();
            return view;
        }

    }
}
