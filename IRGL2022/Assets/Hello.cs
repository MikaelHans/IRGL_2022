using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Hello : MonoBehaviour
{
    // Start is called before the first frame update
    private string filePath = "./soal.txt";
    void Start()
    {
        List<int[]> result = Solve24Game.Generate24();
        StreamWriter writer = new StreamWriter(filePath, false);

        foreach (int[] i in result)
        {
            Debug.Log(result.ToString());
            foreach(int j in i)
            {
                writer.Write(j.ToString());
            }
            writer.WriteLine("\n");
        }
        
        //Debug.Log(Solve24Game.Generate24().ToString());
    }

}
