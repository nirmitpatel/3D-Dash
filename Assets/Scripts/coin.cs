using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour {

	void OnTriggerEnter (Collider collider)
    {
        //switch (collider.gameObject.name)
        //{
            //case "Car":
                CoinBehavior.coinCount += 25;
                Destroy(this.gameObject);
                //break;
       // }
      
    }
}
