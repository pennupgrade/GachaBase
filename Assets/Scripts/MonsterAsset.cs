using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is a Scriptable Object.
[CreateAssetMenu(fileName = "NewMonsterAsset", menuName = "Monster")]
public class MonsterAsset : ScriptableObject
{
    [SerializeField]
    private string monsterName;

    [SerializeField]
    private string sceneName;

    [SerializeField]
    private Sprite iconSprite;

    public string Name => this.monsterName;

    public string SceneName => this.sceneName;

    public Sprite IconSprite => this.iconSprite;

}
