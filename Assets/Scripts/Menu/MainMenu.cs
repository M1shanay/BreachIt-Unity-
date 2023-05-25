using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Animator CameraAnimation;
    public GameObject BackFromLvl1;
    public GameObject BackFromLvl2;
    public GameObject BackFromLvl3;
    public GameObject GoInLvl1;
    public GameObject GoInLvl2;
    public GameObject GoInLvl3;
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
    public void OnLevel1()
    {

        gameObject.transform.GetChild(5).GetComponent<LevelButton>().Deactivate();
        BackFromLvl1.SetActive(true);
        GoInLvl1.SetActive(true);
        CameraAnimation.SetBool("Level1", true);
    }
    public void BackLevel1()
    {
        //gameObject.transform.GetChild(5).GetComponent<LevelButton>().Activate();
        StartCoroutine(gameObject.transform.GetChild(5).GetComponent<LevelButton>().ActiveButton());
        BackFromLvl1.SetActive(false);
        BackFromLvl1.GetComponent<Button>().SetDefaultColor();
        CameraAnimation.SetBool("Level1", false);
    }
    public void GoIn1()
    {
        SceneManager.LoadScene("Level1");
    }
    public void GoIn2()
    {
        SceneManager.LoadScene("Level2");
    }
    public void GoIn3()
    {
        SceneManager.LoadScene("Level3");
    }
    public void OnLevel2()
    {

        gameObject.transform.GetChild(6).GetComponent<LevelButton>().Deactivate();
        BackFromLvl2.SetActive(true);
        GoInLvl2.SetActive(true);
        CameraAnimation.SetBool("Level2", true);
    }
    public void BackLevel2()
    {
        //gameObject.transform.GetChild(5).GetComponent<LevelButton>().Activate();
        StartCoroutine(gameObject.transform.GetChild(6).GetComponent<LevelButton>().ActiveButton());
        BackFromLvl2.SetActive(false);
        BackFromLvl2.GetComponent<Button>().SetDefaultColor();
        CameraAnimation.SetBool("Level2", false);
    }
    public void OnLevel3()
    {

        gameObject.transform.GetChild(7).GetComponent<LevelButton>().Deactivate();
        BackFromLvl3.SetActive(true);
        GoInLvl3.SetActive(true);
        CameraAnimation.SetBool("Level3", true);
    }
    public void BackLevel3()
    {
        //gameObject.transform.GetChild(5).GetComponent<LevelButton>().Activate();
        StartCoroutine(gameObject.transform.GetChild(7).GetComponent<LevelButton>().ActiveButton());
        BackFromLvl3.SetActive(false);
        BackFromLvl3.GetComponent<Button>().SetDefaultColor();
        CameraAnimation.SetBool("Level3", false);
    }
}
