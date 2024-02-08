using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DRAP.Objectives
{
    public class CollectObjective : BaseObjective
    {
        [SerializeField] Collectible target;

        void Awake() => target.OnCollect += delegate(Collectible c) { Complete(); };
    }
}