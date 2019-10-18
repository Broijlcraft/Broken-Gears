using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SFX", menuName = "SFX")]
public class Sfx : ScriptableObject {

    public GameObject soundFab;
    public AudioClip audioKlip;
    public AudioSource source;

}
