//using UnityEngine;
//using System.Collections;
using System.Collections.Generic;

public class uSVGTransformList {
	private List<uSVGTransform> m_listTransform;
	//private ulong m_numberOfItems;
	/*********************************************************************************************/
	public int numberOfItems {
		get{return this.m_listTransform.Count;}
	}
	public uSVGMatrix totalMatrix {
		get {
			if (numberOfItems == 0) {
				return new uSVGMatrix(1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f);
			} else {
				uSVGMatrix matrix = GetItem(0).matrix;
				for (int i = 1; i < numberOfItems; i++ ) {
					matrix = matrix.Multiply(GetItem(i).matrix);
				}
				return matrix;
			}
		}
	}
	/*********************************************************************************************/
	public uSVGTransformList() {
		m_listTransform = new List<uSVGTransform>();
	}
	public uSVGTransformList(string listString) {
		m_listTransform = new List<uSVGTransform>();
		
		Dictionary<string, string> transformList = uSVGStringExtractor.f_ExtractTransformList(listString);
		//this.m_numberOfItems = (ulong) transformList.Count;
		foreach (KeyValuePair<string, string> kvp in transformList) {
			AppendItem(new uSVGTransform(kvp.Key, kvp.Value));
		}
	}
	
	/*********************************************************************************************/
	public uSVGTransformList AppendItem(uSVGTransform newItem) {
		m_listTransform.Add(newItem);
		return this;
	}
	public uSVGTransformList AppendItems(uSVGTransformList newListItem) {
		for(int i = 0; i < newListItem.numberOfItems; i++) {
			uSVGTransform temp = newListItem.GetItem(i);
			m_listTransform.Add(temp);
		}
		return this;
	}
	public uSVGTransform GetItem(int index) {
		
		if ((index < 0) || (index >= numberOfItems)) {
			throw new uDOMException(uDOMExceptionType.IndexSizeErr);
		}
		return this.m_listTransform[index];
	}

	public void InsertItemBefore(uSVGTransform newItem, int index) {
		this.m_listTransform.Insert(index, newItem);
		//return newItem;
	}

	public void ReplaceItem(uSVGTransform newItem, int index) {
		this.m_listTransform[index] = newItem;
		//return newItem;
	}

	public uSVGTransform CreateSVGTransformFromMatrix(uSVGMatrix matrix) {
		return new uSVGTransform(matrix);
	}
	public uSVGTransform Consolidate() {
		uSVGTransform result = CreateSVGTransformFromMatrix(totalMatrix);
		return result;
	}
}