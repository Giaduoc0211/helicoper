using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class helicopter : MonoBehaviour
{
    [SerializeField]
    private GameObject canhquat;
    [SerializeField]
    private GameObject helicoper;
    [SerializeField]
    private GameObject bom;
    [SerializeField]
    private GameObject smoke;
    [SerializeField]
    private GameObject canhquatduoi;
    [SerializeField]
    private GameObject cam;
    [SerializeField]
    private GameObject _telemetry;
    private Telemetry telemetry;
    [SerializeField]
    private AudioSource audio;
    [SerializeField]
    private AudioSource audio_bom;
    public float explosionDuration = 3f;
    private bool is_flying = false;
    private bool is_arm = false;
    private bool is_W = false;
    private bool is_boom = false;
    public float countdownTime = 5f;
    private void RestartScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);

    }
    private void ExplodeIfStillColliding()
    {
        if (telemetry.is_Collide_Tuong)
        {
            audio_bom.Play();
            bom.SetActive(true);
            is_arm = false;
            audio.Stop();
            telemetry.is_Collide_Tuong = false;
            is_boom = true;
            Rigidbody rigidbody = helicoper.AddComponent<Rigidbody>();
            rigidbody.mass = 1f;
            rigidbody.useGravity = true;
            rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            Destroy(helicoper, 3f);
            Destroy(bom, 3f);
            Destroy(smoke, 3f);
            Invoke("RestartScene", explosionDuration);

        }
    }

    void Start()
    {
        telemetry = _telemetry.GetComponent<Telemetry>();
        smoke.SetActive(false);
        bom.SetActive(false);
        audio_bom.Stop();
    }

    void Update()
    {
        cam.transform.position = new Vector3(transform.position.x + 1.027f, transform.position.y + 20.1f, transform.position.z - 40.1f);
        smoke.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        bom.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        if (is_arm && is_boom == false)
        {
            canhquat.transform.RotateAround(canhquat.transform.position, Vector3.up, 2000 * Time.deltaTime);
            canhquatduoi.transform.RotateAround(canhquatduoi.transform.position, Vector3.forward, 2000 * Time.deltaTime);
            if (Input.GetKey(KeyCode.Q) && telemetry.is_Collide_Tuong == false)
            {
                transform.Translate(Vector3.forward * 10 * Time.deltaTime);
                is_flying = true;
                telemetry.is_Landing = false;
            }

            if (Input.GetKey(KeyCode.E) && is_flying && telemetry.is_Collide_Tuong == false)
            {
                transform.Translate(-Vector3.forward * 10 * Time.deltaTime);
                if (telemetry.is_Landing)
                {
                    is_flying = false;
                }

            }

            if (Input.GetKey(KeyCode.W) && is_flying && telemetry.is_Collide_Tuong == false)
            {
                transform.Translate(-Vector3.up * 10 * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.S) && is_flying)
            {
                transform.Translate(Vector3.up * 10 * Time.deltaTime);
                if (telemetry.is_Collide_Tuong)
                {
                    telemetry.is_Collide_Tuong = false;
                    CancelInvoke("ExplodeIfStillColliding");
                }
            }

            if (Input.GetKey(KeyCode.A) && is_flying && telemetry.is_Collide_Tuong == false)
            {
                transform.RotateAround(transform.position, -Vector3.up, 100 * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D) && is_flying && telemetry.is_Collide_Tuong == false)
            {
                transform.RotateAround(transform.position, Vector3.up, 100 * Time.deltaTime);
            }
            if (telemetry.is_Collide_Tuong)
            {
                Invoke("ExplodeIfStillColliding", countdownTime);
                smoke.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!is_arm && is_boom == false)
            {
                is_arm = true;
                audio.Play();
            }
            else
            {
                if (!is_flying)
                {
                    smoke.SetActive(false);
                    is_arm = false;
                    audio.Stop();

                }
            }
        }
    }
}
