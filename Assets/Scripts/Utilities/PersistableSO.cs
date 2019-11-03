using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class PersistableSO : MonoBehaviour
{
    [Header ("Meta")]
    public string persisterName;
    [Header ("Scriptable Objects")]
    [SerializeField]
    ScriptableObject[] _objectsToPersist;

    void Awake ()
    {
        DontDestroyOnLoad (gameObject);
    }

    void OnEnable ()
    {
        for (var i = 0; i < _objectsToPersist.Length; i++)
        {
            var psoFile = GetPsoFile (persisterName, i);
            if (File.Exists (psoFile))
            {
                var bf = new BinaryFormatter ();
                using (var file = File.Open (psoFile, FileMode.Open))
                {
                    JsonUtility.FromJsonOverwrite ((string) bf.Deserialize (file), _objectsToPersist[i]);
                }
            }
        }
    }

    void OnDisable ()
    {
        for (var i = 0; i < _objectsToPersist.Length; i++)
        {
            var bf = new BinaryFormatter ();
            using (var file = File.Create (GetPsoFile (persisterName, i)))
            {
                var json = JsonUtility.ToJson (_objectsToPersist[i]);
                bf.Serialize (file, json);
            }
        }
    }

    string GetPsoFile (string persisterName, int indexer)
    {
        return Application.persistentDataPath + string.Format ("/{0}_{1}.pso", persisterName, indexer);
    }
}
