using System;
using System.Collections.Generic;

namespace ChessRaid
{
    public class TurnChain
    {
        public Champion Champion;
        public List<TurnEvent> TurnEvents = new();

        public void AddAction(Hex hitHex, ActionType selectedAction)
        {
            TurnEvents.Add(new TurnEvent()
            {
                Action = selectedAction,
                Location = hitHex.Location
            });
        }

        public void RemoveLastActionOrdered()
        {
            if(TurnEvents.Count == 0)
            {
                UnityEngine.Debug.LogWarning($"Calling remove ordered action on {Champion.Id} when no action was ordered");
                return;
            }

            TurnEvents.RemoveAt(TurnEvents.Count - 1);
        }

        public void RemoveTurnChain()
        {
            TurnEvents.Clear();
        }
    }
}