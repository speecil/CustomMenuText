using System.Runtime.CompilerServices;
using IPA.Config.Stores;
using CustomMenuText;
using UnityEngine;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace CustomMenuText
{
    internal class Config
    {
        public static Config Instance { get; set; }
        public string soloText = "Solo";
        public string onlineText = "Online";
        public string partyText = "Party";
        public string campaignText = "Campaign";

        public virtual void Changed() => ApplyValues();

        public void ApplyValues()
        {
            if (!Plugin.enabled) return;
        }


    }
}