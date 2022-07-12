using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int Quantity;
    [SerializeField] GameObject prefabs;
    void Start()
    {
        for (int i = 0; i < Quantity; i++)
        {
            var newFliud = Instantiate(prefabs, this.transform.position + new Vector3(Random.Range(0.2f, 0.5f), Random.Range(-0f, 0.5f), this.transform.position.z), Quaternion.identity);
            newFliud.transform.parent = this.gameObject.transform;
        }
    }

}
