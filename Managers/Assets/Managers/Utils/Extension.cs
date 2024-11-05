using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public static class Extension
{
    public static T GetOrAddComponent<T>(this GameObject go) where T : Component
    {
        return Utils.GetOrAddComponent<T>(go);
    }

    public static void BindEvent(this GameObject go, Action<PointerEventData> action = null, Define.ETouchEvent type = Define.ETouchEvent.Click)
    {
        UI_Base.BindEvent(go, action, type);
    }


    public static void DestroyChildren(this GameObject go)
    {
        DestroyChildren(go.transform);
    }

	public static void DestroyChildren(this Transform t)
	{
		foreach (Transform child in t)
		{
			Managers.Resource.Destroy(child.gameObject);
		}
	}

}
