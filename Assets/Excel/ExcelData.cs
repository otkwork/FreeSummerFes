using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class ExcelData : ScriptableObject
{
	public List<GunDataEntity> Gun;
	public List<ShootingObjectEntity> Object;
}
