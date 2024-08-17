using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocksRemaining : MonoBehaviour
{
    public Text rockText;

    void Update()
    {
        rockText.text = PlayerStats.RocksRemaining.ToString() + " Left";
    }
}