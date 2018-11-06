using RimWorld;
using System;
using System.IO;
using UnityEngine;
using Verse;
using RimCompute;

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

        private RimCompute_ComputerThing SelectedThing
        {
            get
            {
                RimCompute_ComputerThing Selected = null;
                if (base.SelObject != null)
                {
                    Selected = base.SelObject as RimCompute_ComputerThing;
                }
                if (Selected == null)
                {
                    Log.Error("Computer Tab found no selected computers to display", false);
                    return null;
                }
                return Selected;
            }
        }

        protected override void FillTab()
        {
            RimCompute_ComputerThing selected = SelectedThing;
            Rect rect = new Rect(17f, 17f, RimCompute_Program_CardUtility.CardSize.x, RimCompute_Program_CardUtility.CardSize.y);
            RimCompute_Program_CardUtility.DrawCard(rect, selected);
        }
    }

    public class RimCompute_Program_CardUtility
    {
        public static Vector2 CardSize = new Vector2(595f, 536f);

        public static Vector2 ScrollPos = new Vector2(0f, 0f);

        public static void DrawCard(Rect rect, RimCompute_ComputerThing Selected)
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
            Widgets.TextEntryLabeled(new Rect(325f, 15f, 400f, 25f), "Select Script", Selected.ScriptName);

            bool Valid = false;
            foreach (FileInfo File in Files)
            {
                if (File.Name == Selected.ScriptName)
                {
                    Valid = true;
                    break;
                }
            }

            if (Valid)
            {
                Widgets.TextArea(new Rect(325f, 85f, 400f, 100f), "Valid script", true);
            }
            else
            {
                Widgets.TextArea(new Rect(325f, 85f, 400f, 100f), "Invalid script", true);
            }
            GUI.EndGroup();
        }
    }

    public static class RimCompute
    {
    }
}