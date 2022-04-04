using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public Image menu;
    public Button startGame;
    public Button help;
    private Button helpButton;
    public Button quitGame;
    private Button startButton;
    private Button endButton;
    // Start is called before the first frame update
    void Start()
    {
        startButton = startGame.GetComponent<Button>();
        endButton = quitGame.GetComponent<Button>();
        helpButton = help.GetComponent<Button>();
        menu.gameObject.SetActive(true);
        startGame.onClick.AddListener(SceneManagement);
        helpButton.onClick.AddListener(Help);
        endButton.onClick.AddListener(Quit);
    }

    // Update is called once per frame
    void Update()
    {
       // quitGame.onClick.AddListener(Quit);
    }
    void SceneManagement()
    {
        SceneManager.LoadScene(2);
    }
    void Help()
    {
        SceneManager.LoadScene(1);
    }
    private void Quit()
    {
        Application.Quit();
    }

}
