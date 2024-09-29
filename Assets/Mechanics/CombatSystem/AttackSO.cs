using UnityEngine;

[CreateAssetMenu(fileName = "AttackSO", menuName = "Game Data/Create new attack")]
public class AttackSO : ScriptableObject
{
    public AnimatorOverrideController animatorOverrideController;
    public float attackSpeed;
}
