using System;
using System.Collections.Generic;

namespace WLMPlugin
{
    /// <summary>
    /// All plugins needs to start with ClientPlugin, so they can be properly read.
    /// 
    /// This example plugin simply loads our current active playing song from WACUP.
    /// You can find WACUP here: https://getwacup.com/
    /// </summary>
    public class ClientPlugin : BaseClientPlugin
    {
        /// <summary>
        /// When our plugin is being loaded, it will first run this function before anything else.
        /// </summary>
        /// <returns>Returns true if everything returned OK</returns>
        public bool Init()
        {
            SetAuthor("Johan");
            SetName("Example WACUP Plugin");
            SetVersion("1.0.0");

            // Let's create an example PluginDataBlock
            PluginDataBlock data = new PluginDataBlock();
            data.ID = "enabled";
            data.Name = "Enable WACUP Playback reading";
            data.Type = DataBlockType.Boolean;
            data.Data = true;           // True by default
            data.SaveToConfig = true;   // Saves to the plugins/config folder
            AddToDataBlock(data);

            // Example
            PluginDataBlock data2 = new PluginDataBlock();
            data2.ID = "float_test";
            data2.Name = "Float Test";
            data2.Type = DataBlockType.Float;
            data2.Data = (float)8.5f;
            data2.Min = (float)0.0f;
            data2.Max = (float)10.0f;
            data2.EnableMinValue = true;
            data2.EnableMaxValue = true;
            data2.SaveToConfig = true;
            AddToDataBlock(data2);

            PluginDataBlock data3 = new PluginDataBlock();
            data3.ID = "int_test";
            data3.Name = "Interger Test";
            data3.Type = DataBlockType.Interger;
            data3.Data = (int)50;
            data3.Min = (int)0;
            data3.Max = (int)100;
            data3.EnableMinValue = true;
            data3.EnableMaxValue = true;
            data3.SaveToConfig = true;
            AddToDataBlock(data3);

            PluginDataBlock data4 = new PluginDataBlock();
            data4.ID = "string_test";
            data4.Name = "String Test";
            data4.Type = DataBlockType.String;
            data4.Data = "test example, does not save to config";
            AddToDataBlock(data4);

            return true;
        }

        /// <summary>
        /// Custom function, checks the newly created PluginDataBlock "enabled".
        /// </summary>
        /// <returns></returns>
        private bool IsPluginActive()
        {
            var item = GetDataBlock("enabled");
            if (item.Data != null)
                return (bool)item.Data;
            return false;
        }

        /// <summary>
        /// Called when the application requires an update from the plugin.
        /// </summary>
        /// <returns>Returns the UpdateStruct the client is going to update</returns>
        public UpdateStruct Update()
        {
            // Return a nullptr, if we are not active.
            if (!IsPluginActive())
                return null;

            // Example update struct:
            // This one uses WACUP "current playing song" as an example.
            // Since this is an example, it will contain some issues, such as not clearing if WACUP isn't playing anything (paused/stopped).
            string wcp_path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WACUP\\Logs"; // Our path
            string wcp_file = "yule_log_now.txt";       // Our txt file
            string song = "";

            // Make sure the path exist
            if (System.IO.Directory.Exists(wcp_path))
            {
                // Will override song if said file exist
                try { song = System.IO.File.ReadAllText( wcp_path + "\\" + wcp_file ); }
                catch { }
            }

            // If empty, then return nothing.
            // A null pointer also works here.
            if (song == "")
                return new UpdateStruct( null, UpdateInformation.None );

            // Overrides the comment temporarily
            string data = ":note: " + song;
            return new UpdateStruct( data, UpdateInformation.UserComment );
        }

        /// <summary>
        /// When the plugin fails the init or some critical error occured
        /// </summary>
        public void Shutdown()
        {
        }
    }
}
