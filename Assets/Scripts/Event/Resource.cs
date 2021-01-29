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

            evt.SetElevenDayEvent(_foodController.FoodBalance);
            InGame.Bubble.BubbleSystem.Instance.OnClickBubbleAny += () =>
            {
                ApplyResource(evt.PopulationUITable, GetResourceTable.populationTable);
                ApplyResource(evt.FoodUITable, GetResourceTable.foodTable);
                ApplyResource(evt.LeaderShipUITable, GetResourceTable.leaderShipTable);
            };
            InGame.Bubble.BubbleSystem.Instance.OnClickBubbleAny?.Invoke();
        }

        public void ApplyPopulation(int amount = 1)
        {
            _resourceTable.populationTable.Now =
            (uint)Mathf.Max(0, _resourceTable.populationTable.Now + amount);
            evt.popupSystem.SpawnPopup((amount >= 0) ? "+"
                + amount.ToString() : amount.ToString(), ResourceType.Population);
        }

        public void ApplyFood(int amount = 1)
        {
            _resourceTable.foodTable.Now =
            (uint)Mathf.Max(0, _resourceTable.foodTable.Now + amount);
            evt.popupSystem.SpawnPopup((amount >= 0) ? "+"
                + amount.ToString() : amount.ToString(), ResourceType.Food);
        }

        public void ApplyLeaderShip(int amount = 1)
        {
            if (_resourceTable.leaderShipTable.Now + amount > _resourceTable.leaderShipTable.Max)
                return;

            _resourceTable.leaderShipTable.Now =
            (uint)Mathf.Max(0, _resourceTable.leaderShipTable.Now + amount);
            evt.popupSystem.SpawnPopup((amount >= 0) ? "+" 
                + amount.ToString() : amount.ToString(), ResourceType.LeaderShip);
        }

        public void ApplyMaxPopulation(int amount = 1)
        {
            _resourceTable.populationTable.Max =
            (uint)Mathf.Max(0, _resourceTable.populationTable.Max + amount);
            evt.popupSystem.SpawnPopup((amount >= 0) ? "+"
                + amount.ToString() : amount.ToString(), ResourceType.Population);
        }

        public void ApplyMaxFood(int amount = 1)
        {
            _resourceTable.foodTable.Max =
            (uint)Mathf.Max(0, _resourceTable.foodTable.Max + amount);
            evt.popupSystem.SpawnPopup((amount >= 0) ? "+" 
                + amount.ToString() : amount.ToString(), ResourceType.Food);
        }

        public void ApplyMaxLeaderShip(int amount = 1)
        {
            _resourceTable.leaderShipTable.Max =
            (uint)Mathf.Max(0, _resourceTable.leaderShipTable.Max + amount);
            evt.popupSystem.SpawnPopup((amount >= 0) ? "+"
                + amount.ToString() : amount.ToString(), ResourceType.LeaderShip);
        }

        #region Debug Check : Resource
        [ContextMenu("ApplyPopulation")]
        void Population() => ApplyPopulation();

        [ContextMenu("ApplyFood")]
        void Food() => ApplyFood();

        [ContextMenu("ApplySupportResource")]
        void SupportResource() => ApplyLeaderShip();

        #endregion


        public double GetFoodResource(double offset)
        {
            return (double)GetResourceTable.foodTable.Max * offset;
        }
        public double GetPopulationResource(double offset)
        {
            return (double)GetResourceTable.populationTable.Max * offset;
        }
        public double GetLeaderShipResource(double offset)
        {
            return (double)GetResourceTable.leaderShipTable.Max * offset;
        }

        

        // Show Table State In InGame Scene
        void ApplyResource(GazeTable gazeTable, Table table)
        {
            if (table.Now == table.Max)
                gazeTable.GazeText.color = Color.yellow;
            else if (table.Now > table.Max)
                gazeTable.GazeText.color = new Color32(255, 161, 161, 255);
            else gazeTable.GazeText.color = Color.white;
            gazeTable.GazeText.text = $"{table.Now} / {table.Max}";
            gazeTable.GazeImage.fillAmount = Utils.ConvertPercentToPoint(Utils.ConvertPercent(table.Now, table.Max), 2);
        }
        

        public IEnumerator EResourceProcess()
        {
            //while (true)
            //{
            //    ApplyResource(evt.PopulationUITable, GetResourceTable.populationTable);
            //    ApplyResource(evt.FoodUITable, GetResourceTable.foodTable);
            //    ApplyResource(evt.LeaderShipUITable, GetResourceTable.leaderShipTable);
            //    yield return null;
            //}
            yield return null;
        }
    }
}
