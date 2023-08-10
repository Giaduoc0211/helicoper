using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telemetry : MonoBehaviour
{
    private bool is_landing = true;
    private bool is_collide_tuong = false;
    public bool is_Landing
    {
        get { return is_landing;}
        set { is_landing = value; }
    }
    public bool is_Collide_Tuong
    {
        get { return is_collide_tuong; }
        set { is_collide_tuong = value; }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
