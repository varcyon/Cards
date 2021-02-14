using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStageManager : MonoBehaviour
{
    public static BattleStageManager i;
    public List<Transform> playerCreatureSlotLocs = new List<Transform>();
    public List<Transform> enemyCreatureSlotLocs = new List<Transform>();
    public Transform handLoc;
    public BattleManagerSO BM;
    private void Awake() {
        if(i != this) {
            i = this;
        }
    }

    private void Start() {
        int index = 0;
        foreach (CardAsset card in BM.playerCreatureSlots) {
            if(card != null) {

               GameObject go = Instantiate(BM.creatureCard, playerCreatureSlotLocs[index]);
                go.GetComponent<CardManager>().cardAsset = BM.playerCreatureSlots[index];
            }
            index++;
        }
    }
}
