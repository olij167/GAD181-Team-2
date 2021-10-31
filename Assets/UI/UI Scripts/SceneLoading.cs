using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoading : MonoBehaviour
{

    public Image _progressBar;
    // Start is called before the first frame update
    void Start()
    {
        //start assync operation
        StartCoroutine(LoadAssyncOperation());
    }


    IEnumerator LoadAssyncOperation()
    {
        //create an async operation
        AsyncOperation gamelevel = SceneManager.LoadSceneAsync(3);
        
        while (gamelevel.progress < 1)
        {
            //take the progess bar fill = async operation progress.
            _progressBar.fillAmount = gamelevel.progress;
            yield return new WaitForEndOfFrame();


        }

    }
 
}
