using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transitionBreach;
    public float transitionTime = 1f;
    private int levelIndex;

    public string levelName;

    private void Start()
    {
        levelName = SceneManager.GetActiveScene().name;

        

    }
    // Update is called once per frame
    void Update()
    {
        if (levelName == "Zero" && Input.GetKeyUp(KeyCode.RightArrow))
        {
            LoadNextLevel();
        }
        
        if (levelName == "Active" && Input.GetKeyDown(KeyCode.RightArrow))
        {
            LoadNextLevel();
        }



        if (levelName == "Active")
        {
            transitionBreach.SetTrigger("Active");
        } 
    }

    public void LoadNextLevel()
    {
       if(levelName == "Active")
        {
            levelIndex = 0;
           
        }
        else if (levelName == "Zero")
        {
            levelIndex = 1;
        }
       StartCoroutine(LoadLevel(levelIndex));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transitionBreach.SetTrigger("Open");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
