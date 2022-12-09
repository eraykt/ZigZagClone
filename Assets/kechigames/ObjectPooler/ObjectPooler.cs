using System.Collections.Generic;
using kechigames.Singleton;
using UnityEngine;

namespace kechigames.ObjectPooler
{
    public class ObjectPooler : Singleton<ObjectPooler>
    {
        #region Lists

        public Dictionary<string, Queue<GameObject>> PoolDictionary = new Dictionary<string, Queue<GameObject>>();
        public List<ObjectsToPool> objectsToPool = new List<ObjectsToPool>();
        

        #endregion
        
        private void Start()
        {
            CreatePooledObjects();
        }

        private void CreatePooledObjects()
        {
            foreach (ObjectsToPool objectToPool in objectsToPool)
            {
                GameObject p = new GameObject(objectToPool.objName);
                p.transform.parent = transform;

                Queue<GameObject> objectPool = new Queue<GameObject>();
                for (int i = 0; i < objectToPool.size; i++)
                {
                    GameObject obj = Instantiate(objectToPool.prefab, p.transform);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }

                PoolDictionary.Add(objectToPool.objName, objectPool);
            }
        }

        public GameObject GetObjectFromPool(string objName, Vector3 position, Quaternion rotation)
        {
            if (!PoolDictionary.ContainsKey(objName))
            {
                Debug.LogWarning("Pool with tag " + objName + " doesn't exist.");
                return null;
            }

            GameObject objectToSpawn = PoolDictionary[objName].Dequeue();
            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;

            IPooledObject pooledObject = objectToSpawn.GetComponent<IPooledObject>();

            pooledObject?.OnObjectSpawn();

            return objectToSpawn;
        }

        public void ReturnObjectToPool(GameObject obj)
        {
            obj.SetActive(false);
            PoolDictionary[obj.tag].Enqueue(obj);

            IPooledObject pooledObject = obj.GetComponent<IPooledObject>();
            pooledObject?.OnObjectReturn();
        }
    }


    [System.Serializable]
    public class ObjectsToPool
    {
        public string objName;
        public GameObject prefab;
        public int size;
    }
}