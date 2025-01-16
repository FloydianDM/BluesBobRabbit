using TMPro;
using UnityEngine;

public class DeathUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _player1DeathCountText;
    [SerializeField] private TMP_Text _player2DeathCountText;
    
    public void UpdatePlayer1DeathCountText(int value)
    {
        _player1DeathCountText.text ="Player1 Death Count: " + value.ToString();
    }

    public void UpdatePlayer2DeathCountText(int value)
    {
        _player2DeathCountText.text = "Player2 Death Count: " + value.ToString();
    }
}
