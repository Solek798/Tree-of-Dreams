using System.Collections.Generic;
using UnityEngine;

public class LampionFactory : MonoBehaviour
{
    [SerializeField] private GameObject lampionPrefab = null;
    [SerializeField] private GameObject player = null;
    public Farmland farmland = null;

    [SerializeField] private BoundsInt targetBounds;
    [SerializeField] private float spawnRadius = 30;
    [SerializeField] private int maxGeneratingAttempts = 10;
    public List<QuestData> questData = null;

    [SerializeField] private AudioSource audioPlayer = null;
    private Lampion newLampion = null;
    private FarmlandSpace targetSpace = null;
    
    public void CreateAndSend()
    {
        if (questData.Count <= 0) return;

        // Select random FarmlandSpace
        var levels = farmland.GetAllLevels();
        var targetLevel = levels[Random.Range(0, levels.Length - 1)];
        var targetSpace = targetLevel.GetRandomSpace(maxGeneratingAttempts);
        if (targetSpace == null) return;

        var newLampion = Create();

        
        Send(newLampion, targetSpace);
    }

    private Lampion Create()
    {
        var newLampion = Instantiate(lampionPrefab, transform).GetComponent<Lampion>();
        newLampion.player = player;
        
        newLampion.questData = questData[Random.Range(0, questData.Count - 1)];
        questData.Remove(newLampion.questData);

        return newLampion;
    }

    private void Send(Lampion _newLampion, FarmlandSpace _targetSpace)
    {
        
        newLampion = _newLampion;
        targetSpace = _targetSpace;

        // Select random spawn point
        newLampion.transform.localPosition = new Vector3(spawnRadius * Random.value, 0, 0);
        newLampion.transform.RotateAround(
            newLampion.transform.parent.position, 
            Vector3.up, 
            360 * Random.value
        );
        
        // send Lampion by setting TravelTarget
        targetSpace.Lampion = newLampion;
        newLampion.TravelTarget = targetSpace.transform.position;
    }
    
    private void Update()
    {
        RingWhenArrived();
    }

    private void RingWhenArrived()
    {
        if(newLampion != null && targetSpace != null)
        {
            if ((int)newLampion.transform.position.x == (int)targetSpace.transform.position.x &&
                (int)newLampion.transform.position.z == (int)targetSpace.transform.position.z)
            {
                audioPlayer.Play();
                newLampion = null;
                targetSpace = null;
            }
        }
    }
}
