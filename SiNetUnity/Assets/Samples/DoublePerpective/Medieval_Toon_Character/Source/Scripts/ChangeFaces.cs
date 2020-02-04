using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFaces : MonoBehaviour {
		public PlayFaceAnimations faceAnimations;
		public int totalEyeExpressions=7;
		public int totalMouthExpressions=7;
		public GameObject[] charPlaceFaceAnim;
		private int curCharInt = 0;

			void Start(){
				charPlaceFaceAnim [0].gameObject.SetActive (true);
				faceAnimations = charPlaceFaceAnim [0].GetComponent<PlayFaceAnimations>();
			}
			public void NextLeftEyeAnim(){
				if (faceAnimations != null) {
						if ((int)faceAnimations.leftEye < totalEyeExpressions)
								faceAnimations.leftEye += 1;
						else
								faceAnimations.leftEye = 0;
				}
			}
			public void NextRightEyeAnim(){
				if (faceAnimations != null) {
						if ((int)faceAnimations.rightEye < totalEyeExpressions)
								faceAnimations.rightEye += 1;
						else
								faceAnimations.rightEye = 0;
				}
			}

			public void NextMouthAnim(){
				if (faceAnimations != null) {
						if ((int)faceAnimations.Mouth < totalMouthExpressions)
								faceAnimations.Mouth += 1;
						else
								faceAnimations.Mouth = 0;
				}
			}

			public void ChangeCurrentActiveCharacter(){
				charPlaceFaceAnim [curCharInt].gameObject.SetActive (false);
				if (curCharInt < charPlaceFaceAnim.Length-1) {
						curCharInt += 1;
				} else {
						curCharInt = 0;
				}
				charPlaceFaceAnim [curCharInt].gameObject.SetActive (true);
				faceAnimations = charPlaceFaceAnim [curCharInt].GetComponent<PlayFaceAnimations>();
			}

}
