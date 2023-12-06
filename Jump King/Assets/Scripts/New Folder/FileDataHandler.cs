using System.Diagnostics;
using System.Net;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Debug = UnityEngine.Debug;
public class FileDataHandler 
{
    private string dataDirPath = "";
    
    private string dataFileName = "";
    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load()
    {
        
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;
        if(File.Exists(fullPath))
        {
            try
            {
                //loaded the serialized data from the file
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using(StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                //deserialize the data from the Json back into the C# object
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }catch(Exception e)
            {
                Debug.Log("Error occured when trying to load data from file " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }
    public void Save(GameData data)
    {
        //use Path.Combine to account for different OS's having different path seperators
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            //create directory path if not exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //Serialize the C# game data object into JSON
            string dataToStore = JsonUtility.ToJson(data, true);

            //Write the serialized data to the file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }

}
