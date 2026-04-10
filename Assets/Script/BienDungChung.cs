using TMPro.EditorUtilities;
using UnityEngine;

[CreateAssetMenu(fileName = "BienDungChung", menuName = "Scriptable Objects/BienDungChung")]
public class BienDungChung : ScriptableObject
{
    //Ko sung
    [SerializeField] public AnimationClip idle;
    [SerializeField] public AnimationClip Dibo;
    [SerializeField] public AnimationClip Chay;
    [SerializeField] public AnimationClip Nhay;
    [SerializeField] public AnimationClip Roi;
    [SerializeField] public AnimationClip kick;

    //co sung
    [SerializeField] public AnimationClip idel_gun;
    [SerializeField] public AnimationClip Gun_walk;
    [SerializeField] public AnimationClip Gun_Run;
    [SerializeField] public AnimationClip Gun_Nhay;



}
