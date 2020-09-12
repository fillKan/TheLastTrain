using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    [System.Serializable]
    public struct Table
    {
        [Tooltip("현재 자원 수")]
        public uint Now;
        [Tooltip("최대 자원 수")]
        public uint Max;
    }


    [System.Serializable]
    public class ResourceTable
    {
        public Table populationTable;
        public Table foodTable;
        public Table supportResourceTable;
    }

    [System.Serializable]
    public struct GazeTable
    {
        public UnityEngine.UI.Text GazeText;
        public UnityEngine.UI.Image GazeImage;
    }

    public class Resource : MonoBehaviour
    {
        [SerializeField] private ResourceTable InitTable;
        private ResourceTable _resourceTable;
        public ResourceTable GetResourceTable
        {
            get
            {
                return _resourceTable;
            }
        }

        public GazeTable populationUITable;
        public GazeTable FoodUITable;
        public GazeTable SurpportResourceUITable;

        
        public void ApplyPopulation(uint amount = 1)
        {
            _resourceTable.populationTable.Now += amount;
        }

        public void ApplyFood(uint amount = 1)
        {
            _resourceTable.foodTable.Now += amount;
        }

        public void ApplySupportResource(uint amount = 1)
        {
            _resourceTable.supportResourceTable.Now += amount;
        }

        #region Debug Check : Resource
        [ContextMenu("ApplyPopulation")]
        void Population() => ApplyPopulation();

        [ContextMenu("ApplyFood")]
        void Food() => ApplyFood();

        [ContextMenu("ApplySupportResource")]
        void SupportResource() => ApplySupportResource();

        #endregion

        //Convert Percent To Resource Table
        short ConvertPercent(uint now, uint max)
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
            gazeTable.GazeText.text = $"{table.Now}/{table.Max}";
            gazeTable.GazeImage.fillAmount = ConvertPercentToPoint(ConvertPercent(table.Now, table.Max), 2);
        }
        void OnEnable()
        {
            _resourceTable = InitTable;
            StartCoroutine(EResourceProcess());
        }
        void OnDisable()
        {
            StopCoroutine(EResourceProcess());
        }

        public IEnumerator EResourceProcess()
        {
            while (true)
            {
                ApplyResource(populationUITable, GetResourceTable.populationTable);
                ApplyResource(FoodUITable, GetResourceTable.foodTable);
                ApplyResource(SurpportResourceUITable, GetResourceTable.supportResourceTable);
                yield return null;
            }
        }
    }
}
