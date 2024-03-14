using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainScreen : MonoBehaviour
{
    public GameObject panel;

    void Start(){
        
        if(panel != null){
            panel.SetActive(false);
        }
    }
    public void levelOne(){
        SceneManager.LoadScene("level1");
    }
    public void levelTwo()
    {
        SceneManager.LoadScene("level2");
    }
    public void credits()
    {
        SceneManager.LoadScene("creditScreen");
    }
    public void instructions()
    {
        SceneManager.LoadScene("instructionScreen");
    }
    public void showPanel(){
        panel.SetActive(true);
    }

    public void hidePanel(){
        panel.SetActive(false);
    }
}
