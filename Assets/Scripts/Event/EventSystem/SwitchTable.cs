using UnityEngine;

namespace InGame.Event
{
    [CreateAssetMenu(fileName = "SwitchTable", menuName = "EventTable/Switch/Create", order = int.MaxValue)]
    public class SwitchTable : ScriptableObject
    {
        public SwitchID ID;
        public new string name;
        public bool IsOn = false;
    }
}