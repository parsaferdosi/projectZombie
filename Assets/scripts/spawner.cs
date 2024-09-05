using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    [SerializeField] GameObject _zombie;
    public int zombieCount;
    public float counter = 3f;
    // Start is called before the first frame update
    void Start()
    {
        int i;
        for (i = 0; i < zombieCount; i++)
        {
            Spawner();
        }
    }
    void Update()
    {
     
            int j;
            bool exsist = existent();
            if (exsist)
            {
                zombieCount++;
                for (j = 0; j < zombieCount; j++)
                {
                    Spawner();
                }

            }
     
    }

    private bool existent()
    {

        if (!(GameObject.FindWithTag("Zombie")))
        {
            counter -= Time.deltaTime;
            if (counter <= 0)
            {
                counter = 3f;
                return true;
            }
            else { return false; }
        }
        else { return false; }
    }
   
    private void Spawner()
    {
        //the only bad thing is you should change the range in every scense
        Instantiate(_zombie, new Vector3(Random.Range(-128f, 128f), 2f,Random.Range(-128f,128f)),Quaternion.identity);
    }
}
