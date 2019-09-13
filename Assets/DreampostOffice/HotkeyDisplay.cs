using UnityEngine;

public class HotkeyDisplay : MonoBehaviour
{
    [SerializeField] private GameObject dreamPostOffice = null;
    [SerializeField] private GameObject player = null;
    [SerializeField] private GameObject floatingTextPrefab = null;

    private GameObject hotkeyText = null;
    private Vector3 offset = new Vector3(0,4, 0);


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
