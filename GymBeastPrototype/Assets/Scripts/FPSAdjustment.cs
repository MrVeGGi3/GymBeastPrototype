using UnityEngine;

public class FPSAdjustment : MonoBehaviour
{
    private int highFPS = 60;
    private int lowFPS = 30;

    public float checkInterval = 5f;
    public float minAcceptableFPS = 50f;

    private float fpsCheckTimer = 0f;
    private float avgFPS = 60f;
    private int frameCount = 0;
    private float timeAccumulator = 0f;

    private bool isLowPerfomanceMode = false;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        Application.targetFrameRate = highFPS;
        QualitySettings.vSyncCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeAccumulator += Time.deltaTime;
        frameCount++;

        fpsCheckTimer += Time.unscaledDeltaTime;
        if(fpsCheckTimer > checkInterval)
        {
            avgFPS = frameCount / fpsCheckTimer;

            if(!isLowPerfomanceMode && avgFPS < minAcceptableFPS)
            {
                Debug.Log("FPS caiu para" + avgFPS + "Reduzindo para 30 FPS");
                Application.targetFrameRate = lowFPS;
                isLowPerfomanceMode = true;
            }

            else if(isLowPerfomanceMode && avgFPS > minAcceptableFPS)
            {
                Application.targetFrameRate = highFPS;
                isLowPerfomanceMode = false;
            }

            frameCount = 0;
            fpsCheckTimer = 0f;
        }
    }
}
