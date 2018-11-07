using NLua;
using Verse;

namespace RimCompute
{
    static class LuaControl
    {
        public static void Start(ThingDef Computer)
        {
            State.DoString(@" import ('RimCompute.dll', 'LuaControl')");
            State.DoFile(Computer.GetModExtension<Script>().ScriptPath);
        }

        public static void End(ThingDef Computer)
        {
            State.Close();
            Computer.GetModExtension<Script>().ScriptName = "";
        }

        public static void WriteOut(object ToPrint)
        {
            try
            {
                LuaOutput += ToPrint.ToString();
            }
            catch
            {
                LuaOutput += "Could not convert to string\n";
            }
        }

        public static string LuaOutput;

        private static Lua State = new Lua();
    }
}
