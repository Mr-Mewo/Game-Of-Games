using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBehaviour : MonoBehaviour
{
    Vector2 dir;
    Vector2 headLast_Pos;
    Color defaultColor;
    public Color targetColor;

    public GameObject partPrefab;
    FoodSpawner spawner;
    GameStarter starter;

    List<SpriteRenderer> renderers = new List<SpriteRenderer>();
    List<GameObject> snakeParts = new List<GameObject>();

    bool isAlive;
    int score;

    public Camera thisCamera;

    void Start()
    {
        score = 0;
        dir = Vector2.right;
        spawner = thisCamera.GetComponent<FoodSpawner>();
        defaultColor = GetComponent<SpriteRenderer>().color;
        starter = thisCamera.GetComponent<GameStarter>();
        isAlive = true;
        InvokeRepeating("Movement", 0.09f, 0.09f);
    }

    void Update()
    {
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && dir != -Vector2.right) dir = Vector2.right;
        else if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && dir != Vector2.right) dir = -Vector2.right;
        else if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && dir != -Vector2.up) dir = Vector2.up;
        else if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && dir != Vector2.up) dir = -Vector2.up;
    }

    void UpdateRenderersList()
    {
        for (int i = 0; i < snakeParts.Count; i++)
        {
            if (!renderers.Contains(snakeParts[i].GetComponent<SpriteRenderer>()))
            {
                renderers.Add(snakeParts[i].GetComponent<SpriteRenderer>());
            }
        }
    }

    void Movement()
    {
        if (isAlive)
        {
            headLast_Pos = transform.position;
            transform.Translate(dir);

            if (snakeParts.Count > 0)
            {
                snakeParts[snakeParts.Count - 1].transform.position = headLast_Pos;
                snakeParts.Insert(0, snakeParts[snakeParts.Count - 1]);
                snakeParts.RemoveAt(snakeParts.Count - 1);
            }
        }
    }

    void Grow()
    {
        Vector2 spawnPoint = new Vector2(transform.position.x - dir.x, transform.position.y - dir.y);
        bool isSafe = false;
        while (!isSafe)
        {
            isSafe = true;

            foreach (GameObject part in snakeParts)
            {
                if ((Vector2)part.transform.position == spawnPoint)
                {
                    spawnPoint -= dir;
                    isSafe = false;
                    break;
                }
            }
        }

            GameObject partToAdd = Instantiate(partPrefab, spawnPoint, Quaternion.identity);
            snakeParts.Add(partToAdd);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border" || collision.gameObject.tag == "Self")
        {
            Debug.Log("Colis�o com" + collision.gameObject.tag);
            Die();
        }
        else if (collision.gameObject.tag == "Food")
        {
            Destroy(collision.gameObject);
            spawner.Spawn(GetSnakePositions());
            Grow();
            UpdateRenderersList();
            score++;
        }
    }

    void ChangeColor()
    {
        Debug.Log("Mudando a cor...");
        foreach (SpriteRenderer local in renderers)
        {
            if (local.color == defaultColor)
                local.color = targetColor;
            else if (local.color == targetColor)
                local.color = defaultColor;
        }
    }

    public void Die()
    {
        Debug.Log("Cobra Morreu.");
        isAlive = false;

        InvokeRepeating("ChangeColor",  .1f, .1f);

        starter.ShowGameOver(score);

        foreach (GameObject part in snakeParts)
        {
            Destroy(part, 2f);
        }
        Destroy(gameObject, 2f);
    }

    public List<Vector2> GetSnakePositions()
    {
        List<Vector2> positions = new List<Vector2>();

        // Adiciona a posi��o da cabe�a
        positions.Add(transform.position);

        // Adiciona as posi��es das partes da cobra
        foreach (GameObject part in snakeParts)
        {
            positions.Add(part.transform.position);
        }

        return positions;
    }
}
