using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class NavigateVideo : MonoBehaviour
{
    public VideoPlayer vp;
    public VideoPlayer vp1;
    
    public int length = 10;

    public float playbackSpeed = 1;

    float outPlaybackspeed = 0;

    public bool IsPLaying = false;

    public float mousePosX;

    public float screenHalfWidth;

    public int screenHalfXDivider = 2;

    public TMPro.TMP_InputField tmp1;
    public TMPro.TMP_InputField tmp2;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePosX = Input.mousePosition.x;

        screenHalfWidth = Screen.width / screenHalfXDivider;

        if (Input.mousePosition.x > (Screen.width / 2))
        {
            if (Input.GetMouseButton(0) == true)
            {
                for (int i = 0; i < length; i++)
                {
                    vp1.StepForward();
                    vp1.Play();

                    tmp1.gameObject.SetActive(false);
                }

            }

            if (Input.GetMouseButton(1) == true)
            {
                //for (int i = 0; i < length; i++)
                {
                    if (vp1.isPaused || vp1.isPlaying == false)
                    {
                        playbackSpeed = 1;

                        vp1.playbackSpeed = playbackSpeed;

                        vp1.Play();
                        IsPLaying = true;
                        tmp1.gameObject.SetActive(false);
                    }
                    else
                    {
                        vp1.Pause();
                        IsPLaying = false;
                        tmp1.gameObject.SetActive(true);
                    }
                }

            }



            if (Input.GetMouseButton(2) == true)
            {
                //for (int i = 0; i < length; i++)
                {
                    playbackSpeed = playbackSpeed + 0.1f;

                    vp1.playbackSpeed = playbackSpeed;
                    vp1.Play();

                    tmp1.gameObject.SetActive(false);

                    if (vp1.playbackSpeed > 3)
                        vp1.playbackSpeed = 0.1f;
                }

            }
        }
        else
        {
            if (Input.GetMouseButton(0) == true)
            {
                for (int i = 0; i < length; i++)
                {
                    vp.StepForward();
                    vp.Play();

                    tmp2.gameObject.SetActive(false);
                }

            }

            if (Input.GetMouseButton(1) == true)
            {
                //for (int i = 0; i < length; i++)
                {


                    if (vp.isPlaying == true)
                    {

                        vp.Pause();

                        tmp2.gameObject.SetActive(true);
                    }
                    else
                    {
                        playbackSpeed = 1;
                        vp.playbackSpeed = playbackSpeed;
                        vp.Play();

                        tmp2.gameObject.SetActive(false);
                    }
                }

            }



            if (Input.GetMouseButton(2) == true)
            {
                //for (int i = 0; i < length; i++)
                {
                    //if(Time.deltaTime > 0.5)
                    { 

                        playbackSpeed = playbackSpeed + 0.05f;

                        vp.playbackSpeed = playbackSpeed;
                        vp.Play();

                        tmp2.gameObject.SetActive(false);

                        if (playbackSpeed > 3)
                            playbackSpeed = 0.1f;

                    }
                }

            }
        }

    }

    

}
