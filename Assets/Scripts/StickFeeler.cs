using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickFeeler : MonoBehaviour
{
    [SerializeField] private StickFollower stickFollower;

    public StickFollower ReturnStickFollower()
    {
        return stickFollower;
    }
}
