using System;
using System.Reflection;
using MoonShared.APIs;
using MoonShared.Attributes;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Delegates;
using StardewValley.Triggers;
using WizardrySkill.API;
using WizardrySkill.Core.Framework;
using WizardrySkill.Core.Framework.Game.Interface; // Added for MagicMenu

namespace WizardrySkill.Core
{
    [SMod]
    public class ModEntry : Mod
    {
        internal static ModEntry Instance;
        internal static Config Config;
        internal static Assets Assets;
        internal static MapEditor Editor;
        internal static LegacyDataMigrator LegacyDataMigrator;
        internal long NewID;

        public Api Api;

        public static bool HasStardewValleyExpanded => ModEntry.Instance.Helper.ModRegistry.IsLoaded("FlashShifter.SVECode");
        internal ITranslationHelper I18N => this.Helper.Translation;
        public static IManaBarApi Mana;

        public override void Entry(IModHelper helper)
        {
            I18n.Init(helper.Translation);
            Instance = this;
            Assembly assembly = this.GetType().Assembly;
            LegacyDataMigrator = new(this.Monitor);

            // 1. Register Console Command
            helper.ConsoleCommands.Add("open_altar", "Opens the Wizardry menu remotely.", (s, a) => this.OpenAltarMenu());

            // 2. Register Button Pressed Event for F13
            helper.Events.Input.ButtonPressed += this.OnButtonPressed;

            GameLocation.RegisterTileAction("MagicAltar", Events.HandleMagicAltar);
            GameLocation.RegisterTileAction("MagicRadio", Events.HandleMagicRadio);
            ModEntry.Instance.Helper.Events.GameLoop.GameLaunched += Events.GameLaunched;

            MoonShared.Attributes.Parser.InitEvents(helper);
            MoonShared.Attributes.Parser.ParseAll(this);

            TriggerActionManager.RegisterAction(
                $"moonslime.WizardrySkill.learnedmagic",
                LearnedMagic);

            TriggerActionManager.RegisterAction(
                $"moonslime.WizardrySkill.learnedspell",
                LearnedSPell);
        }

        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            // Only trigger if world is loaded and no other menu is open
            if (!Context.IsWorldReady || Game1.activeClickableMenu != null || Game1.eventUp)
                return;

            // Check for F13
            if (e.Button == SButton.F13)
            {
                this.OpenAltarMenu();
            }
        }

        private void OpenAltarMenu()
        {
            try
            {
                // Opens the MagicMenu from your Interface folder
                Game1.activeClickableMenu = new MagicMenu();
                this.Monitor.Log("Opening Wizardry Altar menu...", LogLevel.Info);
            }
            catch (Exception ex)
            {
                this.Monitor.Log($"Error opening menu: {ex.Message}", LogLevel.Error);
            }
        }

        public override object GetApi()
        {
            try
            {
                return this.Api ??= new Api();
            }
            catch
            {
                return null;
            }
        }

        static bool LearnedMagic(string[] args, TriggerActionContext context, out string error)
        {
            if (!ArgUtility.TryGetInt(args, 1, out int points, out error, "int points"))
            {
                return false;
            }
            Utilities.LearnedMagic(points);
            return true;
        }

        static bool LearnedSPell(string[] args, TriggerActionContext context, out string error)
        {
            if (!ArgUtility.TryGet(args, 1, out string points, out error))
            {
                return false;
            }
            using System;
            using System.Reflection;
            using MoonShared.APIs;
            using MoonShared.Attributes;
            using StardewModdingAPI;
            using StardewModdingAPI.Events;
            using StardewValley;
            using StardewValley.Delegates;
            using StardewValley.Triggers;
            using WizardrySkill.API;
            using WizardrySkill.Core.Framework;
            using WizardrySkill.Core.Framework.Game.Interface; // Added for MagicMenu

namespace WizardrySkill.Core
    {
        [SMod]
        public class ModEntry : Mod
        {
            internal static ModEntry Instance;
            internal static Config Config;
            internal static Assets Assets;
            internal static MapEditor Editor;
            internal static LegacyDataMigrator LegacyDataMigrator;
            internal long NewID;

            public Api Api;

            public static bool HasStardewValleyExpanded => ModEntry.Instance.Helper.ModRegistry.IsLoaded("FlashShifter.SVECode");
            internal ITranslationHelper I18N => this.Helper.Translation;
            public static IManaBarApi Mana;

            public override void Entry(IModHelper helper)
            {
                I18n.Init(helper.Translation);
                Instance = this;
                Assembly assembly = this.GetType().Assembly;
                LegacyDataMigrator = new(this.Monitor);

                // 1. Register Console Command
                helper.ConsoleCommands.Add("open_altar", "Opens the Wizardry menu remotely.", (s, a) => this.OpenAltarMenu());

                // 2. Register Button Pressed Event for F13
                helper.Events.Input.ButtonPressed += this.OnButtonPressed;

                GameLocation.RegisterTileAction("MagicAltar", Events.HandleMagicAltar);
                GameLocation.RegisterTileAction("MagicRadio", Events.HandleMagicRadio);
                ModEntry.Instance.Helper.Events.GameLoop.GameLaunched += Events.GameLaunched;

                MoonShared.Attributes.Parser.InitEvents(helper);
                MoonShared.Attributes.Parser.ParseAll(this);

                TriggerActionManager.RegisterAction(
                    $"moonslime.WizardrySkill.learnedmagic",
                    LearnedMagic);

                TriggerActionManager.RegisterAction(
                    $"moonslime.WizardrySkill.learnedspell",
                    LearnedSPell);
            }

            private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
            {
                // Only trigger if world is loaded and no other menu is open
                if (!Context.IsWorldReady || Game1.activeClickableMenu != null || Game1.eventUp)
                    return;

                // Check for F13
                if (e.Button == SButton.F13)
                {
                    this.OpenAltarMenu();
                }
            }

            private void OpenAltarMenu()
            {
                try
                {
                    // Opens the MagicMenu from your Interface folder
                    Game1.activeClickableMenu = new MagicMenu();
                    this.Monitor.Log("Opening Wizardry Altar menu...", LogLevel.Info);
                }
                catch (Exception ex)
                {
                    this.Monitor.Log($"Error opening menu: {ex.Message}", LogLevel.Error);
                }
            }

            public override object GetApi()
            {
                try
                {
                    return this.Api ??= new Api();
                }
                catch
                {
                    return null;
                }
            }

            static bool LearnedMagic(string[] args, TriggerActionContext context, out string error)
            {
                if (!ArgUtility.TryGetInt(args, 1, out int points, out error, "int points"))
                {
                    return false;
                }
                Utilities.LearnedMagic(points);
                return true;
            }

            static bool LearnedSPell(string[] args, TriggerActionContext context, out string error)
            {
                if (!ArgUtility.TryGet(args, 1, out string points, out error))
                {
                    return false;
                }
 
