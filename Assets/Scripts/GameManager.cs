using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> tables;
    public GameObject beerPrefab;

    float timer;
    float changeTime = 2.0f;
    int tableIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        timer = changeTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            tableIndex = Random.Range(0, 5);
            GameObject table = tables[tableIndex];
            float randomX = Random.Range(-0.7f, 0.7f);
            float randomY = Random.Range(-0.7f, 0.7f);
            Vector2 beerPosition = new Vector2(table.transform.position.x + randomX, table.transform.position.y + randomY);
            Instantiate(beerPrefab, beerPosition, Quaternion.identity);
            // beerPrefab.transform.parent = table.transform;
            // TODO: Adjust time depending on how many removables are on screen.
            timer = changeTime;
        }
    }
}
