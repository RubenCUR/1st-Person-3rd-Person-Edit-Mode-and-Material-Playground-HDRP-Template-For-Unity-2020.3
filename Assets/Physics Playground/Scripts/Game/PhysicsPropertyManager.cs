using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PhysicsPropertyManager : MonoBehaviour
{
    [Tooltip("The keyboard button to toggle the time scale.")]
    public KeyCode timeScaleKey = KeyCode.Z;
    [Tooltip("The altered time scale.")]
    public float timeScale;
    [Tooltip("The UI text to show the time key button.")]
    public TextMeshProUGUI timeKeyText;
    [Tooltip("The UI image for the time mode.")]
    public Image timeImage;
    [Space]
    [Tooltip("The keyboard button to toggle the gravity.")]
    public KeyCode gravityKey = KeyCode.X;
    [Tooltip("The alterated gravity vector.")]
    public Vector3 fakeGravity;
    [Tooltip("The UI text to show the gravity key button.")]
    public TextMeshProUGUI gravityKeyText;
     [Tooltip("The UI image for the gravity changed mode.")]
    public Image gravityOnImage;
     [Tooltip("The UI image for the gravity default mode.")]
    public Image gravityOffImage;
    [Tooltip("Post Process manager, null if you don't want to switch effects between kinematic and non-kinematic mode")]
    public PostProcessManager postProcessManager;
    [Tooltip("The Audio Controller used to modify the audio based on the events")]
    public AudioController audioController;
   
    bool timeToggle;
    bool gravityToggle;
    
    float defaultTimeScale;
    float defaultFixedDeltaTime;
    Vector3 defaultGravity;

    void Start()
    {
        //Initialize time and gravity to initial values
        defaultTimeScale = Time.timeScale;
        defaultFixedDeltaTime = Time.fixedDeltaTime;
        defaultGravity = Physics.gravity;

        //Update the key labels in the UI
        timeKeyText.text = timeScaleKey.ToString().ToUpper();
        gravityKeyText.text = gravityKey.ToString().ToUpper();

        //Update the UI icons
        setTimeImage(timeToggle);
        setGravityImages(gravityToggle);
    }

    void Update()
    {
        //Check to see if time change key has been released
        if (Input.GetKeyUp(timeScaleKey))
        {
            //Reverse the time toggle and change the timeScale
            timeToggle = !timeToggle;
            Time.timeScale = timeToggle ? timeScale : defaultTimeScale;
            Time.fixedDeltaTime = defaultFixedDeltaTime * Time.timeScale;

            //Update the UI
            setTimeImage(timeToggle);

            //Check if postProcessManager is set in Inspector
            if (postProcessManager != null)
            {
                //If time has been slowed, activate the postProcessVolume to change the post-effects
                if (timeToggle)
                    postProcessManager.ChangeVolume(Mode.timescale);
                else
                    postProcessManager.ChangeVolume(Mode.defaultTimeScale);
            }
            
            //Check if Audiocontroller is assigned in Inspector
            if (audioController != null)
            {
                //If time has been slowed lower the pitch on the audio, otherwise set it to full pitch/speed
                if (timeToggle)
                    audioController.HalfPitchAudio();
                else
                    audioController.FullSpeedAudio();
            }

        }

        //Check to see if the gravity key has been pressed and released
        if (Input.GetKeyUp(gravityKey))
        {
            //Flip the toggle and update the gravity value
            gravityToggle = !gravityToggle;
            Physics.gravity = gravityToggle ? fakeGravity : defaultGravity;
            setGravityImages(gravityToggle);

            //Check if postProcessManager is set in Inspector
            if (postProcessManager != null)
            {
                //If gravity has been changed, activate the postProcessVolume to change the post-effects

                if (gravityToggle)
                    postProcessManager.ChangeVolume(Mode.customGravity);
                else
                    postProcessManager.ChangeVolume(Mode.standardGravity);
            }

            if (audioController != null)
            {
                //If gravity has been reversed fade in the reversed audio, otherwise fade in the forward audio
                if (gravityToggle)
                    audioController.AudioReverse();
                else
                    audioController.AudioForward();
            }
        }
    }

    /// <summary>
    /// Toggle the gravity UI images based on the current gravity value
    /// </summary>
    void setGravityImages(bool v)
    {
        var onColor = gravityOnImage.color;
        onColor.a = v ? .3f : 1f;
        gravityOnImage.color = onColor;

        var offColor = gravityOffImage.color;
        offColor.a = v ? 1f : .3f;
        gravityOffImage.color = offColor;
    }

    /// <summary>
    /// Highlight the time UI image based on the current time value
    /// </summary>
    void setTimeImage(bool v)
    {
        var timeColor = timeImage.color;
        timeColor.a = v ? 1f : .3f;
        timeImage.color = timeColor;
    }
}
