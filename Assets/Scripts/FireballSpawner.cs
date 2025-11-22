using UnityEngine;

public class FireballSpawner : MonoBehaviour
{
    public GameObject fireballPrefab;
    public float spawnInterval = 2f;
    public float minX = -8f;
    public float maxX = 8f;
    public float spawnY = 6f;
    public float fireballSpeed = 5f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnFireball), 4f, spawnInterval);
    }

    void SpawnFireball()
    {
        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPos = new Vector3(randomX, spawnY, 0f);

        GameObject fireball = Instantiate(fireballPrefab, spawnPos, Quaternion.identity);
        StartCoroutine(MoveFireballDown(fireball));
    }

    System.Collections.IEnumerator MoveFireballDown(GameObject fireball)
    {
        while (fireball != null)
        {
            fireball.transform.position += Vector3.down * fireballSpeed * Time.deltaTime;
            yield return null;
        }
    }
}
