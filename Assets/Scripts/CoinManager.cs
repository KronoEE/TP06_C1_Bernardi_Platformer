using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private GameObject panelWin;
    public int coinCount = 0;
    public bool reachedCoinGoal = false;

    private void Update()
    {
        coinText.text = coinCount.ToString();

        if (coinCount >= 24)
        {
            reachedCoinGoal = true;
        }
        else
        {
            reachedCoinGoal = false;
        }
    }
}
