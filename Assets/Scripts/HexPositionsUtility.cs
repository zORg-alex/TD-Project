using UnityEngine;

public static class HexPositionsUtility
{
    const float sin60deg = 0.86602540378f;
    public static Vector2[] FillHex(int count, float edge)
	{
        if (count < 1) return new Vector2[0]; 
        int l = 1;
        int p = 0;
        Vector2 pos = Vector2.left * edge;
        Vector2[] points = new Vector2[count];
		for (int i = 1; i < count; i++)
		{
            points[i] = pos;
			switch (p / l)
			{
                case 0:
                    pos += new Vector2(.5f, sin60deg) * edge;
                    break;
                case 1:
                    pos += Vector2.right * edge;
                    break;
                case 2:
                    pos += new Vector2(.5f, -sin60deg) * edge;
                    break;
                case 3:
                    pos += new Vector2(-.5f, -sin60deg) * edge;
                    break;
                case 4:
                    pos += Vector2.left * edge;
                    break;
                case 5:
                    pos += new Vector2(-.5f, sin60deg) * edge;
                    break;
				default:
					break;
			}
            p++;
            if (p >= l * 6)
            {
                pos += Vector2.left * edge;
                l++;
                p = 0;
            }
		}
        return points;
	}
}