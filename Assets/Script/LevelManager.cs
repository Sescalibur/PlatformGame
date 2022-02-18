using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    Text Score;


    private void Start()
    {
        Score = GameObject.Find("SkorValue").GetComponent<Text>();
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void Close()
    {

    }

    public void ScoreAdds(int score)
    {
        int scoreValue = int.Parse(Score.text);
        scoreValue += score;
        Score.text = scoreValue.ToString();
    }
}
