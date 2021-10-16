using Cinemachine;
using UnityEngine;

public enum CameraAxis{ Horizontal, Vertical}
[RequireComponent(typeof(CinemachineFreeLook))]
public class OrbitCameraController : MonoBehaviour
{
    [Tooltip("The max speed that the camera uses to rotate on the horizontal axis (use the Soft Zones of the Cinemachine Middle Rig to define the trigger area).")]
    public float horizontalSpeed;
    [Tooltip("The max speed that the camera uses to rotate on the vertical axis (use the Soft Zones of the Cinemachine Middle Rig to define the trigger area).")]
    public float verticalSpeed;
    
    CinemachineFreeLook orbit;
    CinemachineComposer defaultComposer;
    float horizontalDelta = -1;
    float verticalDelta = -1;

    void Start()
    {
        orbit = GetComponent<CinemachineFreeLook>();
        var vcam = orbit.GetRig(1);
        if(vcam != null)
        {
            defaultComposer = orbit.GetRig(1).GetCinemachineComponent<CinemachineComposer>();
            if(defaultComposer != null)
            {
                horizontalDelta = CalculateDelta(defaultComposer.m_SoftZoneWidth, CameraAxis.Horizontal);
                verticalDelta = CalculateDelta(defaultComposer.m_SoftZoneHeight, CameraAxis.Vertical);
            }
        }
    }

    void Update()
    {
        if (orbit != null && horizontalDelta != -1 && verticalDelta != -1)
        {
            var coords = Input.mousePosition;
            if (coords.x < 0 || coords.y < 0 || coords.x > Screen.width || coords.y > Screen.height) 
                return;

            if (coords.x > Screen.width - horizontalDelta)
                orbit.m_XAxis.Value  += Time.deltaTime * CalculateSpeed(coords.x, CameraAxis.Horizontal, 1);
            else if(coords.x < horizontalDelta)
                orbit.m_XAxis.Value  += Time.deltaTime * CalculateSpeed(coords.x, CameraAxis.Horizontal, -1);

            if (coords.y >= Screen.height - verticalDelta)
                orbit.m_YAxis.Value  -= Time.deltaTime * CalculateSpeed(coords.y, CameraAxis.Vertical, 1);
            else if(coords.y <= verticalDelta)
                orbit.m_YAxis.Value  -= Time.deltaTime * CalculateSpeed(coords.y, CameraAxis.Vertical, - 1);
            
        }
    }

    /// <summary>
    /// Calculate the camera rotation speed based on the mouse position, the axis and the direction of movement.
    /// </summary>
    float CalculateSpeed(float inputVal, CameraAxis axis, float dir)
    {
        float delta = 0;
        float max = 0;
        float maxSpeed = 0;
        float speed = 0;

        if(axis == CameraAxis.Horizontal)
        {
            delta = horizontalDelta;
            max = dir > 0 ? Screen.width : 0;
            maxSpeed = horizontalSpeed; 
        }
        else
        {
            delta = verticalDelta;
            max = dir > 0 ? Screen.height : 0;
            maxSpeed = verticalSpeed; 
        }
        
        if(max > delta)
            speed = (inputVal - (max-delta)) / -delta * maxSpeed;
        else
            speed = (inputVal - delta)/ -delta * maxSpeed;

       return speed;
    }

    /// <summary>
    /// Calculate the trigger area for the mouse using the Soft Zones of the Cinemachine Middle Rig.
    /// </summary>
    float CalculateDelta(float softZoneWeight, CameraAxis axis)
    {
        float dim = 0;
        float delta = 0;

        if(axis == CameraAxis.Horizontal)
        {
            dim = softZoneWeight * Screen.width;
            delta = dim < Screen.width ? Screen.width - dim : Screen.width;
        }
        else
        {
            dim = softZoneWeight * Screen.height;
            delta = dim < Screen.height ? Screen.height - dim : Screen.height;
        }

        return delta/2;
    }

}
