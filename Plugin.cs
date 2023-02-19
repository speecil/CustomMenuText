using IPA;
using IPA.Config;
using IPA.Config.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using HarmonyLib;
using IPALogger = IPA.Logging.Logger;
using CustomMenuText.UI;
using CustomMenuText.HarmonyPatches;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace CustomMenuText
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }

        internal static bool enabled { get; private set; } = true;

        public static Harmony harmony;

        [Init]
        public void Init(IPA.Config.Config conf, IPALogger logger)
        {
            Instance = this;
            Log = logger;

            Config.Instance = conf.Generated<Config>();

            harmony = new Harmony("Speecil.BeatSaber.CustomMenuText");
        }

        [OnEnable]
        public void OnEnable()
        {
            enabled = true;
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            Config.Instance.ApplyValues();
            BsmlWrapper.EnableUI();
        }

        [OnDisable]
        public void OnDisable()
        {
            enabled = false;

            harmony.UnpatchSelf();
            BsmlWrapper.DisableUI();
        }
    }
}
