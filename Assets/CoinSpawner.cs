using UnityEngine;
using System.Collections;

public class CoinSpawner : MonoBehaviour
{
	public float heigth = 30;
	public float width = 30;
	public int count = 2;

	public float xMul = 1;
	public float yMul = 1;
	public float xParamMul = 7;

	// Use this for initialization
	void Start () {
		var coinsLeft = count;
		var draw = true;
		var skiped = 0;
		var x = 0;
		var y = 0;
		var coinsToSkip = Random.Range (3, 10);

		while (coinsLeft > 0) {
			if (draw) {
				for (int i = 0; i < Random.Range(3,6); i++) {
					var coinInstance = Resources.Load("coin");
					var pos = new Vector3(Mathf.Cos(x * xParamMul) * xMul, y * yMul, 0);

					var coin = Instantiate( coinInstance, 
					                       pos+transform.position, 
					                       Quaternion.identity ) as GameObject;
					coin.transform.parent = transform;
					coinsLeft--;
					y++;
					x++;
				}
				draw = false;
			} else {
				skiped++;
				if (coinsToSkip == skiped) {
					skiped = 0;
					draw = true;
					coinsToSkip = Random.Range(3, 10);
				}
			}
			y++;
			x++;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
