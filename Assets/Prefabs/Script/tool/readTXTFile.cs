using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class readTXTFile
{
    List<string> strings = new List<string>();
    List<int> number = new List<int>();
    Dictionary<string, string> achiveAmount = new Dictionary<string, string>();

    private static char[] cutter = new char[]
    {
        '$'
    };    
    public void Get(string fileName)
    {
        Debug.Log("readingData");
        StreamReader reader = new StreamReader(Application.dataPath + "/Resources/AchivementData/" + fileName + ".txt");
        while (!reader.EndOfStream)
        {
            string[] lines = reader.ReadLine().Split(cutter);
            if (lines != null)
            {               
                strings.Add(lines[0]);
                number.Add(int.Parse(lines[1]));
                if(lines.Length > 2)
                {
                    achiveAmount.Add("achive" + (strings.Count-1), lines[2].Trim());
                    
                }

            }            
            
        }        
        reader.Close();        
    }
    public List<string> getString()
    {
        return strings;
    }
    public List<int> getInt()
    {
        return number;
    }
    public Dictionary<string,string> getAmount()
    {
        return achiveAmount;
    }
}
