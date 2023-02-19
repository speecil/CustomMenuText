using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.GameplaySetup;
using BeatSaberMarkupLanguage.MenuButtons;
using BeatSaberMarkupLanguage.ViewControllers;
using HMUI;
using IPA.Config.Data;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;

namespace CustomMenuText.UI
{
    internal class BFlowCoordinator : FlowCoordinator
    {
        CustomMenuTextViewController view = null;


        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            if (firstActivation)
            {
                SetTitle("Custom Menu Text");
                showBackButton = true;

                if (view == null)
                    view = BeatSaberUI.CreateViewController<CustomMenuTextViewController>();

                ProvideInitialViewControllers(view);
            }
        }

        protected override void BackButtonWasPressed(ViewController topViewController)
        {
            BeatSaberUI.MainFlowCoordinator.DismissFlowCoordinator(this, null, ViewController.AnimationDirection.Horizontal);
            CustomMenuText.Config.Instance.Changed();
        }

        public void ShowFlow()
        {
            var _parentFlow = BeatSaberUI.MainFlowCoordinator.YoungestChildFlowCoordinatorOrSelf();

            BeatSaberUI.PresentFlowCoordinator(_parentFlow, this);
        }

        static BFlowCoordinator flow = null;
        static MenuButton button;

        public static void Initialize()
        {
            MenuButtons.instance.RegisterButton(button ??= new MenuButton("Custom Menu Text", "Change your menu text", () => {
                if (flow == null)
                    flow = BeatSaberUI.CreateFlowCoordinator<BFlowCoordinator>();

                flow.ShowFlow();
            }, true));
        }

        public static void Deinit()
        {
            if (button != null)
                MenuButtons.instance.UnregisterButton(button);
        }
    }

    [HotReload(RelativePathToLayout = @"./settings.bsml")]
    [ViewDefinition("CustomMenuText.UI.settings.bsml")]
    class CustomMenuTextViewController : BSMLAutomaticViewController
    {
        CustomMenuText.Config config = CustomMenuText.Config.Instance;

        string soloText
        {
            get => config.soloText;
            set => config.soloText = value;
        }

        string onlineText
        {
            get => config.onlineText;
            set => config.onlineText = value;
        }

        string partyText
        {
            get => config.partyText;
            set => config.partyText = value;
        }

        string campaignText
        {
            get => config.campaignText;
            set => config.campaignText = value;
        }




    }

    public static class BsmlWrapper
    {
        static readonly bool hasBsml = IPA.Loader.PluginManager.GetPluginFromId("BeatSaberMarkupLanguage") != null;

        public static void EnableUI()
        {
            void wrap() => BFlowCoordinator.Initialize();

            if (hasBsml)
                wrap();
        }
        public static void DisableUI()
        {
            void wrap() => BFlowCoordinator.Deinit();

            if (hasBsml)
                wrap();
        }
    }
}
