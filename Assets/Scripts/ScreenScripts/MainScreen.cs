using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainScreen : MonoBehaviour
{
    public void gameplay()
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
}
