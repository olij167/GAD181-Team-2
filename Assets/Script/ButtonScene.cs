using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScene : MonoBehaviour
{
    public string MyScene;

    public void OnClick()
    {
        SceneManager.LoadScene(MyScene);
    }

}
