using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public int _score;

    public void AddScore(int value)
    {
        _score = _score + value;
    }
}