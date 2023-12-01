using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Scriptable Object/Enemy Data", order = int.MaxValue)]
public class EnemyData : ScriptableObject
{
    public enum Enemies { Archer, Magician, Warrior}
    [SerializeField]
    public Enemies enemy;
    [SerializeField]
    public int hp;
    [SerializeField]
    public float cooltime;
    [SerializeField]
    public int weaponId;
}
