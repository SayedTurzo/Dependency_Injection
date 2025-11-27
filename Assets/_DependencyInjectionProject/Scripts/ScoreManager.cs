using UnityEngine;

public class ScoreManager : IScoreReader, IScoreWriter
{
    public int CurrentScore { get; private set; }
    
    public void AddPoints(int amount)
    {
        CurrentScore += amount;
        Debug.Log($"[Score] Points Added! Total: {CurrentScore}");
    }
}