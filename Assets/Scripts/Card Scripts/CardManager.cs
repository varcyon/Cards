using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour {
    public CardAsset cardAsset;
    public CardManager PreviewManager;
    public Image faceBackground;
    public Image faceFrame;
    public Image faceCardImage;

    public TMP_Text castCost;
    public TMP_Text attack;
    public TMP_Text health;
    public TMP_Text cardName;
    public TMP_Text cardType;
    public TMP_Text cardDescription;


    private int currentAttack;
    private int currentHealth;
    private int currentCastCost;
    private string currentCardType;

    public Image cardGlowImage;

    
    private bool canBePlayedNow = false;
    public bool CanBePlayedNow {
        get {
            return canBePlayedNow;
        }

        set {
            canBePlayedNow = value;
            cardGlowImage.enabled = value;
        }
    }
    public bool isMine = false;
    public bool IsMine {
        get { return isMine; }
        set { isMine = value; }
    }
    public bool inPlay = false;
    public bool targeting = false;
    /*
    public bool Inplay {
        get { return inPlay; }
        set { inPlay = value; }
    }
    */
    private bool canAttackNow = false;
    public bool CanAttackNow {
        get { return canAttackNow; }
        set { if (inPlay) {
                canAttackNow = value;
                cardGlowImage.enabled = value;
            } }
    }

    public void TakeDamage(int amount, int healthAfter) {
        if(amount > 0) {
            //DamageEffect.CreateDamageEffect(transform.position, amount);
            currentHealth = healthAfter;
            health.text = currentHealth.ToString();
        }
    }

    private void Awake() {
        
    }
    void Start() {
        if (cardAsset != null)
            SetCard();
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void SetCard() {
        cardName.text = cardAsset.name;
        if (cardDescription != null)
            cardDescription.text = cardAsset.Desctription;
        faceCardImage.sprite = cardAsset.CardImage;

        currentCardType = cardAsset.CardType.ToString();
        if (cardType != null)
            cardType.text = currentCardType;

        currentCastCost = cardAsset.CastCost;
        if (castCost != null)
            castCost.text = currentCastCost.ToString();

        if (cardAsset.CardType == CardTypes.Spell) {
            attack.text = null;
            health.text = null;
        } else if (cardAsset.CardType == CardTypes.Creature) {
            currentAttack = cardAsset.Attack;
            if (attack != null)
                attack.text = currentAttack.ToString();

            currentHealth = cardAsset.MaxHealth;
            if (health != null)
                health.text = currentHealth.ToString();
        }

        if (PreviewManager != null) {
            PreviewManager.cardAsset = cardAsset;
            PreviewManager.SetCard();
        }
    }
}
