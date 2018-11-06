using System;
using Verse;

namespace RimCompute
{
    public class RimCompute_ComputerThing : ThingDef
    {
        public string ScriptName;

        public string StringPath
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\RimComputeScripts\\" + this.ScriptName;
            }
        }
    }
}