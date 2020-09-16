using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InGame.UI.Resource.Control;

/*
 * 
 * Usage
 * 
 *  // Control Population Amount
 * ApplyPopulation(1);
 * ApplyPopulation(-3);
 * 
 *  // Control Food Amount
 * ApplyFood(1);
 * ApplyFood(-2);
 *
 *  // Control SupportResource Amount
 * ApplySupportResource(1);
 * ApplySupportResource(-5);
 * 
 */
namespace InGame.UI.Resource
{
    public class Resource : Iinit
    {
        
        private ResourceTable _resourceTable;
        public ResourceTable GetResourceTable
        {
            get
            {
                return _resourceTable;
            }
        }

        private GameEvent evt;
        //Constructor
        public Resource(GameEvent evt)
        {
            this.evt = evt;
        }

        FoodControl _foodController;
        //override
        public void Initialize()
        {
            _resourceTable = evt.InitResourceTable;

            _foodController = new FoodControl(this);

            evt.SetSixDayEvent(_foodController.FoodBalance);
        }

        public void ApplyPopulation(int amount = 1)
        {
            _resourceTable.populationTable.Now =
            (uint)Mathf.Max(0, _resourceTable.populationTable.Now + amount);
        }

        public void ApplyFood(int amount = 1)
        {
            _resourceTable.foodTable.Now =
            (uint)Mathf.Max(0, _resourceTable.foodTable.Now + amount);
        }

        public void ApplyLeaderShip(int amount = 1)
        {
            _resourceTable.leaderShipTable.Now =
            (uint)Mathf.Max(0, _resourceTable.leaderShipTable.Now + amount);
        }

        public void ApplyMaxPopulation(int amount = 1)
        {
            _resourceTable.populationTable.Max =
            (uint)Mathf.Max(0, _resourceTable.populationTable.Max + amount);
        }

        public void ApplyMaxFood(int amount = 1)
        {
            _resourceTable.foodTable.Max =
            (uint)Mathf.Max(0, _resourceTable.foodTable.Max + amount);
        }

        public void ApplyMaxLeaderShip(int amount = 1)
        {
            _resourceTable.leaderShipTable.Max =
            (uint)Mathf.Max(0, _resourceTable.leaderShipTable.Max + amount);
        }

        #region Debug Check : Resource
        [ContextMenu("ApplyPopulation")]
        void Population() => ApplyPopulation();

        [ContextMenu("ApplyFood")]
        void Food() => ApplyFood();

        [ContextMenu("ApplySupportResource")]
        void SupportResource() => ApplyLeaderShip();

        #endregion

        //Convert Percent To Resource Table
        public short ConvertPercent(uint now, uint max)
        {
            return (short)(((float)now / max) * 100);
        }

        //Convert Percent To Point
        float ConvertPercentToPoint(short percentage ,int pointUnit)
        {
            return (percentage * Mathf.Pow(0.1f, pointUnit));
        }

        // Show Table State In InGame Scene
        void ApplyResource(GazeTable gazeTable, Table table)
        {
            gazeTable.GazeText.text = $"{table.Now} / {table.Max}";
            gazeTable.GazeImage.fillAmount = ConvertPercentToPoint(ConvertPercent(table.Now, table.Max), 2);
        }
        

        public IEnumerator EResourceProcess()
        {
            while (true)
            {
                ApplyResource(evt.PopulationUITable, GetResourceTable.populationTable);
                ApplyResource(evt.FoodUITable, GetResourceTable.foodTable);
                ApplyResource(evt.LeaderShipUITable, GetResourceTable.leaderShipTable);
                yield return null;
            }
        }
    }
}
