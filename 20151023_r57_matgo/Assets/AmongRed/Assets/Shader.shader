Shader "Custom/mask1"
{
	SubShader
	{
		Tags{"Queue" = "Transparent-1"}

		ZWrite On
		ColorMask 0

		Pass {}

	}
}