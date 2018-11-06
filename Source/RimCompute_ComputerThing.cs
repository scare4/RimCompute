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
                return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + this.ScriptName;
            }
        }

        public RimCompute_ComputerThing()
        {

        }

        public RimCompute_ComputerThing(Thing PrevThing)
        {

        }
    }
}