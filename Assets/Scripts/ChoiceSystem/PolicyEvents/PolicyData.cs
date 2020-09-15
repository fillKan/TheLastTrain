using UnityEngine;
using System;

public class PolicyData : MonoSingleton<PolicyData>
{

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
        int amount = (int)(GameEvent.Instance.InitResourceTable.foodTable.Max * 0.3f);

        GameEvent.Instance.GetResource.ApplyFood(amount);

        GameEvent.Instance.GetResource.ApplyLeaderShip(-4);
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

        GameEvent.Instance.GetResource.ApplyLeaderShip(-5);
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

        GameEvent.Instance.GetResource.ApplyPopulation(-amount);

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
        int population = (int)(GameEvent.Instance.InitResourceTable.populationTable.Max * 0.1f);

        GameEvent.Instance.GetResource.ApplyPopulation(-population * 2);

        GameEvent.Instance.GetResource.ApplyFood(population * 3);
    }
}