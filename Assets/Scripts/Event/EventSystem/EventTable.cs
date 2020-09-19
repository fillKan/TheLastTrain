using UnityEngine;
using UnityEngine.Events;

namespace InGame.Event
{
    [CreateAssetMenu(fileName = "EventTable", menuName = "EventTable/Create", order = int.MaxValue)]
    public class EventTable : ScriptableObject
    {
        public EventID ID;
        public bool IsEventOn;
        public TAG tagType = TAG.NORMAL;
        public new string name;

        public Sprite icon;
        [TextArea]
        public string description;


        public bool _nextEvent = true;
    }
}
