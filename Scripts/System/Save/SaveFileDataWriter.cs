using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using Debug = ccm.Debug;

public class SaveFileDataWriter
{
    private string saveDataDirectoryPath = string.Empty;
    private string fileName = string.Empty;
    private Regex saveFilePattern = new Regex(@"^save[1-3].json$");

    public void Init()
    {
        saveDataDirectoryPath = Path.Combine(Application.persistentDataPath, Constants.Save.DIRECTORY_NAME);
    }

    private void CreateFile(SaveData data, string path)
    {
        try
        {
            if (File.Exists(saveDataDirectoryPath))
            {
                Directory.CreateDirectory(saveDataDirectoryPath);
            }

            string json = data.ToJson();
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(json);
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public void UpdateSaveFile(SaveData data)
    {
        string path = Path.Combine(saveDataDirectoryPath, fileName);
        CreateFile(data, path);
    }

    public Dictionary<string, SaveData> LoadSaveFileAll()
    {
        Dictionary<string, SaveData> datas = new Dictionary<string, SaveData>();
        DirectoryInfo di = new DirectoryInfo(saveDataDirectoryPath);

        foreach (FileInfo file in di.GetFiles())
        {
            if (!saveFilePattern.IsMatch(file.Name))
                continue;

            SaveData data = LoadSaveFile(file.Name);
            datas.Add(fileName, data);
        }

        return datas;
    }

    public SaveData LoadSaveFile(string fileName)
    {
        SaveData data = null;
        this.fileName = fileName;
        string path = Path.Combine(saveDataDirectoryPath, fileName);

        try
        {
            if (File.Exists(path))
            {
                string json = "";

                using (FileStream stream = new FileStream(path, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        json = reader.ReadToEnd();
                    }
                }

                data = JsonUtility.FromJson<SaveData>(json);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }

        return data;
    }

    public string GetFileName(int index)
    {
        fileName = Constants.Save.FILE_NAME + (index + 1) + ".json";
        return fileName;
    }


}
