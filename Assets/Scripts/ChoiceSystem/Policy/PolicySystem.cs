using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public enum Policy
{
    None,
    /// <summary>
    /// 포교
    /// </summary>
    MissionaryWork,
    /// <summary>
    /// 식량 절약
    /// </summary>
    FoodSaving,
    /// <summary>
    /// 인구 감축
    /// </summary>
    PopulationDownSize,
    /// <summary>
    /// 의료 산업
    /// </summary>
    MedicalIndustry,
    /// <summary>
    /// 야간 노동
    /// </summary>
    ExtraWork
}


public class PolicySystem : Singleton<PolicySystem>
{
    [Header("Policy ICon Objects")]
    [SerializeField] private PolicyIcon[] mPolicyIcons;
    
    [Header("Policy ICon Images")]
    [SerializeField] private Sprite IConMissionaryWork;
    [SerializeField] private Sprite IConFoodSaving;
    [SerializeField] private Sprite IConPopulationDownSize;
    [SerializeField] private Sprite IConMedicalIndustry;
    [SerializeField] private Sprite IConExtraWork;

    private int MIndex;
    private int RIndex
    { get => (MIndex + 1) > 2 ? 0 : (MIndex + 1); }
    private int LIndex
    { get => (MIndex - 1) < 0 ? 2 : (MIndex - 1); }

    private Dictionary<Policy, IEnforcementable> mPolicy;
    public Dictionary<Policy, IEnforcementable> GetPolicy 
    {
        get
        {
            if (mPolicy != null)
                return mPolicy;
            return null;
        }
    }
    private List<Policy> mAccumulatePolicy;
    public List<Policy> GetAccumulatePolicy
    {
        get
        {
            if (mAccumulatePolicy != null)
                return mAccumulatePolicy;
            return null;
        }
    }
    public bool IsExistAccumulatePolicy(Policy policy) => GetAccumulatePolicy.Contains(policy);
    public bool RemoveAccumulatePolicy(Policy policy) => GetAccumulatePolicy.Remove(policy);

    private void Awake()
    {
        mAccumulatePolicy = new List<Policy>();

        MIndex = 1;

        mPolicy = new Dictionary<Policy, IEnforcementable>();

        mPolicy.Add(Policy.MissionaryWork,
                       new MissionaryWork());

        mPolicy.Add(Policy.FoodSaving,
                       new FoodSaving());

        mPolicy.Add(Policy.PopulationDownSize,
                       new PopulationDownSize());

        mPolicy.Add(Policy.MedicalIndustry,
                       new MedicalIndustry());

        mPolicy.Add(Policy.ExtraWork,
                       new ExtraWork());
    }

    public Sprite GetPolicyICon(Policy policy)
    {
        switch (policy)
        {
            case Policy.MissionaryWork:
                return IConMissionaryWork;

            case Policy.FoodSaving:
                return IConFoodSaving;

            case Policy.PopulationDownSize:
                return IConPopulationDownSize;

            case Policy.MedicalIndustry:
                return IConMedicalIndustry;

            case Policy.ExtraWork:
                return IConExtraWork;
        }
        return null;
    }

    public void Enforce(Policy policy)
    {
        if (mPolicy.ContainsKey(policy)) 
        {
            mPolicy[policy].Enforce();

            AddEnforcementPolicy(policy);

            if (!IsExistAccumulatePolicy(policy))
                GetAccumulatePolicy.Add(policy);
        }
    }
    private void AddEnforcementPolicy(Policy policy)
    {
        mPolicyIcons[RIndex].SetAniStste(PolicyAniState.LeftMove);
        mPolicyIcons[LIndex].SetAniStste(PolicyAniState.OnPolicy);
        mPolicyIcons[MIndex].SetAniStste(PolicyAniState.DisPolicy);

        mPolicyIcons[LIndex].SetSprite(GetPolicyICon(policy));

        MIndex = (MIndex + 1) > 2 ? 0 : (MIndex + 1);
    }
}
public interface IEnforcementable
{
    Policy GetPolicy
    {
        get;
    }
    void Enforce();
}

public class MissionaryWork : IEnforcementable
{
    public Policy GetPolicy
    {
        get => Policy.MissionaryWork;
    }
    public void Enforce()
    {
        GameEvent.Instance.GetResource.ApplyLeaderShip(3);
    }
}

public class FoodSaving : IEnforcementable
{
    public Policy GetPolicy
    {
        get => Policy.FoodSaving;
    }
    public void Enforce()
    {
        int amount = (int)(GameEvent.Instance.InitResourceTable.foodTable.Max * 0.2f);

        GameEvent.Instance.GetResource.ApplyFood(amount);

        GameEvent.Instance.GetResource.ApplyLeaderShip(-2);
    }
}
public class PopulationDownSize : IEnforcementable
{
    public Policy GetPolicy
    {
        get => Policy.PopulationDownSize;
    }
    public void Enforce()
    {
        int amount = (int)(GameEvent.Instance.InitResourceTable.populationTable.Max * 0.4f);

        GameEvent.Instance.GetResource.ApplyPopulation(-amount);

        GameEvent.Instance.GetResource.ApplyLeaderShip(-3);
    }
}

public class MedicalIndustry : IEnforcementable
{
    public Policy GetPolicy
    {
        get => Policy.MedicalIndustry;
    }
    public void Enforce()
    {
        int amount = (int)(GameEvent.Instance.InitResourceTable.foodTable.Max * 0.3f);

        GameEvent.Instance.GetResource.ApplyFood(-amount);

        GameEvent.Instance.GetResource.ApplyLeaderShip(5);
    }
}
public class ExtraWork : IEnforcementable
{
    public Policy GetPolicy
    {
        get => Policy.ExtraWork;
    }
    public void Enforce()
    {
        int population = (int)(GameEvent.Instance.InitResourceTable.populationTable.Max * 0.2f);

        GameEvent.Instance.GetResource.ApplyPopulation(-population);

        GameEvent.Instance.GetResource.ApplyFood(population);
    }
}