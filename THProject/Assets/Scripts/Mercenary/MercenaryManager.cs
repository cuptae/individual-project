using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MercenaryManager : MonoSingleton<MercenaryManager>
{
    public List<Mercenary> mercenaryList = new List<Mercenary>(); // 병사 리스트
}
