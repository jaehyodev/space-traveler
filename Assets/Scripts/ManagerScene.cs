using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManagerScene : MonoBehaviour
{
    public void SceneLoadMain()
    {
        SceneManager.LoadScene("Main");    
    }

    public void SceneLoadPlay()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("Play"); 
    }
}
