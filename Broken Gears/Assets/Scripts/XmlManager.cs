using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class XmlManager : MonoBehaviour {

    private string path;
    public string fileName;
    public DataBase dataBase;

    GameObject camControl;

    ZoomScript zoomScript;

    //private void Start() {
    //    camControl = GameObject.Find("CamControl");
    //    zoomScript = camControl.GetComponentInChildren<ZoomScript>();
    //    path = Application.persistentDataPath;
    //    print(path);
    //    SetSliderValues();
    //}

    public void SetSliderValues() {
        dataBase = Load();
        UiManager.staticMenuScript.camSensitivity.value = dataBase.cameraSensitivity;
        zoomScript.zoomIncrease = dataBase.zoomSensitivity;
        UiManager.staticMenuScript.volume.value = dataBase.masterVolume;
        UiManager.staticMenuScript.sfx.value = dataBase.sfx;
        UiManager.staticMenuScript.music.value = dataBase.music;
    }

    public void Save() {
        dataBase = new DataBase();

        //controls
        dataBase.cameraSensitivity = UiManager.staticMenuScript.camSensitivity.value;
        dataBase.zoomSensitivity = zoomScript.zoomIncrease;

        //volume
        dataBase.masterVolume = UiManager.staticMenuScript.volume.value;
        dataBase.sfx = UiManager.staticMenuScript.sfx.value;
        dataBase.music = UiManager.staticMenuScript.music.value;

        var serializer = new XmlSerializer(typeof(DataBase));
        var stream = new FileStream(path + "/" + fileName, FileMode.Create);
        serializer.Serialize(stream, dataBase);
        stream.Close();
    }

    public DataBase Load() {
        var serializer = new XmlSerializer(typeof(DataBase));
        var stream = new FileStream(path + "/" + fileName, FileMode.Open);
        DataBase loadedDatabase = serializer.Deserialize(stream) as DataBase;
        stream.Close();
        return loadedDatabase;
    }
}
