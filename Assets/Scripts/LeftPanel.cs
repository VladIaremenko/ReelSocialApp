using UnityEngine;
using UnityEngine.UI;

public class LeftPanel : MonoBehaviour
{

    public Button leftPanelButton;
    public Button closeButton;
    private bool leftPanelState = false;
    public GameObject leftPanelObject;

    public Button goldButton;
    public Text goldCountLabel;

    public Button coinsButton;
    public Text coinsCountLabel;

    private int _gold = -1;

    public int Gold
    {
        get => _gold;
        set
        {
            if ( value < 0 )
                value = 0;

            if ( _gold == value )
                return;

            _gold = value;
            goldCountLabel.text = value.ToString();
        }
    }

    private int _coins = 0;

    public int Coins
    {
        get => _coins;
        set
        {
            if ( value < 0 )
                value = 0;

            if ( _coins == value )
                return;

            _coins = value;
            coinsCountLabel.text = value.ToString();
        }
    }

    private void Start() {
        Gold = 500;
        Coins = 100;
    }

    protected void OnEnable()
    {
        leftPanelButton.onClick.AddListener( LeftPanelButtonClicked );
        closeButton.onClick.AddListener( LeftPanelButtonClicked );
    }

    protected void OnDisable()
    {
        leftPanelButton.onClick.RemoveAllListeners();
        closeButton.onClick.RemoveAllListeners();
    }

    enum Bttype
    {
        none,
        messages,
        users,
        room
    }

    private void LeftPanelButtonClicked()
    {
        leftPanelState = !leftPanelState;
        leftPanelObject.SetActive( leftPanelState );
        closeButton.gameObject.SetActive( leftPanelState );
    }

    private void GoldButtonClicked()
    {
        changeColor( Bttype.none );
    }

    private void changeColor( Bttype bttype )
    {
        var disabledcolor = new Color( 0.188f, 0.13f, 0.15f );
        var enabledcolor = new Color( 0.85f, 0.11f, 0.29f );
    }
}
