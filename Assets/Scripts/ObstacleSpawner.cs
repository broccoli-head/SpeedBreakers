using Unity.VisualScripting;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public float spawnInterval;
    public Vector2 xSpawnRange;
    public float spawnY;
    public float objectSpeed;
    public float despawnY;

    private float timer;
    private bool lastNegative = false;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0;
            SpawnObject();
        }
    }

    void SpawnObject()
    {
        int prefabIndex = Random.Range(0, prefabs.Length);
        float positionX = Random.Range(xSpawnRange.x, xSpawnRange.y);
        float scale = Random.Range(0.2f, 0.5f);

        if (lastNegative)
        {
            positionX = -positionX;
            lastNegative = false;
        }
        else lastNegative = true;
     
        GameObject obj = Instantiate(
            prefabs[prefabIndex],
            new Vector3(positionX, spawnY, 0),
            Quaternion.identity
        );


        transform.localScale = new Vector3(scale, scale, scale);

        MoveObstacle mover = obj.AddComponent<MoveObstacle>();
        mover.speed = objectSpeed;
        mover.despawnY = despawnY;
    }
}
