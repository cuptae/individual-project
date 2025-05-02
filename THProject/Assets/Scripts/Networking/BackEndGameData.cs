using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using System.Text; //뒤끝 SDK를 사용하기 위한 네임스페이스


public class UserData
{
    public int level = 1;
    public float atk = 3.5f;
    public string info = string.Empty;
    public Dictionary<string, int> inventory = new Dictionary<string, int>();
    public List<string> equipment = new List<string>();

    // 데이터를 디버깅하기 위한 함수입니다.(Debug.Log(UserData);)
    public override string ToString()
    {
        StringBuilder result = new StringBuilder();
        result.AppendLine($"level : {level}");
        result.AppendLine($"atk : {atk}");
        result.AppendLine($"info : {info}");

        result.AppendLine($"inventory");
        foreach (var itemKey in inventory.Keys)
        {
            result.AppendLine($"| {itemKey} : {inventory[itemKey]}개");
        }

        result.AppendLine($"equipment");
        foreach (var equip in equipment)
        {
            result.AppendLine($"| {equip}");
        }

        return result.ToString();
    }

}

public class BackEndGameData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
