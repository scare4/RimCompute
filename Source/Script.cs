using System;
using Verse;

namespace RimCompute
{
    public class Script : DefModExtension
    {
        public string ScriptName;

        public string ScriptPath
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\RimComputeScripts\\" + this.ScriptName;
            }
        }

        public bool IsScriptRunning;
    }
}