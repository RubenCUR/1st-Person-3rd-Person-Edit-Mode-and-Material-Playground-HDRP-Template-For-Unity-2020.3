using MLAPI;
using MLAPI.Connection;
using MLAPI.NetworkVariable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//C#
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem;

public class SetTouchToText : MonoBehaviour
{
    public bool IsUsingMouse = true;

    public Text m_Text1;
    public Text m_Text2;

    public GameObject go1;
    public GameObject go2;

    public GameObject[] go;

    public ShipControl sc1;
    public ShipControl sc2;

    public NetworkManager nm;

    [DllImport("user32.dll")]
    static extern bool SetCursorPos(int X, int Y);

    float xPos = 0, yPos = 0;
    float xPosPrev = 0, yPosPrev = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    RaycastHit hit = new RaycastHit();

    // Update is called once per frame
    void Update()
    {
        //if (Physics.Raycast(transform.position, transform.forward, out hit))
        //{
        //    if (hit.collider.gameObject.tag == "Player")
        //    {
        //        Debug.DrawRay(transform.position, transform.forward, Color.green);
        //    }
        //    else
        //    {
        //        Debug.DrawRay(transform.position, transform.forward, Color.red);
        //    }
        //}




        if (nm != null)
        {
            //if (nm.IsServer || nm.IsHost)
            //{
            //    Debug.Log("Server or Host");

            //    if (nm.ConnectedClients.Count > 0)
            //        if (nm.ConnectedClients[2] != null)
            //        {
            //            NetworkClient nc = nm.ConnectedClients[2];

            //            go2 = nc.PlayerObject.gameObject;

            //            if (go2 != null)
            //                sc2 = go2.GetComponent<ShipControl>();
            //        }

            //    //if(nm.ConnectedClients.Count > 1)
            //    //    if (nm.ConnectedClients[2] != null)
            //    //    {
            //    //        NetworkClient nc = nm.ConnectedClients[2];

            //    //        go1 = nc.PlayerObject.gameObject;

            //    //        if (go1 != null)
            //    //            sc1 = go1.GetComponent<ShipControl>();
            //    //    }
            //}
        }

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



                



        //        xPos = float.Parse(sc2.PlayerName.Value.Substring(0, sc2.PlayerName.Value.IndexOf(",")).ToString());
        //        yPos = float.Parse(sc2.PlayerName.Value.Substring(sc2.PlayerName.Value.IndexOf(",") + 2));
        //        //SetCursorPos(int.Parse(sc2.PlayerName.Value.Substring(0, sc2.PlayerName.Value.IndexOf(",") - 1)), 100);//Call this when you want to set the mouse position    

        //        //SetCursorPos(xPos, yPos);//Call this when you want to set the mouse position    

        //    }



        //}

        //if(IsUsingMouse == false)
        //{ 
        //    if (xPos != xPosPrev)
        //        SetCursorPos((int)xPos, (int)yPos);//Call this when you want to set the mouse position    
        //}
        //else
        //{ 
            //For RemoteMouse
            //Vector2 pos = new Vector2((int)xPos, (int)yPos);
            //----------------------------------------------

    //        //For Local Mouse
    //        Vector2 pos = new Vector2((int)0, (int)0);
        
    //        Vector3 mousepos = Camera.main.WorldToScreenPoint(pos); // + MoveTarget
    //        //                                                        // 'feature' workaround: https://forum.unity.com/threads/inputsystem-reporting-wrong-mouse-position-after-warpcursorposition.929019/
    //        InputSystem.QueueDeltaStateEvent(Mouse.current.position,  (Vector2)mousepos);   // required 8 bytes, not 12!
    //        InputState.Change(Mouse.current.position, (Vector2)mousepos);
    //#if !UNITY_EDITOR
    //                            // bug workaround : https://forum.unity.com/threads/mouse-y-position-inverted-in-build-using-mouse-current-warpcursorposition.682627/#post-5387577
    //                            mousepos.Set(mousepos.x, Screen.height - mousepos.y, mousepos.z);
    //#endif
   //     }
        //----------------------------------------------

        //For RemoteMouse
        //Mouse.current.WarpCursorPosition(pos);
        //----------------------------------------------

    }
}