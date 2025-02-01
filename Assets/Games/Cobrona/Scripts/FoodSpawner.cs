using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public Transform right_Border, left_Border, top_Border, bottom_Border;
    public GameObject foodPrefab;
    private GameObject currentFood;
    public SnakeBehaviour pos;

    void Start()
    {
        if (pos != null)
        {
            Spawn(pos.GetSnakePositions());
        }
    }

    public void Spawn(List<Vector2> occupiedPositions)
    {
        bool positionValid = false;
        Vector2 spawnPosition = Vector2.zero;
        while (!positionValid)
        {
            int x = (int)Random.Range(left_Border.transform.position.x, right_Border.transform.position.x);
            int y = (int)Random.Range(bottom_Border.transform.position.y, top_Border.transform.position.y);

            spawnPosition = new Vector2(x, y);

            positionValid = true; 
            foreach (Vector2 pos in occupiedPositions)
            {
                if (spawnPosition == pos)
                {
                    positionValid = false;
                    break;
                }
            }
        }
        DestroyCurrentFood();

        currentFood =  Instantiate(foodPrefab, spawnPosition, Quaternion.identity);
    }

    public void DestroyCurrentFood() 
    {
        if (currentFood != null)
        {
            Destroy(currentFood);
            currentFood = null;
        }
    }


}
