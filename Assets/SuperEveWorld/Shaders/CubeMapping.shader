Shader "GLSL Cube Environmnent Mapping" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_SpecColor ("Specular Color", Color) = (0.5,0.5,0.5,1)
		_Shininess ("Shininess", Range (0.01, 1)) = 0.07
		_ReflectColor ("Reflection Color", Color) = (1,1,1,0.5)
		_BumpMap ("Normalmap", 2D) = "bump" {}
		_Cube("Reflection Map", Cube) = "" {}
	}
   	SubShader {
    	Pass {   
	        GLSLPROGRAM
	 
	        uniform samplerCube _Cube;   
	        uniform sampler2D _BumpMap;
	        uniform vec4 _BumpMap_ST;
	      	uniform vec4 _Color;
	      	uniform vec4 _SpecColor;
	      	uniform vec4 _ReflectColor;
	      	uniform float _Shininess;
	 
	        uniform vec3 _WorldSpaceCameraPos; 
	        uniform vec4 _WorldSpaceLightPos0;
	        uniform vec4 _LightColor0;

	        uniform mat4 _Object2World; // model matrix
	        uniform mat4 _World2Object; // inverse model matrix
	          
	        varying vec3 normalDirection;
	        varying vec3 viewDirection;
	        varying vec4 position;
	        varying vec4 textureCoordinates;
	        varying mat3 TBNMatrix;
	 
	        #ifdef VERTEX
	 
	 		attribute vec4 Tangent;
	 
	        void main()
	        {            
	        	mat4 modelMatrix = _Object2World;
	            mat4 modelMatrixInverse = _World2Object;
	 
	 			TBNMatrix[0] = normalize(vec3(modelMatrix * vec4(vec3(Tangent), 0.0)));
	 			TBNMatrix[2] = normalize(vec3(vec4(gl_Normal, 0.0) * modelMatrixInverse));
	 			TBNMatrix[1] = normalize(cross(TBNMatrix[2], TBNMatrix[0]) * Tangent.w);
	 	
	 			position = modelMatrix * gl_Vertex;
	 			textureCoordinates = gl_MultiTexCoord0;
	 
	            gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
	        }
 
	        #endif
	 
	 
	        #ifdef FRAGMENT
	 
	        void main()
	        {
		        vec4 encodedNormal = texture2D(_BumpMap, 
	               							   _BumpMap_ST.xy * textureCoordinates.xy + _BumpMap_ST.zw);
	            vec3 localCoords = vec3(2.0 * encodedNormal.ag - vec2(1.0), 0.0);
	            localCoords.z = sqrt(1.0 - dot(localCoords, localCoords));

	            vec3 normalDirection = 
	               normalize(TBNMatrix * localCoords);
	 
	            vec3 viewDirection = 
	               normalize(_WorldSpaceCameraPos - vec3(position));
	        
				vec3 lightDir;
				float attenuation;
				
				if(_WorldSpaceLightPos0.w == 0.0) {
					attenuation = 1.0;
					lightDir = normalize(vec3(_WorldSpaceLightPos0));
				}
				else {
					vec3 vertexToLightSource = vec3(_WorldSpaceLightPos0 - position);
					float dist = length(vertexToLightSource);
					lightDir = normalize(vertexToLightSource);
					attenuation = 1.0 / dist;
				}
				
				vec3 ambColor = vec3(gl_LightModel.ambient) * vec3(_Color);
				
				vec3 diffColor = attenuation * vec3(_LightColor0) * vec3(_Color) * max(0.0, dot(normalDirection, lightDir));
				
				vec3 specColor;
				if(dot(normalDirection, lightDir) < 0.0) {
					specColor = vec3(0.0,0.0,0.0);
				}
				else {
					specColor = attenuation * vec3(_LightColor0) * vec3(_SpecColor) * pow(max(0.0, dot(reflect(-lightDir,normalDirection),viewDirection)), _Shininess*32);
				}
				
	            vec3 reflectedDirection = reflect(-viewDirection, normalize(normalDirection));
	            gl_FragColor = vec4(ambColor*2 + diffColor*2 + specColor*2,1.0) + _ReflectColor * textureCube(_Cube, reflectedDirection);
	        }
	 
	        #endif
	 
	        ENDGLSL
		}
	}
}