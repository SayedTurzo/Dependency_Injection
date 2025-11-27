using System;
using Reflex.Attributes;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [Inject] IScoreWriter _scoreWriter;

    private void Start()
    {
        _scoreWriter.AddPoints(100);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _scoreWriter.AddPoints(100);
        }
    }
}
