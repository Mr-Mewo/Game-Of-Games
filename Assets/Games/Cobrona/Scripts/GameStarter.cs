using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameStarter : MonoBehaviour
{
    public GameObject snakeHeadPrefab; // Prefab da cabe a da cobra
    public GameObject foodPrefab;
    private GameObject currentSnake;  // Refer ncia   cobra na cena
    private GameObject food;
    FoodSpawner foodSpawner;


    // public Canvas main; // Canvas principal
    // public Button playButton; // Bot o para iniciar ou reiniciar o jogo
    // public TextMeshProUGUI gOverLabel, scoreLabel, scoreMessage; // Elementos de UI

    private void Start()
    {
        foodSpawner = GetComponent<FoodSpawner>();

        ShowMenu(false);
        
        StartGame();
        // playButton.onClick.AddListener(StartGame);
        
        // The only thing I have to add. Sorry honey
        // - Oh, forget about it
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ShowMenu(bool isGameOver)
    {
        // main.enabled = true;
        //
        // playButton.interactable = true;
        // gOverLabel.gameObject.SetActive(isGameOver);
        // scoreLabel.gameObject.SetActive(isGameOver);
        // scoreMessage.gameObject.SetActive(isGameOver);
    }

    public void StartGame()
    {
        // main.enabled = false;

        ResetGame();
    }

    private void SpawnSnake()
    {
        currentSnake = Instantiate(snakeHeadPrefab, Vector2.zero, Quaternion.identity);
    }

    public void ShowGameOver(int score)
    {
        ShowMenu(true);
        // scoreMessage.text = $"{score}";
    }

    private void ResetGame()
    {
        if (currentSnake != null)
        {
            Destroy(currentSnake);
        }

        SpawnSnake();

        SnakeBehaviour snakeBehaviour = currentSnake.GetComponent<SnakeBehaviour>();

        if (snakeBehaviour != null)
        {
            List<Vector2> snakePositions = snakeBehaviour.GetSnakePositions();
            foodSpawner.DestroyCurrentFood(); 
            foodSpawner.Spawn(snakePositions);
        }
        else
        {
            Debug.LogError("Erro: SnakeBehaviour n o foi encontrado no objeto currentSnake.");
        }
    }


    public void ExitGame()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
        
        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
