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

    GameObject gameManager;
    GameObject camControl;

    MenuScript menuScript;
    ZoomAndSelectTile zoomAndSelectTile;

    private void Start() {
        gameManager = GameObject.Find("GameManager");
        camControl = GameObject.Find("CamControl");
        zoomAndSelectTile = camControl.GetComponentInChildren<ZoomAndSelectTile>();
        menuScript = gameManager.GetComponent<MenuScript>();
        path = Application.persistentDataPath;
        print(path);
        Save();
        SetSliderValues();
    }

    public void SetSliderValues() {
        dataBase = Load();
        menuScript.camSensitivity.value = dataBase.cameraSensitivity;
        zoomAndSelectTile.zoomIncrease = dataBase.zoomSensitivity;
        menuScript.volume.value = dataBase.masterVolume;
        menuScript.sfx.value = dataBase.sfx;
        menuScript.music.value = dataBase.music;
    }

    public void Save() {
        dataBase = new DataBase();

        //controls
        dataBase.cameraSensitivity = menuScript.camSensitivity.value;
        dataBase.zoomSensitivity = zoomAndSelectTile.zoomIncrease;

        //volume
        dataBase.masterVolume = menuScript.volume.value;
        dataBase.sfx = menuScript.sfx.value;
        dataBase.music = menuScript.music.value;

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
