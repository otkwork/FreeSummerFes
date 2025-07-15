using System.Transactions;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class ShootingShelf : MonoBehaviour
{
	[SerializeField] private ExcelData m_excelData;
	[SerializeField] private Transform[] m_objectsPos;
	[SerializeField] private Transform m_objectsParent;
	private static GameObject[] m_objects;
	private (int, int) m_prevChangeDay = (0, 0);

	private const int NewObjectSetTime = 3;

	private void Awake()
	{
		m_objects = new GameObject[m_objectsPos.Length];
		for (int i = 0; i < m_objectsPos.Length; ++i)
		{
			m_objectsPos[i] = transform.GetChild(i);
		}
	}

	private void Update()
	{
		if (WorldTime.GetWorldTime().Item1 >= NewObjectSetTime && WorldTime.GetWorldDay() != m_prevChangeDay)
		{
			m_prevChangeDay = WorldTime.GetWorldDay();
			for (int i = 0; i < m_objectsPos.Length; ++i)
			{
				if (m_objects[i] == null)
				{
					int objectIndex = Random.Range(0, m_excelData.Object.Count - 1);
					SetObject(i, objectIndex);
				}
			}
		}
	}

	private void SetObject(int index, int objectIndex)
	{
		// いずれマスターデータに基づいてモデルや情報を設定する
		// 重さ30,50,100
		Loader.LoadGameObjectAsync(m_excelData.Object[objectIndex].objectName).Completed += op =>
		{
			GameObject obj =Instantiate(op.Result, m_objectsPos[index].position, m_objectsPos[index].rotation, m_objectsParent);
			obj.GetComponent<Rigidbody>().mass = index <= 10 ? 30 : index <= 12 ? 50 : 100;
			obj.GetComponent<ShootingObject>().SetName(m_excelData.Object[objectIndex].displayName);
			obj.transform.localScale = Vector3.Scale(obj.transform.localScale, m_objectsPos[index].localScale);
			m_objects[index] = obj; // 生成したオブジェクトを記録
		};
	}
}
