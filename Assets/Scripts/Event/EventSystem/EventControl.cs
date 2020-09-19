using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Event
{
    public enum TAG
    {
        NORMAL,
        RESOURCE,
        SPECIAL,
    }
    public class EventControl : Iinit
    {
        private GameEvent evt;
        private EventEffect evtEffect;

        //Constructor
        public EventControl(GameEvent evt, EventEffect evtEffect)
        {
            this.evt = evt;
            this.evtEffect = evtEffect;
        }

        public void Initialize()
        {
            evt.SubscribeDayEvent(OnEventListener);
        }
        public void OnEventListener()
        {
            //evtEffect.PrintAllTable();
            if(evtEffect != null)
            {
                evtEffect.ApplyEffect();
                SoundManager.Instance.PlayOneShot("EventUpAlarm");
            }
        }
    }
}