using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotkeyDisplay : MonoBehaviour
{
    [SerializeField] private GameObject dreamPostOffice;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject floatingTextPrefab;

    private GameObject hotkeyText;
    private Vector3 offset = new Vector3(4,4, 0);


    void Update()
    {
        var playerInRange = Vector3.Distance(dreamPostOffice.transform.position, player.transform.position);

        if (playerInRange <= 10.0f)
        {
            if (!transform.GetComponentInChildren<TextMesh>())
            {
                hotkeyText = Instantiate(floatingTextPrefab, transform.position + offset, Quaternion.identity, transform);
            }

            hotkeyText.transform.LookAt(Camera.main.transform);
            hotkeyText.transform.Rotate(0,180,0);
        }
        else if (hotkeyText != null)
        {
            Destroy(hotkeyText);
        }
    }
}
