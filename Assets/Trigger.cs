using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{

    [SerializeField]
    private GameObject _telemetry;
    private Telemetry telemetry;

    // Start is called before the first frame update
    void Start()
    {
        telemetry = _telemetry.GetComponent<Telemetry>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Noc" || other.tag == "Duong")
        {
            telemetry.is_Landing = true;
           
        }
        else if (other.tag == "Tuong")
        {
            Debug.Log("hssi");
            telemetry.is_Collide_Tuong = true;
        }
    }

    void OnTriggerStay(Collider other)
    { }

    void OnTriggerExit(Collider other)
    { }
}
