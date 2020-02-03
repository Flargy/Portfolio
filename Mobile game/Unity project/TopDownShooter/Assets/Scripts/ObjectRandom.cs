using UnityEngine;


public class ObjectRandom : MonoBehaviour {

    public GameObject[] objects = new GameObject[3];
    public GameObject oildrum;
    public GameObject teslaCoil;
    public GameObject reactor;
    private int amountOfTesla = 0, amountOfReactor = 0;
    private float boxScale, randXPos, randYPos;

    private int spawnChance = 35;

	// Use this for initialization
	void Start () {
        int spawnCheck = (int)Random.Range(0,100);
        int objectSpawn = (int)Random.Range(0, 3);
        //boxScale = Random.Range(0.0f,0.3f);
        //box.transform.localScale = new Vector3(0.1f, 0.1f, 0f);
        randXPos = Random.Range(0.0f, 2.0f) - 1.0f;
        randYPos = Random.Range(0.0f, 2.0f) - 1.0f;
        Vector3 position = new Vector3(this.transform.position.x + randXPos, this.transform.position.y + randYPos, this.transform.position.z);

        if (spawnCheck <= spawnChance)
        {
            if(objectSpawn == 0)
            {
                //Debug.Log(boxScale);
                
                //box.transform.localScale += new Vector3(boxScale, boxScale, 0f);
                Instantiate(oildrum, position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0, 359)));
            }
            else if(objectSpawn == 1 && amountOfReactor < 3)
            {
                amountOfReactor++;
                Instantiate(reactor, position, Quaternion.identity);
            }
            else
            {
                if(amountOfTesla < 3)
                {
                    amountOfTesla++;
                    Instantiate(teslaCoil, position, Quaternion.identity);
                }
                else
                {
                    Instantiate(oildrum, position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0, 359)));
                }

            }

        }

    }
}
