using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// use File
using System.IO;

public static class Data
{
    public static Dictionary<string, int> unlockedSkills = new Dictionary<string, int>();
    public static int gold;
    public static bool beatenTutorial;
    public static bool beatenLevel1;
    public static bool beatenLevel2;
    public static bool beatenLevel3;
    public static bool beatenLevel4;

    public static void saveData()
    {
        string data = "";
        data += "unlockedSkills:";
        foreach (KeyValuePair<string, int> entry in unlockedSkills)
        {
            data += entry.Key + "," + entry.Value + "|";
        }
        // foreach (string skill in unlockedSkills)
        // {
        //     data += skill + ",";
        // }
        data += ";";
        data += "gold:" + gold + ";";
        data += "beatenTutorial:" + beatenTutorial + ";";
        data += "beatenLevel1:" + beatenLevel1 + ";";
        data += "beatenLevel2:" + beatenLevel2 + ";";
        data += "beatenLevel3:" + beatenLevel3 + ";";
        data += "beatenLevel4:" + beatenLevel4 + ";";
        File.WriteAllText(Application.persistentDataPath + "/data.txt", data);
    }

    public static void loadData()
    {
        if (File.Exists(Application.persistentDataPath + "/data.txt"))
        {
            string data = File.ReadAllText(Application.persistentDataPath + "/data.txt");
            string[] dataSplit = data.Split(';');
            foreach (string dataPiece in dataSplit)
            {
                string[] dataPieceSplit = dataPiece.Split(':');
                if (dataPieceSplit[0] == "unlockedSkills")
                {
                    unlockedSkills = new Dictionary<string, int>();
                    string[] skills = dataPieceSplit[1].Split('|');
                    foreach (string skill in skills)
                    {
                        if (skill != "")
                        {
                            string[] skillSplit = skill.Split(',');
                            unlockedSkills.Add(skillSplit[0], int.Parse(skillSplit[1]));
                        }
                    }
                    // string[] skills = dataPieceSplit[1].Split(',');
                    // foreach (string skill in skills)
                    // {
                    //     if (skill != "")
                    //     {
                    //         unlockedSkills.Add(skill);
                    //     }
                    // }
                }
                else if (dataPieceSplit[0] == "gold")
                {
                    gold = int.Parse(dataPieceSplit[1]);
                }
                else if (dataPieceSplit[0] == "beatenTutorial")
                {
                    beatenTutorial = bool.Parse(dataPieceSplit[1]);
                }
                else if (dataPieceSplit[0] == "beatenLevel1")
                {
                    beatenLevel1 = bool.Parse(dataPieceSplit[1]);
                }
                else if (dataPieceSplit[0] == "beatenLevel2")
                {
                    beatenLevel2 = bool.Parse(dataPieceSplit[1]);
                }
                else if (dataPieceSplit[0] == "beatenLevel3")
                {
                    beatenLevel3 = bool.Parse(dataPieceSplit[1]);
                }
                else if (dataPieceSplit[0] == "beatenLevel4")
                {
                    beatenLevel4 = bool.Parse(dataPieceSplit[1]);
                }
            }
        }
    }
}