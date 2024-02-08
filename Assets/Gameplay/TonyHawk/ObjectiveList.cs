using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public interface IObjective
{
    bool IsCompleted();
    System.Action<IObjective> OnCompleted { get; set; }
}

namespace DRAP.Objectives
{

    public class OnAllObjectivesComplete : Signal {}

    public class ObjectiveList : MonoBehaviour
    {
        [Inject] SignalBus signalBus;

        List<BaseObjective> objectives = new List<BaseObjective>();

        void Awake()
        {
            signalBus.Subscribe<OnObjectiveComplete>(Callback_OnObjectiveCompleted);
            GetComponents<BaseObjective>(objectives);
        }

        void Callback_OnObjectiveCompleted(OnObjectiveComplete objective)
        {
            CompleteObjective(objective.Objective);
            if (AllObjectivesAreComplete())
            {
                Debug.Log("All objectives completed.");
                signalBus.Fire<OnAllObjectivesComplete>();
            }
        }

        private bool AllObjectivesAreComplete()
        {
            return objectives.Count == 0;
        }

        private void CompleteObjective(BaseObjective objective)
        {
            Debug.Log("Objective complete: " + objective.ToString());
            objectives.Remove(objective);
        }
    }
}