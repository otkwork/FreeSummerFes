using System.Transactions;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class ShootingShelf : MonoBehaviour
{
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
					SetObject(i);
				}
			}
		}
	}

	private void SetObject(int index)
	{
		// ������}�X�^�[�f�[�^�Ɋ�Â��ă��f�������ݒ肷��
		// �d��30,50,100
		Loader.LoadGameObjectAsync("Fire Alarm").Completed += op =>
		{
			GameObject obj =Instantiate(op.Result, m_objectsPos[index].position, m_objectsPos[index].rotation, m_objectsParent);
			Rigidbody b = obj.AddComponent<Rigidbody>();
			obj.AddComponent<BoxCollider>();
			ShootingObject a = obj.AddComponent<ShootingObject>();
			a.SetName("Fire Alarm");
			obj.transform.localScale = m_objectsPos[index].localScale * 10;
			b.mass = index <= 10 ? 30 : index <= 12 ? 50 : 100;
			obj.layer = 6;
			m_objects[index] = obj; // ���������I�u�W�F�N�g���L�^
		};
	}
}
