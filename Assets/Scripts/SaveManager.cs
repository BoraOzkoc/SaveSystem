using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class SaveManager : MonoBehaviour
{
    public CubeController objToSave;
    public SaveDB SaveDB;
    private SaveData _saveData;

    private void Start()
    {
        Load();
    }

    public void Save()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(SaveDB));
        FileStream stream = new FileStream(Application.persistentDataPath + "/savedata.xml", FileMode.Create);
        serializer.Serialize(stream, SaveDB);
        stream.Close();
    }

    public void Load()
    {
        
        if (!File.Exists(Application.persistentDataPath + "/savedata.xml"))
        {
            Debug.Log("file does not exits");
            return;
        }
        XmlSerializer serializer = new XmlSerializer(typeof(SaveDB));
        FileStream stream = new FileStream(Application.persistentDataPath + "/savedata.xml", FileMode.Open);
        SaveDB = serializer.Deserialize(stream) as SaveDB;
        stream.Close();
        foreach (var save in SaveDB.SaveDataList)
        {
            objToSave.Init(save.name, save.pos);
        }
    }

    public void AddObject(GameObject obj)
    {
        SaveData saveData = new SaveData();

        saveData.name = obj.name;
        saveData.pos = obj.transform.position;

        SaveDB.SaveDataList.Add(saveData);
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            AddObject(objToSave.gameObject);

        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}

[System.Serializable]
public class SaveData
{
    public string name;
    public Vector3 pos;
}

[System.Serializable]
public class SaveDB
{
    public List<SaveData> SaveDataList = new List<SaveData>();
}