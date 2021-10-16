using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(LineRenderer))]
public class Marker : MonoBehaviour
{
    //This class controls the marker which allows you to choose where to spawn objects
    [HideInInspector]
    public Material lineMaterial;
    
    LineRenderer line;
    GameObject cursor;

    void Start()
    {
        //Get a reference to the LineRenderer which draws the line under the marker
        line = GetComponent<LineRenderer>();

        //Set the properties of the Line Renderer here
        line.positionCount = 2;
        line.startWidth = 0.01f;
        line.endWidth = 0.02f;
        line.material = lineMaterial;

        //Spawn a sphere to mark the point below the marker
        cursor = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        cursor.transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
        cursor.transform.SetParent(transform);
        cursor.GetComponent<MeshRenderer>().material = lineMaterial;
        Destroy(cursor.GetComponent<Collider>());

        //Make sure that the cursor follows the Cinemachine update loop
        CinemachineCore.CameraUpdatedEvent.AddListener(UpdateObjectPosition);
    }

    /// <summary>
    /// Recalculate the start and final position for the marker based on the mouse position and a raycast down
    /// </summary>
    void UpdateObjectPosition(CinemachineBrain brain)
    {
        Vector3 endPos = new Vector3(transform.position.x, 0, transform.position.z);

        //Raycast down from the marker to position the line and sphere underneath the marker
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, transform.position.y))
            endPos = hit.point;

        //Set the positions of the line to start at the marker and end below it
        line.SetPosition(0, transform.position);
        line.SetPosition(1, endPos);

        cursor.transform.position = endPos;
    }
}
