using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public GameObject go1stGO1;
    public GameObject go1stGO2;
    public GameObject go1stGO3;
    public GameObject go2ndGO1;
    public GameObject go2ndGO2;
    public GameObject go2ndGO3;
    public GameObject go3rdGO1;
    public GameObject go3rdGO2;
    public GameObject go3rdGO3;
    public GameObject go4thGO1;
    public GameObject go4thGO2;
    public GameObject go4thGO3;

    public bool bGo1stEditModeGOs;
    public bool bGo2ndThirdPersonGOs;
    public bool bGo3rdFirstPersonModeGOs;
    public bool bGo4thDemoModeGOs;

    public Vector3 delta = Vector3.zero;
    private Vector3 lastPos = Vector3.zero;

    public string magnitude = "";


    // Start is called before the first frame update
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
            lastPos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            delta = Input.mousePosition - lastPos;

            // Do Stuff here

            Debug.Log("delta X : " + delta.x);
            Debug.Log("delta Y : " + delta.y);

            Debug.Log("delta distance : " + delta.magnitude);

            magnitude = "delta distance : " + delta.magnitude;

            // End do stuff

            lastPos = Input.mousePosition;
        }


        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (bGo1stEditModeGOs == true)
            {
                bGo1stEditModeGOs = false;
            }
            else
            {
                bGo1stEditModeGOs = true;

                bGo2ndThirdPersonGOs = false;
                bGo3rdFirstPersonModeGOs = false;
                bGo4thDemoModeGOs = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (bGo2ndThirdPersonGOs == true)
            {
                bGo2ndThirdPersonGOs = false;
            }
            else
            { 
                bGo2ndThirdPersonGOs = true;

                bGo1stEditModeGOs = false;
                bGo3rdFirstPersonModeGOs = false;
                bGo4thDemoModeGOs = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (bGo3rdFirstPersonModeGOs == true)
            {
                bGo3rdFirstPersonModeGOs = false;
            }
            else
            {
                bGo3rdFirstPersonModeGOs = true;

                bGo1stEditModeGOs = false;
                bGo2ndThirdPersonGOs = false;
                bGo4thDemoModeGOs = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (bGo4thDemoModeGOs == true)
            {
                bGo4thDemoModeGOs = false;
            }
            else
            {
                bGo4thDemoModeGOs = true;

                bGo1stEditModeGOs = false;
                bGo2ndThirdPersonGOs = false;
                bGo3rdFirstPersonModeGOs = false;
            }
        }

        if (bGo1stEditModeGOs == true)
        {
            if(go1stGO1 != null)
                go1stGO1.SetActive(bGo1stEditModeGOs);

            if (go1stGO2 != null)
                go1stGO2.SetActive(bGo1stEditModeGOs);

            if (go1stGO3 != null)
                go1stGO3.SetActive(bGo1stEditModeGOs);
        }
        else
        {
            if (go1stGO1 != null)
                go1stGO1.SetActive(false);

            if (go1stGO2 != null)
                go1stGO2.SetActive(false);

            if (go1stGO3 != null)
                go1stGO3.SetActive(false);
        }

        if (bGo2ndThirdPersonGOs == true)
        {
            if (go2ndGO1 != null)
                go2ndGO1.SetActive(bGo2ndThirdPersonGOs);

            if (go2ndGO2 != null)
                go2ndGO2.SetActive(bGo2ndThirdPersonGOs);

            if (go2ndGO3 != null)
                go2ndGO3.SetActive(bGo2ndThirdPersonGOs);
        }
        else
        {
            if (go2ndGO1 != null)
                go2ndGO1.SetActive(false);

            if (go2ndGO2 != null)
                go2ndGO2.SetActive(false);

            if (go2ndGO3 != null)
                go2ndGO3.SetActive(false);
        }

        if (bGo3rdFirstPersonModeGOs == true)
        {
            if (go3rdGO1 != null)
                go3rdGO1.SetActive(bGo3rdFirstPersonModeGOs);

            if (go3rdGO2 != null)
                go3rdGO2.SetActive(bGo3rdFirstPersonModeGOs);

            if (go3rdGO3 != null)
                go3rdGO3.SetActive(bGo3rdFirstPersonModeGOs);
        }
        else
        {
            if (go3rdGO1 != null)
                go3rdGO1.SetActive(false);

            if (go3rdGO2 != null)
                go3rdGO2.SetActive(false);

            if (go3rdGO3 != null)
                go3rdGO3.SetActive(false);
        }

        if (bGo4thDemoModeGOs == true)
        {
            if (go4thGO1 != null)
                go4thGO1.SetActive(bGo4thDemoModeGOs);

            if (go4thGO2 != null)
                go4thGO2.SetActive(bGo4thDemoModeGOs);

            if (go4thGO3 != null)
                go4thGO3.SetActive(bGo4thDemoModeGOs);
        }
        else
        {
            if (go4thGO1 != null)
                go4thGO1.SetActive(false);

            if (go4thGO2 != null)
                go4thGO2.SetActive(false);

            if (go4thGO3 != null)
                go4thGO3.SetActive(false);
        }
    }
}
