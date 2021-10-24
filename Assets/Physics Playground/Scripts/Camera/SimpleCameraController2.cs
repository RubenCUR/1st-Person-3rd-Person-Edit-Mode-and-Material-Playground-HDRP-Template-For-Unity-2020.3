#if ENABLE_INPUT_SYSTEM && ENABLE_INPUT_SYSTEM_PACKAGE
#define USE_INPUT_SYSTEM
    using UnityEngine.InputSystem;
    using UnityEngine.InputSystem.Controls;
#endif

using UnityEngine;
using Cinemachine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//C#
//using System.Runtime.InteropServices;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem;


namespace UnityTemplateProjects
{
    public class SimpleCameraController2 : MonoBehaviour
    {
        class CameraState
        {
            public float yaw;
            public float pitch;
            public float roll;
            public float x;
            public float y;
            public float z;

            public void SetFromTransform(Transform t)
            {
                pitch = t.eulerAngles.x;
                yaw = t.eulerAngles.y;
                roll = t.eulerAngles.z;
                x = t.position.x;
                y = t.position.y;
                z = t.position.z;
            }

            public void Translate(Vector3 translation, Collider bounds = null)
            {
                Vector3 rotatedTranslation = Quaternion.Euler(pitch, yaw, roll) * translation;

                if(bounds != null){
                    Vector3 p = new Vector3(x, y, z) + rotatedTranslation;
                    if(!bounds.bounds.Contains(p))
                    {
                        rotatedTranslation = Vector3.zero;
                    }
                }

                x += rotatedTranslation.x;
                y += rotatedTranslation.y;
                z += rotatedTranslation.z;

                
            }

            public void LerpTowards(CameraState target, float positionLerpPct, float rotationLerpPct)
            {
                yaw = Mathf.Lerp(yaw, target.yaw, rotationLerpPct);
                pitch = Mathf.Lerp(pitch, target.pitch, rotationLerpPct);
                roll = Mathf.Lerp(roll, target.roll, rotationLerpPct);
                
                x = Mathf.Lerp(x, target.x, positionLerpPct);
                y = Mathf.Lerp(y, target.y, positionLerpPct);
                z = Mathf.Lerp(z, target.z, positionLerpPct);
            }

            public void UpdateTransform(Transform t)
            {
                t.eulerAngles = new Vector3(pitch, yaw, roll);
                t.position = new Vector3(x, y, z);
            }
        }

        public Text m_Text1;
        public Text m_Text2;

        RaycastHit hit = new RaycastHit();

        public GameObject go1;
        public GameObject go2;

        public GameObject[] go;

     

        //[DllImport("user32.dll")]
        //static extern bool SetCursorPos(int X, int Y);

        float xPos = 0, yPos = 0;
        float xPosPrev = 0, yPosPrev = 0;

        CameraState m_TargetCameraState = new CameraState();
        CameraState m_InterpolatingCameraState = new CameraState();
        CinemachineConfiner confiner;
        Collider bounds;

        [Header("Movement Settings")]
        [Tooltip("Exponential boost factor on translation, controllable by mouse wheel.")]
        public bool rightClickToLook = false;

        [Tooltip("Exponential boost factor on translation, controllable by mouse wheel.")]
        public float boost = 3.5f;

        [Tooltip("Time it takes to interpolate camera position 99% of the way to the target."), Range(0.001f, 1f)]
        public float positionLerpTime = 0.2f;

        [Header("Rotation Settings")]
        [Tooltip("X = Change in mouse position.\nY = Multiplicative factor for camera rotation.")]
        public AnimationCurve mouseSensitivityCurve = new AnimationCurve(new Keyframe(0f, 0.5f, 0f, 5f), new Keyframe(1f, 2.5f, 0f, 0f));

        [Tooltip("Time it takes to interpolate camera rotation 99% of the way to the target."), Range(0.001f, 1f)]
        public float rotationLerpTime = 0.01f;

        [Tooltip("Whether or not to invert our Y axis for mouse input to rotation.")]
        public bool invertY = false;



        void Start()
        {
            confiner = GetComponent<CinemachineConfiner>();
            if(confiner != null)
                bounds = confiner.m_BoundingVolume;
        }

        void OnEnable()
        {
            m_TargetCameraState.SetFromTransform(transform);
            m_InterpolatingCameraState.SetFromTransform(transform);
        }

        Vector3 GetInputTranslationDirection()
        {
            Vector3 direction = new Vector3();
            if (Input.GetKey(KeyCode.W))
            {
                direction += Vector3.forward;
            }
            if (Input.GetKey(KeyCode.S))
            {
                direction += Vector3.back;
            }
            if (Input.GetKey(KeyCode.A))
            {
                direction += Vector3.left;
            }
            if (Input.GetKey(KeyCode.D))
            {
                direction += Vector3.right;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                direction += Vector3.down;
            }
            if (Input.GetKey(KeyCode.E))
            {
                direction += Vector3.up;
            }
            return direction;
        }
        
        void Update()
        {


            //if (nm != null)
            //{
            //    if (nm.IsServer || nm.IsHost)
            //    {
            //        Debug.Log("Server or Host");

            //        if (nm.ConnectedClients.Count > 0)
            //            if (nm.ConnectedClients[2] != null)
            //            {
            //                NetworkClient nc = nm.ConnectedClients[2];

            //                go2 = nc.PlayerObject.gameObject;

            //                if (go2 != null)
            //                    sc2 = go2.GetComponent<ShipControl>();
            //            }

            //        //if(nm.ConnectedClients.Count > 1)
            //        //    if (nm.ConnectedClients[2] != null)
            //        //    {
            //        //        NetworkClient nc = nm.ConnectedClients[2];

            //        //        go1 = nc.PlayerObject.gameObject;

            //        //        if (go1 != null)
            //        //            sc1 = go1.GetComponent<ShipControl>();
            //        //    }
            //    }
            //}

            //if(go1 == null) 
            //{
            //    go1 = GameObject.FindGameObjectWithTag("RemoteTouch");

            //    if(go1 != null)
            //        sc1 = go1.GetComponent<ShipControl>();
            //}

            //if (go2 == null)
            //{
            //    go = GameObject.FindGameObjectsWithTag("RemoteTouch");

            //    if (go1 != null)
            //    { 
            //        for (int i = 0; i < go.Length; i++)
            //        {
            //            if (go1 != go[i])
            //            {
            //                go2 = go[i];

            //                sc2 = go2.GetComponent<ShipControl>();
            //            }                
            //        }
            //    }
            //}

            //if (sc1 != null)
            //{
            //    if (m_Text1 != null)
            //        m_Text1.text = sc1.PlayerName.Value;

            //}

            //if (sc2 != null)
            //{
            //    if (m_Text2 != null)
            //        m_Text2.text = sc2.PlayerName.Value;



            //    if (sc2.PlayerName.Value.IndexOf(",") > 0)
            //    {
            //        xPosPrev = xPos;
            //        yPosPrev = yPos;






            //        if(sc2.PlayerName.Value.Contains("("))
            //        {
            //            xPos = float.Parse(sc2.PlayerName.Value.Substring(1, sc2.PlayerName.Value.IndexOf(",")).ToString());
            //            yPos = float.Parse(sc2.PlayerName.Value.Substring(sc2.PlayerName.Value.IndexOf(",") + 2));
            //        }
            //        else
            //        {
            //            xPos = float.Parse(sc2.PlayerName.Value.Substring(0, sc2.PlayerName.Value.IndexOf(",")).ToString());
            //            yPos = float.Parse(sc2.PlayerName.Value.Substring(sc2.PlayerName.Value.IndexOf(",") + 2));
            //        }

            //        //SetCursorPos(int.Parse(sc2.PlayerName.Value.Substring(0, sc2.PlayerName.Value.IndexOf(",") - 1)), 100);//Call this when you want to set the mouse position    

            //        //SetCursorPos(xPos, yPos);//Call this when you want to set the mouse position    

            //    }



            //}

            //if(xPos != xPosPrev)
            //SetCursorPos((int)xPos, (int)yPos);//Call this when you want to set the mouse position    

            //Vector2 pos = new Vector2((int)xPos, (int)yPos);

            ////        Vector3 mousepos = Camera.main.WorldToScreenPoint(pos); // + MoveTarget
            ////        // 'feature' workaround: https://forum.unity.com/threads/inputsystem-reporting-wrong-mouse-position-after-warpcursorposition.929019/
            ////        //InputSystem.QueueDeltaStateEvent(Mouse.current.position,  (Vector2)mousepos);   // required 8 bytes, not 12!
            ////        InputState.Change(Mouse.current.position, (Vector2)mousepos);
            ////#if !UNITY_EDITOR
            ////            // bug workaround : https://forum.unity.com/threads/mouse-y-position-inverted-in-build-using-mouse-current-warpcursorposition.682627/#post-5387577
            ////            mousepos.Set(mousepos.x, Screen.height - mousepos.y, mousepos.z);
            ////#endif
            //Mouse.current.WarpCursorPosition(pos);





            Vector3 translation = Vector3.zero;

            // Exit Sample  
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }

            if (Input.GetMouseButton(1) || !rightClickToLook || (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(0)))
            {
                var mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") * (invertY ? 1 : -1));

                var mouseSensitivityFactor = mouseSensitivityCurve.Evaluate(mouseMovement.magnitude);

                m_TargetCameraState.yaw += mouseMovement.x * mouseSensitivityFactor;
                m_TargetCameraState.pitch += mouseMovement.y * mouseSensitivityFactor;
            }

            // Translation
            translation = GetInputTranslationDirection() * Time.unscaledDeltaTime;

            // Modify movement by a boost factor (defined in Inspector)
            translation *= Mathf.Pow(2.0f, boost);

            m_TargetCameraState.Translate(translation, bounds);

            // Framerate-independent interpolation
            // Calculate the lerp amount, such that we get 99% of the way to our target in the specified time
            var positionLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / positionLerpTime) * Time.unscaledDeltaTime);
            var rotationLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / rotationLerpTime) * Time.unscaledDeltaTime);
            m_InterpolatingCameraState.LerpTowards(m_TargetCameraState, positionLerpPct, rotationLerpPct);

            m_InterpolatingCameraState.UpdateTransform(transform);
        }
    }

}