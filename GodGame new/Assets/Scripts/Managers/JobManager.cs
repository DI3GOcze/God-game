using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobManager : MonoBehaviour
{
    private static JobManager _instance;

    public static JobManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    
    [Serializable] public class ProffesionsGrownListDictionary : UnitySerializedDictionary<Professions, List<Grown>> { }
    [SerializeField] ProffesionsGrownListDictionary _agentsProfessions;

    // Start is called before the first frame update
    void Start()
    {
        _agentsProfessions = new ProffesionsGrownListDictionary();
    }

    public void AddToList(Grown grown, Professions profession)
    {
        if (_agentsProfessions.ContainsKey(profession))
        {
            _agentsProfessions[profession].Add(grown);
        }
        else
        {
            List<Grown> newList = new List<Grown>();
            newList.Add(grown);

            _agentsProfessions.Add(profession, newList);
        }
    }

}
