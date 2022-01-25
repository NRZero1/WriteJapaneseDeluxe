using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveSystem
{
    public static void save(Level levelConfig)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, "save.sav");
        FileStream stream = new FileStream(path, FileMode.Create);
        LevelData levelData = new LevelData(levelConfig);

        formatter.Serialize(stream, levelData);
        stream.Close();
    }

    public static bool checkSaveFile()
    {
        string path = Path.Combine(Application.persistentDataPath, "save.sav");
        bool isExist;

        if (!File.Exists(path))
        {
            isExist = false;
        }
        else
        {
            isExist = true;
        }

        return isExist;
    }

    public static LevelData load()
    {
        string path = Path.Combine(Application.persistentDataPath, "save.sav");
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);

        LevelData levelData = formatter.Deserialize(stream) as LevelData;
        stream.Close();
        
        return levelData;
    }

    public static void delete()
    {
        string path = Path.Combine(Application.persistentDataPath, "save.sav");
        File.Delete(path);
    }
}
