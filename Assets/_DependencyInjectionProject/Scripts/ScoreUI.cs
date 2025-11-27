using Reflex.Attributes;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    private IScoreReader _scoreReader;

    [Inject] // Requests the Reader access
    public void Construct(IScoreReader reader)
    {
        _scoreReader = reader;
    }

    private void Update()
    {
        // I can see the score!
        Debug.Log(_scoreReader.CurrentScore);
    }
}