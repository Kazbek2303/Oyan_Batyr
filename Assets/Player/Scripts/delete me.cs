using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class deleteme : MonoBehaviour
{
    public string gonextscene;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) {
            GoNextLevel();
        }
    }
    private void GoNextLevel()
    {
        SceneManager.LoadScene(gonextscene);
    }
}
