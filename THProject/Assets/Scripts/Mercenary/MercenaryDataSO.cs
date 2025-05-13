using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mercenary", menuName = "GameData/MercenaryData")]
public class MercenaryDataSO : ScriptableObject
{
    public Sprite mercenaryPortrait;
    public string mercenaryName;
    public int maxHp;
    public int attackPower;
    public int moverange;
    public int attackRange;
}
