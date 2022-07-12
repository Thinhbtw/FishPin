using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBlock : MonoBehaviour
{
    int count;
    [SerializeField] GameObject water;
    [SerializeField] int Quanitty, amountToMelt;
    bool hasAdded;
    private void Awake()
    {
        count = 0;
        hasAdded = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Lava" && !hasAdded)
        {
            count++;
            Destroy(collision.gameObject);
        }

        if(count>= amountToMelt)
        {
            hasAdded = true;
            count = 0;
            this.transform.GetChild(0).gameObject.SetActive(false);
            for (int i = 0; i < Quanitty; i++)
            {
                var newFliud = Instantiate(water, this.transform.position + new Vector3(Random.Range(0f, 0.1f), Random.Range(0f, 0.1f), this.transform.position.z), Quaternion.identity);
                newFliud.transform.parent = this.gameObject.transform;
            }
            
        }

        if (collision.gameObject.tag == "Ground")
            this.gameObject.tag = "Untagged";
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            this.gameObject.tag = "IceBlock";
        }


    }


}
