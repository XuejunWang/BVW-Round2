//Shader 的基本结构
//Shader->Property Definition SubShaders(->Passes) Fallback
//在实际运行中哪一个SubShader被使用是由运行的平台所决定的。每个SubShader中包含一个或者多个Pass，在计算着色时，平台先选择最优先可以使用的着色器，然后依次运行其中的Pass，然后得到输出的结果。最后指定一个Fallback来处理所有SubShader都不能运行的情况。

Shader "Custom/GlowShader" {
	Properties {
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Color ("Color", Color) = (1,1,1,1)           
		_RimColor ("Rim Color", Color) = (1,1,1,1)
		_BumpMap("Normal Map", 2D) = "bump" {}                                      
		_Glossiness ("Smoothness", Range(0,1)) = 0.5    
		_Metallic ("Metallic", Range(0,1)) = 0.0 
		_RimPower ("RimPower", Range(1.0,6.0)) = 3.0

		//_Name("Display Name", type) = defaultValue[{options}]
		//type
		//Color 一种颜色 由RGBA[0,1]定义											  
		//2D 一张2的阶数大小的贴图。这张贴图将在采样后被转为对应基于模型UV的每个像素的颜色，并最终被显示出来
		//Rect 一个非2阶数大小的贴图
		//Cube 立方体纹理(Cube map texture) 6张有联系的2D贴图的组合，主要用来做反射效果(比如天空盒盒动态反射)，也会被转换为对应点的采样
		//Range 一个介于最小值和最大值之间的浮点数，一般用来当作调整shader某些特性的参数。比如透明度渲染的截止值
		//Float 任意一个浮点数
		//Vector 一个四维数
		//defaultValue 对于贴图来说，defaultValue可以为一个代表默认tint颜色的字符串，可以是空字符串或者‘white’‘black’‘gray’‘bump’中的一个。
		//{option} 只对2D，Rect，或者Cube贴图有关 可能的选择有ObjectLinear EyeLinear SphereMap CubeReflect CubeNormal。    (这些都是OpenGl中TexGen的模式) 
	}
	SubShader {
		Tags { "RenderType"="Opaque" }  
		
		//通过这些标签来决定什么时候调用该着色器  -> "RenderType"="Opaque" 渲染非透明物体的时候调用。 Tags暗指Shader输出的是什么 。								
		//其他常用的还有 "IgnoreProjector"="Ture"不被projector影响  "ForceNoShadowCasting"="True"不产生阴影 "Queue"="xxx"指定渲染队列。
		// 预定义的Queue有  
		// Background 最早被调用的渲染，用来渲染天空盒或者背景	  =1000。
		// Geometry 默认值，用来渲染非透明物体 =2000。
		// AlphaTest 用来渲染经过Alpha Test的像素 处于效率考虑单独为其设定一个Queue =2450。
		// Transparent 从后往前的顺序渲染透明物体 =3000。
		// Overlay 用来渲染叠加的效果 渲染的最后阶段 比如镜头光晕等特效 =4000。
		// 设置Queue值的时候可以自己指定，比如"Queue"="Transparent+100"，表示在一个Transparent后100的Queue上调用。
		// 通过调整Queue值可以确保某一些物体一定在一些物体之前或者之后渲染。

		LOD 200  
		// Level of Detail 这个数值决定了能用什么样的shader  在Quality Setting 中可以设定允许的最大LOD，当设定的LOD小于SubShader所指定的LOD的时候，这个LOD将不可用。
		// Unity 内建shader的LOD参考
		// VertexLit 100
		// Decal, Reflective VertexLit = 150
		// Diffuse = 200
		// Diffuse Detail, Reflective Bumped Unlit, Reflective Bumped VertexLit = 250
		// Bumped, Specular = 300
		// Bumped Specular = 400
		// Parallax = 500
		// Parallax Specular = 600
		
		CGPROGRAM  
		//开始标记 表示从这里开始是一段CG程序 (Cg/HLSL语言) 与ENDCG对应
		#pragma surface surf Standard fullforwardshadows  // Physically based Standard lighting model, and enable shadows on all light types
		// #pragma surface surfaceFunction lightModel [optionalparams]

		#pragma target 3.0  // Use shader model 3.0 target, to get nicer looking lighting

		sampler2D _MainTex;  
		//sampler2D  在CG中，sampler2D是和texture所绑定的一个数据容器接口。 
		//加载了以后的texture是一块内存存储的，使用了RGB(A)通道的，每个通道8bits的数据。具体地想知道像素与坐标之间的对应关系，以及获取这些数据，不可能一次次去计算内存地址或者偏移，因此可以通过sampler2D来对贴图进行操作。
		//sampler2D就是GLSL中的2D贴图的类型，相应的还有sampler1D, sampler3D, samplerCube等格式。
		//想要在这段CG代码中使用之前定义的属性，必须使用和之前变量相同的名字进行声明。 这里再次声明并且链接了_MainTex 使得接下来CG可以使用这个变量。
		fixed4 _Color;
		half _Glossiness;   
		//half 半精度浮点数，精度最低，运算性能相对高。
		half _Metallic;
		sampler2D _BumpMap;
		float4 _RimColor;
		float _RimPower;

		struct Input {  
			float2 uv_MainTex;     
			//在贴图变量之前加上uv两个字母，就代表提取它的uv值(贴图上点的二维坐标)。
			float4 color: Color;
			float2 uv_BumpMap;
			float3 viewDir;
		};

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_CBUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_CBUFFER_END

		void surf (Input IN, inout SurfaceOutputStandard o) {  

			//着色器就是给定了输入，然后给出输出进行着色的代码。
			//CG规定了声明为表面着色器的方法的参数类型和名字，第一个参数是一个Input结构，第二个参数是一个inout的SurfaceOutput结构。
			//把所需要参与计算的数据都放到这个Input结构中，传给surf函数。SurfaceOutput 已经定义好了里面类型的输出结构，内容空白，向里面填写输出。
			//在计算输出时，Shader会多次调用surf函数，每次给入一个贴图上的点坐标，来计算输出。
			//SurfaceOutput 的结构如下
			//struct SurfaceOutput {
			//	half3 Albedo;    //颜色
			//	half3 Normal;    //法向值
			//	half3 Emission;  //发散颜色
			//	half Specular;   //镜面高光
			//	half Gloss;      //发光强度
			//	half Alpha;      //透明度
			//}

			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;    //tex2D函数，是CG用来在一张贴图中对一个点进行采样的方法，返回一个float4.
			o.Albedo = c.rgb;   
			o.Alpha = c.a;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;

			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
			half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
			o.Emission = _RimColor.rgb * pow(rim, _RimPower);
		}
		ENDCG
	}
	FallBack "Diffuse"
}
