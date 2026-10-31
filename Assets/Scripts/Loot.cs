using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LootType{
    weapon,
    chatSkill,
    None
}

[RequireComponent(typeof(Collider2D))]
public class Loot : MonoBehaviour
{
    public LootType lootType = LootType.None;
    public ChatType skill = ChatType.invalid;
    public Weapon weaponLoot;
    // Start is called before the first frame update
    void Start()
    {
        Yoink();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Yoink(){
        // to do: yoink away from origin pos
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player)){
            if(lootType==LootType.weapon)PickWeaponHandle(player);
            if(lootType==LootType.chatSkill)PickChatSkillHandle(player);
        }
    }
    void PickWeaponHandle(Player player){
        HoldingWeapons l = player.lWeapon;
        HoldingWeapons r = player.rWeapon;
        if(l.weapon==null){
            l.weapon = weaponLoot;
            Destroy(gameObject);
        }
        if(r.weapon==null){
            r.weapon = weaponLoot;
            Destroy(gameObject);
        }
        if(GameManager.Instance.controller.attackLeft.isPressed){
            (l.weapon,weaponLoot) = (weaponLoot,l.weapon);
            Yoink();
            return;
        }
        if(GameManager.Instance.controller.attackRight.isPressed){
            (r.weapon,weaponLoot) = (weaponLoot,r.weapon);
            Yoink();
            return;
        }
    }
    void PickChatSkillHandle(Player player){
        if(skill!=ChatType.invalid && GameManager.Instance.availableSkill.Contains(skill))GameManager.Instance.availableSkill.Add(skill);
        Destroy(gameObject);
    }
}
