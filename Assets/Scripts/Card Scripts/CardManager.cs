using System.Collections;
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


    public int currentAttack;
    private int currentHealth;
    private int currentCastCost;
    private string currentCardType;

    public Image cardGlowImage;
    public Image cardTargetedGlow;
    public Image cardActiveGlow;

    public int cardSlot;
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
    private bool inPlay = false;
    public bool targeting = false;
    public bool Inplay {
        get { return inPlay; }
        set { inPlay = value; }
    }
    private bool canAttack = false;
    public bool CanAttack {
        get { return canAttack; }
        set {
            if (inPlay) {
                canAttack = value;
            }
        }
    }

    public bool isTargeted = false;
    public bool IsTargeted {
        get { return isTargeted; }
        set {
            isTargeted = value;
            cardTargetedGlow.enabled = value;
        }
    }
    public bool isActive = false;
    public bool IsActive {
        get { return isActive; }
        set {
            isActive = value;
            cardActiveGlow.enabled = value;
        }
    }
    public void TakeDamage(int amount) {
        if (amount > 0) {
            //DamageEffect.CreateDamageEffect(transform.position, amount);
            currentHealth -= amount;
            health.text = currentHealth.ToString();
            if (currentHealth <= 0) {
                StartCoroutine(Death());
            }
        }
    }
    public IEnumerator Death() {
        yield return new WaitForSecondsRealtime(1f);
        print(cardName + " dies...");
        Destroy(this.gameObject);
    }
    private void Awake() {
        if (cardAsset != null && !Inplay)
            SetCard();
    }
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (currentHealth > cardAsset.MaxHealth) {
            health.color = Color.green;
        }
        if (currentHealth < cardAsset.MaxHealth) {
            health.color = Color.red;
        }

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
