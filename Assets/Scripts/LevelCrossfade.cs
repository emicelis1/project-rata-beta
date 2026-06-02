using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCrossfade : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            LoadNextLevel();
        }
        

        }

        public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }


    }

