using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
   public static void SavePlayer(Player player)
    {
        // create a file to save stuff
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.amogus"; //name doesnt matter so hehe
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);
        // change data to binary -> more secure
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.amogus";
        if (File.Exists(path))
        {
            // open save file
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            //change data back from binary
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            // if there is no save file, can't load anyting
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

}
