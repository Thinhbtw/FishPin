using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class achivementManager : MonoBehaviour
{
    public static achivementManager instance;

    public List<string> title = new List<string>();
    public List<int> presentCode = new List<int>();
    [SerializeField] GameObject achivePref;
    [SerializeField] GameObject general;
    public List<GameObject> achivements = new List<GameObject>();
    readTXTFile reader = new readTXTFile();

    Dictionary<string, string> achiveAmount = new Dictionary<string, string>();

    [SerializeField] Button btnBack;

    private void Awake()
    {
        instance = (achivementManager)this;
    }

    void Start()
    {
        reader.Get("archiveList");
        title = reader.getString();
        presentCode = reader.getInt();
        achiveAmount = reader.getAmount();
        for(int i = 0; i < title.Count; i++)
        {
            GameObject achive =  Instantiate(achivePref, general.transform);
            setAchive(achive, i);            
            achivements.Add(achive);            
        }

        
    }
    
    public void setAchive(GameObject achive, int i)
    {

        /*achivComp = new archivement(title[i], presentCode[i], getAmount(i));*/
        achive.GetComponent<archivement>().archivementTitle = title[i];
        achive.GetComponent<archivement>().titleSet(title[i]);
        achive.GetComponent<archivement>().achiveCode = presentCode[i];
        achive.GetComponent<archivement>().achiveAmount = getAmountbyId(i);
        achive.GetComponent<archivement>().achiveIndex = achivements.Count;        
    }
     
    string getAmountbyId(int i)
    {
        string temp;
        if (achiveAmount.TryGetValue("achive" + i, out temp))
        {
            temp = achiveAmount["achive" + i];
            Debug.Log("temp "+i+": "+ temp);
        }
        else
        {
            Debug.Log("deo co temp");
            temp = null;
        }
        return temp;
    }

    private void OnEnable()
    {
        if (btnBack != null)
            btnBack.onClick.AddListener(() =>
            {
                if (UIManager.Instance.list.Count == 2)
                {
                    UIManager.Instance.LoadPreviousDialog();
                    UIHome.Instance.gameObject.SetActive(true);
                }
            });
    }

}
