using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class SpawnedObjectManager : MonoBehaviour
{
    [Tooltip("The keyboard button to activate the gravity.")]
    public KeyCode kinematicActivateKey = KeyCode.Space;
    [Tooltip("The UI text to show the gravity key button.")]
    public TextMeshProUGUI kinematicKeyText;
    [Tooltip("The UI image for the play mode.")]
    public Image playImage;
    [Tooltip("The UI image for the pause mode.")]
    public Image pauseImage;

    [Tooltip("Post Process manager, leave this empty (null) if you don't want to switch effects between kinematic and non-kinematic mode")]
    public PostProcessManager postProcessManager;

    [Tooltip("Audio Controller to allow changing the audio based on kinematic and non-kinematic (physics on) mode")]
    public AudioController audioController;

    List<GameObject> spawnedBodies = new List<GameObject>();

    bool isKinematic = true;

    void Start(){
        kinematicKeyText.text = kinematicActivateKey.ToString().ToUpper();
        setImages(isKinematic);
    }

    /// <summary>
    /// Add a GameObject to the spawned list.
    /// </summary>
    public void AddObject(GameObject o)
    {
        handleGravity(o, isKinematic);

        spawnedBodies.Add(o);
    }

    /// <summary>
    /// Handles the rigidbodies and the colliders of the root object
    /// </summary>
    void handleGravity(GameObject o, bool isKinematicBody)
    {
        var rigidbodies = o.GetComponentsInChildren<Rigidbody>();
        var colliders = o.GetComponentsInChildren<Collider>();

        foreach (var rb in rigidbodies)
            rb.isKinematic = isKinematicBody;

        foreach (var coll in colliders)
            coll.isTrigger = isKinematicBody;
    }

    /// <summary>
    /// Check if a GameObject was in the list and if found destroy it.
    /// </summary>
    public bool CheckAndDestroyObject(GameObject o)
    {
       //if(spawnedBodies.Remove(o))
        {
            o.transform.DOScale(0, .1f).SetEase(Ease.InBack).OnComplete(() => Destroy(o));
            return true;
        }
        return false;
    }

    /// <summary>
    /// Activate the gravity for all the spawned objects.
    /// </summary>
    void ActivateGravity(bool isKinematicBody)
    {
        spawnedBodies = spawnedBodies.Where(x => x != null).ToList();

        foreach(var b in spawnedBodies)
        {
            handleGravity(b, isKinematicBody);
        }
    }

    void Update()
    {
        if(Input.GetKeyUp(kinematicActivateKey)){
            isKinematic = !isKinematic;

            setImages(isKinematic);
            
            if (postProcessManager != null)
            {
                if (isKinematic)
                    postProcessManager.ChangeVolume(Mode.kinematic);
                else
                    postProcessManager.ChangeVolume(Mode.nonKinematic);
            }

            if (audioController != null)
            {
                if (isKinematic)
                    audioController.LowpassAudio();
                else
                    audioController.RemoveLowpassEffect();
            }

            ActivateGravity(isKinematic);
        }     
    }

    /// <summary>
    /// Toggle the play/pause UI images based on the current state.
    /// </summary>
    void setImages(bool v)
    {
        var playColor = playImage.color;
        playColor.a = v ?.3f : 1f;
        playImage.color = playColor;

        var pauseColor = pauseImage.color;
        pauseColor.a = v ? 1f : .3f;
        pauseImage.color = pauseColor;
    }

}
