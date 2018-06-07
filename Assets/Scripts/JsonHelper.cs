using System;
using System.Collections.Generic;
using UnityEngine;

public class JsonHelper
{
    //Tool use to use List/Array with json
    //https://forum.unity.com/threads/how-to-load-an-array-with-jsonutility.375735/

    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(List<T> list)
    {
        return ToJson<T>(list.ToArray());
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        var a = JsonUtility.ToJson(wrapper, prettyPrint);
        return a;
    }

    public static string ToJson<T>(List<T> list, bool prettyPrint)
    {
        return ToJson<T>(list.ToArray(), prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}