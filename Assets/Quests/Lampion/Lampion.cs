using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UIElements.Image;


public class Lampion : MonoBehaviour
{
    public GameObject player;
    public float maxDistanceToPlayer = 10f;
    public Quest quest;
    public GameObject ui;

    [SerializeField] private GameObject npcImage = null;
    
    
    private void Start()
    {
        

        
    }


    private void LampionActivation()
    {
        //Get Data of the Scriptable Object
        ui.GetComponentInChildren<Text>().text = quest.questDescription;
        npcImage.GetComponent<UnityEngine.UI.Image>().sprite = quest.questNPCImage;
        
        
        ui.GetComponent<Canvas>().enabled = true;
    }

    private void Update()
    {
        var playerInRange = Vector3.Distance(transform.position, player.transform.position);

        if (Input.GetKeyDown(KeyCode.E) && playerInRange <= maxDistanceToPlayer)
        {
            LampionActivation();
        }
    }
}
