using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Dictionary support Key(string) Value(class)
/// </summary>
/// <typeparam name="T"></typeparam>

[Serializable]
public class SmartDictionary<T> where T : class
{
    public List<string> keys;
    public List<T> values;
  
    public SmartDictionary()
    { 
        keys = new List<string>();
        values = new List<T>();
    }

    public int Count => keys.Count;
    
    /// <summary>
    /// Check if key of element exist in collection
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool ContainsKey( string key )
    {
        return keys.Contains( key );
    }

    /// <summary>
    /// Get index of element in collection by key
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public int IndexOf( string key )
    {
        var id = 0;
        foreach( var k in keys )
        {
            if( k == key ) return id;
            id++;
        }
        
        return -1;
    }

    /// <summary>
    /// Add/Replace element in collection
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void Add( string key, T value )
    {
        if( keys.Contains( key ) )
        {
            var keyId = keys.IndexOf( key );
            if(keyId <= values.Count -1) values[keyId] = value;
            else
            {
                values.Add( value );
                Debug.LogWarning( "SmartDictionary -> _Keys & _Values was not synced - Fixed Now" );
            }
        }
        else
        {
            keys.Add( key );
            var keyId = keys.IndexOf( key );
            values.Add( value );
            if(values.Count - 1 != keyId) Debug.LogWarning( "SmartDictionary -> _Keys & _Values r not synced!" );
        }
    }

    /// <summary>
    /// Get element from collection by key
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public T Get( string key )
    {
        if( keys.Contains( key ) )
        {
            var keyId = keys.IndexOf( key );
            if( keyId <= values.Count - 1 )
            {
                return values[keyId];
            }
        }

        Debug.LogWarning( "SmartDictionary -> _Value is null from Get()" );
        return null;
    }

    /// <summary>
    /// Remove element from collection by key
    /// </summary>
    /// <param name="key"></param>
    public void Remove( string key )
    {
        if (!keys.Contains(key)) return;
        var keyId = keys.IndexOf( key );
        values.RemoveAt( keyId );
        keys.Remove( key );
    }
  
  
  
  
  


}
