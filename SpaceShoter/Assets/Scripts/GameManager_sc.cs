using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_sc : MonoBehaviour
{
    [SerializeField]
    private bool isGameOver;

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && isGameOver == true) {
            SceneManager.LoadScene("Game");
        }
    }

    public void GameOver() {
        isGameOver = true;
    }
}
