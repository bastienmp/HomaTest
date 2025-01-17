﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreUI : MonoBehaviour
{
    [SerializeField]
    UIStoreItem itemUIPrefab;
    void Start()
    {
        Debug.Log("storeui : " + Store.Instance.StoreItems.Count);
        foreach (StoreItem item in Store.Instance.StoreItems)
        {
            UIStoreItem listItem = Instantiate(itemUIPrefab, transform);
            listItem.Initialize(item);
        }
    }
}
