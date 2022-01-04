using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;

[FirestoreData]
public struct CharacterData
{
    [FirestoreProperty]
    public float points { get; set; }

    [FirestoreProperty]
    public Vector3 position { get; set; }
}
