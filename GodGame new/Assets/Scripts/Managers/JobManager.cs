using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Finally wasnt used nor completed
/// </summary>
public class JobManager : MonoBehaviour
{
    private static JobManager _instance;
    public static JobManager Instance { get { return _instance; } }
    [Serializable] public class ProffesionsGrownListDictionary : UnitySerializedDictionary<Professions, List<Grown>> { }
    [SerializeField] ProffesionsGrownListDictionary _agentsProfessions;

    private void Awake()
    {
        // Singleton...
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

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
