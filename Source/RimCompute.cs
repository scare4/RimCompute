using RimWorld;
using System;
using System.IO;
using UnityEngine;
using Verse;

namespace RimCompute
{
    public class ITab_Computer : ITab
    {
        public override bool IsVisible
        {
            get
            {
                return true;
            }
        }

        public ITab_Computer()
        {
            Vector2 offsets = new Vector2(17f, 17f);
            this.size = new Vector2(595f, 536f);
            this.labelKey = "program";
        }

        private ThingDef SelectedThing
        {
            get
            {
                ThingDef Selected = null;
                if (base.SelThing != null)
                {
                    if (!(SelThing is Thing CurrThing))
                    {
                        Log.Error("Computer Tab found no thing computers to display");
                        return null;
                    }
                    Selected = CurrThing.def;
                    if (Selected == null)
                    {
                        Log.Error("Computer Tab found no def in " + CurrThing.Label);
                        return null;
                    }
                }
                return Selected;
            }
        }

        protected override void FillTab()
        {
            ThingDef selected = SelectedThing;
            Rect rect = new Rect(17f, 17f, RimCompute_Program_CardUtility.CardSize.x, RimCompute_Program_CardUtility.CardSize.y);
            RimCompute_Program_CardUtility.DrawCard(rect, selected);
        }
    }

    public class RimCompute_Program_CardUtility
    {
        public static Vector2 CardSize = new Vector2(595f, 536f);

        public static Vector2 ScrollPos = new Vector2(0f, 0f);

        public static Vector2 ScrollPos2 = new Vector2(0f, 0f);

        public static void DrawCard(Rect rect, ThingDef Selected)
        {
            GUI.BeginGroup(rect);
            string Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\RimComputeScripts";
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
            DirectoryInfo Dir = new DirectoryInfo(Path);
            FileInfo[] Files = Dir.GetFiles("*.lua");
            bool Scripts = false;
            string ScriptList = "";
            foreach (FileInfo File in Files)
            {
                ScriptList += File.Name + "\n";
                Scripts = true;
            }
            if (!Scripts)
            {
                ScriptList = "No lua scripts detected in " + Path;
            }
            Widgets.TextArea(new Rect(15f, 15f, 300f, 25f), "Scripts", true);
            Widgets.TextAreaScrollable(new Rect(15f, 50f, 300f, 300f), ScriptList, ref ScrollPos, false);
            Selected.GetModExtension<Script>().ScriptName = Widgets.TextArea(new Rect(305f, 15f, 300f, 25f), Selected.GetModExtension<Script>().ScriptName);

            bool Valid = false;
            foreach (FileInfo File in Files)
            {
                if (File.Name == Selected.GetModExtension<Script>().ScriptName)
                {
                    Valid = true;
                    break;
                }
            }

            if (Valid)
            {
                Widgets.TextArea(new Rect(325f, 85f, 100f, 25f), "Valid script", true);
                bool RunToogle = Widgets.ButtonText(new Rect(325f, 105f, 300f, 25f), "Start script", true);
                if (RunToogle)
                {
                    RunToogle = false;
                    if (!Selected.GetModExtension<Script>().IsScriptRunning)
                    {
                        LuaControl.Start(Selected);
                    }
                    else
                    {
                        LuaControl.End(Selected);
                    }
                    Selected.GetModExtension<Script>().IsScriptRunning = !Selected.GetModExtension<Script>().IsScriptRunning;
                }
            }
            else
            {
                Widgets.TextArea(new Rect(325f, 85f, 100f, 25f), "Invalid script", true);
            }
            Widgets.TextAreaScrollable(new Rect(325f, 130f, 300f, 300f), LuaControl.LuaOutput, ref ScrollPos2, false);
            GUI.EndGroup();
        }
    }

    public static class RimCompute
    {
    }
}