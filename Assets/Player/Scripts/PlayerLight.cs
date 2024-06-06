using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    public float lightRange = 10f;        // Range of the light
    public float lightIntensity = 0.2f;    // Intensity of the light
    public Color lightColor = Color.white; // Color of the light

    private Light playerLight;

    void Start()
    {
        // Create a new GameObject for the light
        GameObject lightGameObject = new GameObject("PlayerLight");

        // Add a Light component to the GameObject
        playerLight = lightGameObject.AddComponent<Light>();

        // Set the light type to Point
        playerLight.type = LightType.Point;

        // Set the light properties
        playerLight.range = lightRange;
        playerLight.intensity = lightIntensity;
        playerLight.color = lightColor;

        // Set the culling mask to ignore the player's layer
        playerLight.cullingMask = ~(1 << LayerMask.NameToLayer("IgnoreLight"));

        // Make the light a child of the player object
        lightGameObject.transform.SetParent(transform);

        // Position the light at the player's position
        lightGameObject.transform.localPosition = Vector3.zero;
    }

    // Update the light color in case it is changed in the Inspector at runtime
    void Update()
    {
        playerLight.color = lightColor;
    }
}
