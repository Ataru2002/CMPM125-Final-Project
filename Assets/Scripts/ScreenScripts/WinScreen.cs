using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToMainScreen()
    {
        SceneManager.LoadScene("MainScreen");
    }

    // Bit of a hack, but works since we know there are only 2 levels.
    public void GoToNextLevel()
    {
        SceneManager.LoadScene("Level2");
    }
}
