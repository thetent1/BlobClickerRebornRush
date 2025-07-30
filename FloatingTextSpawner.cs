using UnityEngine;

public class FloatingTextSpawner : MonoBehaviour
{
    public GameObject floatingTextPrefab;
    public Transform spawnParent;

    public float verticalOffset = 200f; // how far above the click

    public void SpawnFloatingText(string text, Color color)
    {
        GameObject instance = Instantiate(floatingTextPrefab, spawnParent);
        instance.GetComponent<FloatingText>().Initialize(text, color);

        Vector3 spawnPos = Input.mousePosition + new Vector3(0f, verticalOffset, 0f);
        instance.transform.position = spawnPos;
    }

    //overload
    public void SpawnFloatingText(string text, Color color, Vector3 uiPosition)
    {
        GameObject instance = Instantiate(floatingTextPrefab, spawnParent);
        instance.transform.position = uiPosition; // use UI position directly
        instance.GetComponent<FloatingText>().Initialize(text, color);
    }

    //overload
    public void SpawnFloatingText(string text, Color color, Vector3 uiPosition, Transform parentOverride = null)
    {
        // always instantiate under spawnParent (the Canvas) first
        GameObject instance = Instantiate(floatingTextPrefab, spawnParent);

        if (parentOverride != null)
            instance.transform.SetParent(parentOverride, worldPositionStays: true);

        instance.transform.position = uiPosition;
        instance.GetComponent<FloatingText>().Initialize(text, color);
    }


}
