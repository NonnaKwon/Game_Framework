
using System;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Analytics;
using Object = UnityEngine.Object;

public static class Utils
{
    public static long TickCount { get { return (long)(Time.time * 1000); } }

    public static T GetOrAddComponent<T>(GameObject go) where T : Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();
        return component;
    }

    public static T FindAncestor<T>(GameObject go) where T : Object
    {
        Transform t = go.transform;
        while (t != null)
        {
            T component = t.GetComponent<T>();
            if (component != null)
                return component;
            t = t.parent;
        }
        return null;
    }

    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform == null)
            return null;

        return transform.gameObject;
    }

    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : Object
    {
        if (go == null)
            return null;

        if (recursive == false)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }

        return null;
    }

    public static Color HexToColor(string color)
    {
        Color parsedColor;

        if (color.Contains("#") == false)
            ColorUtility.TryParseHtmlString("#" + color, out parsedColor);
        else
            ColorUtility.TryParseHtmlString(color, out parsedColor);

        return parsedColor;
    }


    public static T ParseEnum<T>(string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }
}