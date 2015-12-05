Shader "HDR Bloom" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_HDRTex ("HDR Texture", 2D) = "white" {}
	}
	SubShader {
		Pass {
			GLSLPROGRAM

			uniform vec4 _Color;
			uniform sampler2D _HDRTex;
			uniform vec2 tc_offset[25];
			uniform int lodLevel;
			
			uniform vec3 _WorldSpaceCameraPos; 
	        uniform vec4 _WorldSpaceLightPos0;
	        uniform vec4 _LightColor0;
	        
	        uniform mat4 _Object2World; // model matrix
	        uniform mat4 _World2Object; // inverse model matrix

			varying vec4 vertOutTexCoords;
			varying vec4 vertOutFragColor;

			#ifdef VERTEX

			void main() {
				vec3 N = normalize(gl_NormalMatrix * gl_Normal);
			
				vec4 vertexPos = gl_ModelViewMatrix * gl_Vertex;
				vec3 vertexEyePos = vertexPos.xyz / vertexPos.w;
				
				vec3 L = normalize(_WorldSpaceLightPos0.xyz - vertexEyePos);
				
				float NdotL = max(0.0, dot(N,L));
				
				vertOutFragColor.rgb = _Color.rgb * NdotL;
    			vertOutFragColor.a = _Color.a;
			
				vertOutTexCoords = gl_MultiTexCoord0;
			
				gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
			}
			
			#endif

			#ifdef FRAGMENT

			void main() {
				const float bloomlimit = 1.0;
			
				vec4 hdrSample[25];
				for(int i = 0; i < 25; i++) {
					hdrSample[i] = texture2D (_HDRTex, vertOutTexCoords.st + tc_offset[i]);
				}
				
				vec4 color = hdrSample[12];
				vec4 blurColor = (
					(1.0 * (hdrSample[0] + hdrSample[4] + hdrSample[20] + 
							hdrSample[24])) +
		            (4.0 * (hdrSample[1] + hdrSample[3] + hdrSample[5] + hdrSample[9] +
		                    hdrSample[15] + hdrSample[19] + hdrSample[21] +
		                    hdrSample[23])) +
		            (7.0 * (hdrSample[2] + hdrSample[10] + hdrSample[14] +
		                    hdrSample[22])) +
		            (16.0 * (hdrSample[6] + hdrSample[8] + hdrSample[16] +
		                     hdrSample[18])) +
		            (26.0 * (hdrSample[7] + hdrSample[11] + hdrSample[13] +
		                     hdrSample[17])) +
		            (41.0 * (hdrSample[12]))
		            ) / 273.0;
		            
		        vec3 hdrBright = max (color.rgb - vec3(bloomlimit), vec3(0.0));
		        float bright = dot (hdrBright, vec3(1.0));
		        bright = smoothstep (0.0f, 0.5f, bright);
		        vec4 brightColor;
		        brightColor.rgb = mix (vec3(0.0), color.rgb, bright).rgb;
		        brightColor.a = 1.0; 
		               
		    	gl_FragColor = blurColor * _Color + brightColor;
			}
			
			#endif
			
			ENDGLSL
		}
	}
}
