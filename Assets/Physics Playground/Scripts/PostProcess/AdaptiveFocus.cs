using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using System.Collections.Generic;

//This script controls the focus of the camera and adapts the depth of field based on where the mouse is pointing
// For a tutorial on how this was created, check out this video from Game Dev Guide: https://www.youtube.com/watch?v=7od2j4s85ww

public class AdaptiveFocus : MonoBehaviour
{
    public List<Volume> volumes;
    public float defaultDepth = 5.5f;

    List<DepthOfField> dof;

    void Start()
    {
        dof = new List<DepthOfField>();
        if(volumes != null && volumes.Count > 0)
        {
            foreach (var vol in volumes)
            {
                DepthOfField depthOfField;
                vol.profile.TryGet<DepthOfField>(out depthOfField);
                if (depthOfField != null)
                    dof.Add(depthOfField);

            }
        }
        CinemachineCore.CameraUpdatedEvent.AddListener(UpdateDepth);
    }

    void UpdateDepth(CinemachineBrain brain)
    {      
        RaycastHit hit;
        Vector3 fw = transform.TransformDirection(Vector3.forward);
        float depth = defaultDepth;
        if (Physics.Raycast(transform.position, fw, out hit, defaultDepth))
        {
            depth = (hit.point - transform.position).magnitude;
        }

        foreach (var d in dof)
            d.focusDistance.value = Mathf.Lerp(d.focusDistance.value,depth,.2f);

    }
}
