using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorldBorderState
{
    public Transform position;
    public float ratio;

    WorldBorderState(Transform position, float ratio)
    {
        this.position = position;
        this.ratio = ratio;
    }
}

public class ShrinkLogic : MonoBehaviour
{
    public float shrinkDuration = 60f;
    public float shrinkDelay = 120f;

    private int tapeCounter = 0;
    private float totalTime = 0;

    private Vector3 currentPosition;
    private Vector3 currentScale;

    [SerializeField]
    public List<WorldBorderState> shrinkTape;

    // Start is called before the first frame updates
    void Start()
    {
        currentPosition = transform.position;
        currentScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        totalTime += Time.deltaTime;
        if (totalTime > shrinkDelay && tapeCounter < shrinkTape.Count)
        {
            if (totalTime <= shrinkDelay + shrinkDuration)
            {
                transform.position = currentPosition - ((currentPosition - shrinkTape[tapeCounter].position.localPosition) * ((totalTime - shrinkDelay) / (shrinkDuration)));

                float scaler = (1 - (shrinkTape[tapeCounter].ratio * ((totalTime - shrinkDelay) / shrinkDuration)));

                Vector3 newScale = currentScale;
                newScale.x = currentScale.x + ((currentScale.x * scaler) - currentScale.x);
                newScale.y = currentScale.y + ((currentScale.y * scaler) - currentScale.y);
                newScale.z = currentScale.z + ((currentScale.z * scaler) - currentScale.z);
                transform.localScale = newScale;
            }
            else
            {
                tapeCounter++;
                currentPosition = transform.position;
                currentScale = transform.localScale;
                totalTime = 0;
            }
        }
    }
}
