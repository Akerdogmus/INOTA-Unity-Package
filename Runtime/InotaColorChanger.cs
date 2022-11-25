using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InotaColorChanger : MonoBehaviour
{
    [SerializeField] private Material[] inotaMaterials;
    Renderer rend;
    GameObject[] inotaRobotBodyComponent;
    int counter = 0;
    void Start()
    {
        inotaRobotBodyComponent = GameObject.FindGameObjectsWithTag("InotaBody");
    }
    public void ColorChange()
    {
        if(inotaRobotBodyComponent.Length>0){
            for(int i=0; i<inotaRobotBodyComponent.Length;i++)
            {
                rend = inotaRobotBodyComponent[i].GetComponent<Renderer>();
                rend.sharedMaterial = inotaMaterials[counter];
            }
        }
        counter++;
        if(counter>inotaMaterials.Length-1){counter=0;}
    }
}
