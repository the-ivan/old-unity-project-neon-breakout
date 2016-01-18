using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Levels : MonoBehaviour {
	// Pull from saved files for premade/custom levels
    // Level name is #.lvl for premade and 'name_custom.lvl for custom

    private int[] level_array = new int[400];
    private int[,] loaded_grid = new int[20, 20];

    public void SaveGrid(string level, int[,] grid)
    {
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                level_array[j * 20 + i] = loaded_grid[i, j];
            }
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + level + ".lvl");
        bf.Serialize(file, level_array);
        file.Close();
        Debug.Log(Application.persistentDataPath);
    }

    public int[,] LoadGrid (int level)
    {
        if (File.Exists(Application.persistentDataPath + "/" + level + ".lvl"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + level + ".lvl", FileMode.Open);
            level_array = (int[])bf.Deserialize(file);
            file.Close();
        }

        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                loaded_grid[i, j] = level_array[j * 20 + i];
            }
        }
        return loaded_grid;
    }
}