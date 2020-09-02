using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class readCSV : MonoBehaviour
{
    List<csvLines> pointsCsv = new List<csvLines>();
    int counter = 0;
    float decide = 0;
    float object_scale = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        TextAsset csvData = Resources.Load<TextAsset>("crack_bump_1a");
        string[] data = csvData.text.Split(new char[] { '\n', '\r' });

        for (int i = 1; i < data.Length - 1; i++)
        {
            string[] row = data[i].Split(new char[] { ',' });
            if (row[1] != "")
            {
                csvLines linesCsv = new csvLines();

                int.TryParse(row[0], out linesCsv.GeoX);
                int.TryParse(row[1], out linesCsv.D1);
                int.TryParse(row[2], out linesCsv.D2);
                int.TryParse(row[3], out linesCsv.D3);
                int.TryParse(row[4], out linesCsv.Proxy);
                int.TryParse(row[5], out linesCsv.Direction);
                int.TryParse(row[6], out linesCsv.NoRep);
                int.TryParse(row[7], out linesCsv.CrackType);
                int.TryParse(row[8], out linesCsv.NoExp);
               

                if (true
                    )
                {
                    //sphere.transform.position = new Vector3(linesCsv.PositionSlaveX, linesCsv.PositionSlaveY, linesCsv.PositionSlaveZ);
                    //sphere.transform.position = new Vector3(linesCsv.PositionMasterX, linesCsv.PositionMasterY, linesCsv.PositionMasterZ);
                    //object_touch.transform.position = new Vector3(decide, 0, 0);

                    Vector3 Spawnposition = new Vector3(i, linesCsv.Proxy, 0);
                    float radius = object_scale;

                    if (Physics.CheckSphere(Spawnposition, radius-0.5f))
                    {
                        Debug.Log("Sphere already there");
                    }
                    else
                    {
                        Debug.Log("Sphere!");
                        GameObject object_touch = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        object_touch.transform.position = Spawnposition;
                        object_touch.transform.localScale = new Vector3(object_scale, object_scale, object_scale);

                        Rigidbody gameObjectsRigidBody = object_touch.AddComponent<Rigidbody>();
                        gameObjectsRigidBody.mass = 5f;
                        gameObjectsRigidBody.constraints = RigidbodyConstraints.FreezeAll;
                        gameObjectsRigidBody.useGravity = false;
                        //SphereCollider sphere_collider = object_touch.gameObject.AddComponent<SphereCollider>();
                        //gameObjectsRigidBody.isKinematic = true;
                        decide = decide + object_scale;
                        counter++;
                    }

                    Debug.Log("Sphere" + counter +  " Created");
                }

                pointsCsv.Add(linesCsv);
            }
        }
        //foreach (csvLines linesCsv in pointsCsv)
        //{
        //    Debug.Log(linesCsv.IDmaster);// + "," + q.desc);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}



//public class LoadQuests : MonoBehaviour
//{
//    List<Quest> quests = new List<Quest>();

//    // Use this for initialization
//    void Start()
//    {
//        TextAsset questdata = Resources.Load<TextAsset>("questdata");

//        string[] data = questdata.text.Split(new char[] { '\n' });

//        for (int i = 1; i < data.Length - 1; i++)
//        {
//            string[] row = data[i].Split(new char[] { ',' });

//            if (row[1] != "")
//            {
//                Quest q = new Quest();

//                int.TryParse(row[0], out q.id);
//                q.name = row[1];
//                q.npc = row[2];
//                q.desc = row[3];
//                int.TryParse(row[4], out q.status);
//                q.rewards = row[5];
//                q.task = row[6];
//                int.TryParse(row[7], out q.parent);

//                quests.Add(q);
//            }
//        }

//        foreach (Quest q in quests)
//        {
//            Debug.Log(q.name + "," + q.desc);
//        }
//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }
//}

