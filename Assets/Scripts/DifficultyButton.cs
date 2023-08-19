using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;
    public int difficulty;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manger").GetComponent<GameManager>();
        button = GetComponent<Button>();
        button.onClick.AddListener(setDifficulty);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void setDifficulty()
    {
        Debug.Log(button.gameObject.name + " Was Clicked!");
        gameManager.startGame(difficulty);
    }
}
