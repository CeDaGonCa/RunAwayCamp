using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MainLightSource : MonoBehaviour
{
    private Light2D mainLight;
    void Awake()
    {
        mainLight = GetComponent<Light2D>();
    }
    void Start()
    {
        mainLight.intensity = 0f;
    }
}
