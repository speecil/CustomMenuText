using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMenuText.HarmonyPatches
{
    internal class CustomMenuText
    {
        [HarmonyPatch(typeof(MainMenuViewController))]
        [HarmonyPatch("DidActivate")]
        internal class MainMenuViewControllerDidActivate
        {
            internal static void Postfix(ref UnityEngine.UI.Button ____soloButton, ref UnityEngine.UI.Button ____multiplayerButton, ref UnityEngine.UI.Button ____campaignButton, ref UnityEngine.UI.Button ____partyButton)
            {
                ____soloButton.GetComponentInChildren<HMUI.CurvedTextMeshPro>(____soloButton.gameObject).SetText(Config.Instance.soloText);
                ____multiplayerButton.GetComponentInChildren<HMUI.CurvedTextMeshPro>(____multiplayerButton.gameObject).SetText(Config.Instance.onlineText);
                ____campaignButton.GetComponentInChildren<HMUI.CurvedTextMeshPro>(____campaignButton.gameObject).SetText(Config.Instance.campaignText);
                ____partyButton.GetComponentInChildren<HMUI.CurvedTextMeshPro>(____partyButton.gameObject).SetText(Config.Instance.partyText);
            }
        }
    }
}
