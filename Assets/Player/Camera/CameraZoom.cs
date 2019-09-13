using System.Collections;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private Transform focusTransform =null;
    [SerializeField] private Transform player = null;
    [SerializeField] private int minZoomLevel = -5;
    [SerializeField] private int maxZoomLevel = 5;

    private int _currentZoomLevel = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && _currentZoomLevel + 1 <= maxZoomLevel)
        {
            StartCoroutine(Move(5));
            _currentZoomLevel++;
        }
        if (Input.GetKeyDown(KeyCode.O) && _currentZoomLevel - 1 >= minZoomLevel)
        {
            StartCoroutine(Move(-5));
            _currentZoomLevel--;
        }
    }

    private IEnumerator Move(float amount)
    {
        float absoluteZoom = 0;

        while (Mathf.Abs(absoluteZoom) < Mathf.Abs(amount))
        {
            var zoom = amount * Time.deltaTime;
            
            transform.Translate(0, 0, zoom);
            absoluteZoom += zoom;
            
            yield return null;
        }
    }
}