using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> tables;
    public GameObject beerPrefab;
    public GameObject blueTrayPrefab;
    public GameObject redTrayPrefab;

    float beerTimer;
    float beerChangeTime = 4.0f;
    float trayTimer;
    float trayChangeTime = 9.0f;
    int tableIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        beerTimer = beerChangeTime;
        trayTimer = trayChangeTime;
    }

    // Update is called once per frame
    void Update()
    {
        beerTimer -= Time.deltaTime;
        trayTimer -= Time.deltaTime;

        if (beerTimer < 0)
        {
            spawnBeer();
        }
        if (trayTimer < 0)
        {
            spawnTray();
        }
    }

    void spawnBeer()
    {
        tableIndex = Random.Range(0, 5);
        GameObject table = tables[tableIndex];
        float randomX = Random.Range(-0.7f, 0.7f);
        float randomY = Random.Range(-0.7f, 0.7f);
        Vector2 beerPosition = new Vector2(table.transform.position.x + randomX, table.transform.position.y + randomY);
        Instantiate(beerPrefab, beerPosition, Quaternion.identity);
        // TODO: Set beer as a child of the table so the sorting group is applied.
        // beerPrefab.transform.parent = table.transform;
        // TODO: Adjust time depending on how many removables are on screen.
        beerTimer = beerChangeTime;
    }

    void spawnTray()
    {
        tableIndex = Random.Range(1, 5);
        GameObject table = tables[tableIndex];
        float randomX = Random.Range(-0.4f, 0.4f);
        float randomY = Random.Range(-0.5f, 0.7f);
        Vector2 trayPosition = new Vector2(table.transform.position.x + randomX, table.transform.position.y + randomY);
        Instantiate(Random.Range(0, 2) < 1 ? blueTrayPrefab : redTrayPrefab, trayPosition, Quaternion.identity);
        // TODO: Set tray as a child of the table so the sorting group is applied.
        trayTimer = trayChangeTime;
    }
}
