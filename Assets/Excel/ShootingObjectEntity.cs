[System.Serializable]
public class ShootingObjectEntity
{
	public int id;					// ID
	public string objectName;		// 画像、モデルと合わせるための名前
	public string displayName;      // 表示名
	public int maxPrice;            // 最大価格 ※100%の確率で売れない
	public int minPrice;			// 最低価格 ※100%の確率で売れる
}
