using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Animator CameraAnimation;
    public void StartButton()
    {
        CameraAnimation.SetBool("Right", true);
        CameraAnimation.SetBool("Start", true);
        Debug.Log("Start");
    }
    public void ExitButton()
    {
        Debug.Log("Exit");
    }
    public void TutorButton()
    {
        CameraAnimation.SetBool("Left", true);
        CameraAnimation.SetBool("Tutorial", true);
        Debug.Log("Tutorial");
    }
    public void BackFromStart()
    {
        CameraAnimation.SetBool("Right", false);
        CameraAnimation.SetBool("Start", false);
    }
    public void BackFromTutor()
    {
        CameraAnimation.SetBool("Left", false);
        CameraAnimation.SetBool("Tutorial", false);
    }
}
