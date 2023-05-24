using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Countdown : MonoBehaviour
{

    [SerializeField] float totalTime = 300f;
    float remainingTime;

    public TMP_Text displayText;
    bool gameCountdown;
    public static bool levelEnded;
    
    // Start is called before the first frame update
    void Start()
    {
        displayText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(startCountdown(3));
        remainingTime = totalTime;
    }

    // Update is called once per frame
    void Update()
    {
        int minutes = (int)(remainingTime/60);
        int seconds = (int)(remainingTime%60);
        int miliseconds = (int)((remainingTime*10)%10);
        if (gameCountdown && !levelEnded)
        {
            remainingTime -= Time.deltaTime;
            displayText.text = string.Format("{0:0}:{1:00}:{2:0}",minutes,seconds,miliseconds);
        }
        if(remainingTime <= 0)
        {
            gameCountdown = false;
            displayText.text = "TIME'S UP!";
            levelEnded = true;
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                RestartLevel();
            }
        }
        else if (remainingTime >0 && levelEnded)
        {
            gameCountdown = false;
            displayText.text = string.Format("LEVEL COMPLETE!\nTIME REMAINING={0}:{1:00}:{2}",minutes,seconds,miliseconds);
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                RestartLevel();
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
            {
                QuitGame();
            }
    }

    IEnumerator startCountdown (int seconds)
    {
        while (seconds >= 0)
        {
            if(seconds>0)
            {
                displayText.text = (seconds).ToString();
            }
            else if (seconds == 0)
            {
                displayText.text = "GO!";
            }
            seconds--;
            yield return new WaitForSeconds(1f);
        }
        displayText.text ="";
        gameCountdown = true;
    }

    void RestartLevel ()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        Countdown.levelEnded = false;
    }

    void QuitGame ()
    {
        if(Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
    }
}
