using NLua;
using System;
using Verse;

namespace RimCompute
{
    static class LuaControl
    {
        public static void Start(ThingDef Computer)
        {
            State.LoadCLRPackage();
            try
            {
                State.DoString(@" import ('RimCompute.dll', 'RimCompute')");
            }
            catch (Exception E)
            {
                LuaOutput += "Error loading RimCompute dll\n" + E + "\n";
            }
            try
            {
                State.DoString(@" local Stuf = RimLua()");
            }
            catch (Exception E)
            {
                LuaOutput += "Error loading RimCompute library\n" + E + "\n";
            }
            try
            {
                State.DoFile(Computer.GetModExtension<Script>().ScriptPath);
            }
            catch (Exception E)
            {
                LuaOutput += "Error loading script " + Computer.GetModExtension<Script>().ScriptName + "\n" + E + "\n";
            }
            
        }

        public static void End(ThingDef Computer)
        {
            State.Close();
            Computer.GetModExtension<Script>().ScriptName = "";
        }

        public static string LuaOutput;

        private static Lua State = new Lua();
    }

    public class RimLua
    {
        public static void WriteOut(object ToPrint)
        {
            try
            {
                LuaControl.LuaOutput += ToPrint.ToString();
            }
            catch
            {
                LuaControl.LuaOutput += "Could not convert to string\n";
            }
        }

        public RimLua()
        {

        }
    }
}
