using System.Collections.Generic;
using UnityEngine;

public class BattleStageManager : MonoBehaviour {
    public static BattleStageManager i;
    public Transform handLoc;
    public BattleManagerSO BM;
    [SerializeField]
    private List<Transform> playerCreatureSlotLocs = new List<Transform>(6);
    [SerializeField]
    private List<Transform> enemyCreatureSlotLocs = new List<Transform>(6);
    public int numOfCreatures=0;
    public int numOfCreaturesSet=0;
    private void Awake() {
        if (i != this) {
            i = this;
        }

    }

    private void Start() {
        int index = 0;
        foreach (CardAsset card in BM.playerCreatureSlots) {
            if (card != null) {

                GameObject go = Instantiate(BM.creatureCard, playerCreatureSlotLocs[index]);
                go.GetComponent<CardManager>().cardAsset = BM.playerCreatureSlots[index];
            }
            index++;
        }

        numOfCreatures = UnityEngine.Random.Range(1, 7);
        while(numOfCreaturesSet < numOfCreatures) {
            SetEnemyCreature();
        }
    }

    private void SetEnemyCreature() {
        int slot = UnityEngine.Random.Range(1, 7);
        if (BM.enemyCreatureSlots[slot] == null) {
            BM.enemyCreatureSlots[slot] = BM.enemyCreatures[UnityEngine.Random.Range(0, BM.enemyCreatures.Count - 1)];
            numOfCreaturesSet++;
        } 

    }
}
