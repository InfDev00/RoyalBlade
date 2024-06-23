using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Utils
{
    public static class Util
    {
        public static T GetOrAddComponent<T>(this GameObject obj) where T : Component
        {
            var comp = obj.GetComponent<T>();
            if (comp == null) comp = obj.AddComponent<T>();

            return comp;
        }

        public static void AddEventTriggerListener(this GameObject obj, EventTriggerType eventType, UnityAction<BaseEventData> action)
        {
            var trigger = obj.GetOrAddComponent<EventTrigger>();

            var entry = new EventTrigger.Entry{eventID = eventType};
            entry.callback.AddListener(action);
            
            trigger.triggers.Add(entry);
        }
    }
}