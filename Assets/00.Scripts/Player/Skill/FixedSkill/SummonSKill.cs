using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonSKill : SkillBase
{
    //����,��ź,����ü,����
    public GameObject pref;
    public override void SkillMing(SkillData skillData)
    {
        if (GameMana.instance.songMana.power[2] > 0.1f)
        {
            Instantiate(pref, skillData.trm.position, skillData.trm.rotation);
        }
    }
}
