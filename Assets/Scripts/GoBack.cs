using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoBack : MonoBehaviour
{
    public Button back;
    private Button backGame;
    public Image screen;
    // Start is called before the first frame update
    void Start()
    {
        backGame = back.GetComponent<Button>();
        backGame.onClick.AddListener(BackGame);
        screen.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void BackGame()
    {
        SceneManager.LoadScene(0);
    }
}
