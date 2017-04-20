using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehavior : MonoBehaviour {

    public static int coinCount = 0;

    private void OnGUI()
    {
        string name = "Player 2";
        GUI.Box(new Rect(Screen.width - 150, 25, 130, 20), name);
        string coinText = "Total Coins: " + coinCount;
        GUI.Box(new Rect(Screen.width - 150, 50, 130, 20), coinText);

        name = "Player 1";
        GUI.Box(new Rect(Screen.width - 1350, 25, 130, 20), name);
        GUI.Box(new Rect(Screen.width - 1350, 50, 130, 20), coinText);
    }
}
