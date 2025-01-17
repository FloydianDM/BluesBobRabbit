using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ScoreUI : MonoBehaviour
{
    [FormerlySerializedAs("_player1DeathCountText")] [SerializeField] private TMP_Text _player1ScoreCountText;
    [FormerlySerializedAs("_player2DeathCountText")] [SerializeField] private TMP_Text _player2ScoreCountText;
    
    public void UpdatePlayer1ScoreCountText(int value)
    {
        _player1ScoreCountText.text ="Host Score Count: " + value.ToString();
    }

    public void UpdatePlayer2ScoreCountText(int value)
    {
        _player2ScoreCountText.text = "Client Score Count: " + value.ToString();
    }
}
