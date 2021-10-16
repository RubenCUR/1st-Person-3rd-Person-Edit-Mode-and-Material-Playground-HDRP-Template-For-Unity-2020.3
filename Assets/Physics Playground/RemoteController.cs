using UnityEngine;
using MLAPI;
using MLAPI.Extensions;
using MLAPI.NetworkVariable;
using UnityEngine.Assertions;
using MLAPI.Messaging;

public class RemoteController : NetworkBehaviour
{
    public NetworkVariableString PlayerNameRC = new NetworkVariableString("SomeGuy");


    void Update()
    {
        if (IsServer)
        {
            UpdateServer();
        }

        if (IsClient)
        {
            UpdateClient();
        }
    }

    void UpdateServer()
    {

    }

    void UpdateClient()
    {
        if (!IsLocalPlayer)
        {
            return;
        }

        // movement
        int spin = 0;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            spin += 1;

            SetNameServerRpc("Left + " + spin);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            spin -= 1;

            SetNameServerRpc("Right + " + spin);
        }

        int moveForce = 0;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveForce += 1;

            SetNameServerRpc("Up " + moveForce);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveForce -= 1;

            SetNameServerRpc("Down " + moveForce);
        }

        // fire
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetNameServerRpc("Space");
        }

        //// center camera.. only if this is MY player!
        //Vector3 pos = transform.position;
        //pos.z = -50;
        //Camera.main.transform.position = pos;
    }

    [ServerRpc]
    public void SetNameServerRpc(string name)
    {
        PlayerNameRC.Value = name;
    }
}
