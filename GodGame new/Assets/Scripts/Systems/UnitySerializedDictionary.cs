using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Type of dictionary that can be serializable
/// Inspired by https://forum.unity.com/threads/finally-a-serializable-dictionary-for-unity-extracted-from-system-collections-generic.335797/
/// </summary>
public class UnitySerializedDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
	[SerializeField] List<TKey> _keyData = new List<TKey>();
	[SerializeField] List<TValue> _valueData = new List<TValue>();

    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {
		this.Clear();
		for (int i = 0; i < this._keyData.Count && i < this._valueData.Count; i++)
		{
			this[this._keyData[i]] = this._valueData[i];
		}
    }

    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {
		this._keyData.Clear();
		this._valueData.Clear();

		foreach (var item in this)
		{
			this._keyData.Add(item.Key);
			this._valueData.Add(item.Value);
		}
    }
}