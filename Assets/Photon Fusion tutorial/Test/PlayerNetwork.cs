using Fusion;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
    }
}