using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float speed = 8f;
    public float speedIncrease = 0.8f;

    public static System.Action OnBackgroundScrolled;
    public static float TotalScrolledDistance { get; private set; }
    public static float BackgroundHeight { get; private set; }
    public static int TotalScrolls { get; private set; }

    public static bool gameRunning = true;

    private Transform[] currentSet;
    private float backgroundHeight;
    private int activeSetIndex = -1;

    void Start()
    {
        TotalScrolledDistance = 0f;
        TotalScrolls = 0;

        if (transform.childCount == 0)
        {
            Debug.LogError("ScrollingBackground: No background sets under this GameObject!");
            return;
        }

        ChangeSet(0);
    }

    public int GetPageCount()
    {
        if (activeSetIndex < 0) return 2;
        return transform.GetChild(activeSetIndex).childCount;
    }

    public void ChangeToRandomSet()
    {
        if (transform.childCount <= 1) return;

        int newIndex;
        do
        {
            newIndex = Random.Range(0, transform.childCount);
        }
        while (newIndex == activeSetIndex);

        ChangeSet(newIndex);
    }

    public void ChangeSet(int index)
    {
        if (index < 0 || index >= transform.childCount)
        {
            Debug.LogError("ScrollingBackground: Invalid set index: " + index);
            return;
        }

        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(false);

        Transform set = transform.GetChild(index);
        set.gameObject.SetActive(true);
        activeSetIndex = index;

        currentSet = new Transform[set.childCount];
        for (int i = 0; i < set.childCount; i++)
            currentSet[i] = set.GetChild(i);

        backgroundHeight = currentSet[0].GetComponent<SpriteRenderer>().bounds.size.y;
        BackgroundHeight = backgroundHeight;

        // Pozycjonowanie dw�ch pierwszych
        currentSet[0].localPosition = new Vector3(0, 0, 0);
        currentSet[1].localPosition = new Vector3(0, backgroundHeight, 0);

        Debug.Log("Active background set: " + set.name);
    }

    void Update()
    {
        if (!gameRunning || currentSet == null || currentSet.Length < 2)
            return;

        speed += speedIncrease * Time.deltaTime;
        float delta = speed * Time.deltaTime;

        TotalScrolledDistance += delta;

        foreach (var bg in currentSet)
            bg.position += Vector3.down * delta;

        foreach (var bg in currentSet)
        {
            if (bg.position.y < -backgroundHeight)
            {
                Transform other = (bg == currentSet[0]) ? currentSet[1] : currentSet[0];

                bg.position = new Vector3(
                    bg.position.x,
                    other.position.y + backgroundHeight,
                    bg.position.z
                );

                TotalScrolls++;
                OnBackgroundScrolled?.Invoke();
            }
        }
    }

    public void ResetScrolling()
    {
        TotalScrolls = 0;
        TotalScrolledDistance = 0;

        currentSet[0].localPosition = new Vector3(0, 0, 0);
        currentSet[1].localPosition = new Vector3(0, BackgroundHeight, 0);
    }

    public static void ResetTotalScrolledDistance()
    {
        TotalScrolledDistance = 0f;
    }
}
