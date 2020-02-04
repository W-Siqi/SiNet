using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFaceAnimations : MonoBehaviour {
	public enum Eyes_Expressions{Happy=0,Mad=1,Sad=2,Tired=3,Closed=4,Closed_happy=5,Closed_smile=6,Closed_mad=7};
	public enum Mouth_Expressions{Happy_open=0,Terrified_open=1,Surprised_open=2,Surprised2_open=3,Unconcerned_closed=4,Sad_closed=5,Happy_closed=6,Cute=7};

	public Eyes_Expressions leftEye;
	public Eyes_Expressions rightEye;
	public Mouth_Expressions Mouth;

	public FaceAnimations faceAnims;
	// Use this for initialization
	void Start () {
		faceAnims=gameObject.GetComponent<FaceAnimations>();
	}
	
	// Update is called once per frame
	void Update () {
		if (faceAnims.leftEye.Length > 0) {
			if (leftEye == Eyes_Expressions.Happy) 	faceAnims.leftEye[0].SetActive (true);
			else	faceAnims.leftEye[0].SetActive (false);

			if (leftEye == Eyes_Expressions.Mad) 	faceAnims.leftEye[1].SetActive (true);
			else	faceAnims.leftEye[1].SetActive (false);

			if (leftEye == Eyes_Expressions.Sad) 	faceAnims.leftEye[2].SetActive (true);
			else	faceAnims.leftEye[2].SetActive (false);

			if (leftEye == Eyes_Expressions.Tired) 	faceAnims.leftEye[3].SetActive (true);
			else	faceAnims.leftEye[3].SetActive (false);

			if (leftEye == Eyes_Expressions.Closed) 	faceAnims.leftEye[4].SetActive (true);
			else	faceAnims.leftEye[4].SetActive (false);

			if (leftEye == Eyes_Expressions.Closed_happy) 	faceAnims.leftEye[5].SetActive (true);
			else	faceAnims.leftEye[5].SetActive (false);

			if (leftEye == Eyes_Expressions.Closed_smile) 	faceAnims.leftEye[6].SetActive (true);
			else	faceAnims.leftEye[6].SetActive (false);

			if (leftEye == Eyes_Expressions.Closed_mad) 	faceAnims.leftEye[7].SetActive (true);
			else	faceAnims.leftEye[7].SetActive (false);
		}

		if (faceAnims.leftEye.Length > 0) {
			if (rightEye == Eyes_Expressions.Happy) 	faceAnims.rightEye[0].SetActive (true);
			else	faceAnims.rightEye[0].SetActive (false);

			if (rightEye == Eyes_Expressions.Mad) 	faceAnims.rightEye[1].SetActive (true);
			else	faceAnims.rightEye[1].SetActive (false);

			if (rightEye == Eyes_Expressions.Sad) 	faceAnims.rightEye[2].SetActive (true);
			else	faceAnims.rightEye[2].SetActive (false);

			if (rightEye == Eyes_Expressions.Tired) 	faceAnims.rightEye[3].SetActive (true);
			else	faceAnims.rightEye[3].SetActive (false);

			if (rightEye == Eyes_Expressions.Closed) 	faceAnims.rightEye[4].SetActive (true);
			else	faceAnims.rightEye[4].SetActive (false);

			if (rightEye == Eyes_Expressions.Closed_happy) 	faceAnims.rightEye[5].SetActive (true);
			else	faceAnims.rightEye[5].SetActive (false);

			if (rightEye == Eyes_Expressions.Closed_smile) 	faceAnims.rightEye[6].SetActive (true);
			else	faceAnims.rightEye[6].SetActive (false);

			if (rightEye == Eyes_Expressions.Closed_mad) 	faceAnims.rightEye[7].SetActive (true);
			else	faceAnims.rightEye[7].SetActive (false);
		}

		if(faceAnims.mouths.Length>0){
				if (Mouth==Mouth_Expressions.Happy_open)faceAnims.mouths[0].SetActive(true);
				else faceAnims.mouths[0].SetActive(false);
				if (Mouth==Mouth_Expressions.Terrified_open)faceAnims.mouths[1].SetActive(true);
				else faceAnims.mouths[1].SetActive(false);
				if (Mouth==Mouth_Expressions.Surprised_open)faceAnims.mouths[2].SetActive(true);
				else faceAnims.mouths[2].SetActive(false);
				if (Mouth==Mouth_Expressions.Surprised2_open)faceAnims.mouths[3].SetActive(true);
				else faceAnims.mouths[3].SetActive(false);
				if (Mouth==Mouth_Expressions.Unconcerned_closed)faceAnims.mouths[4].SetActive(true);
				else faceAnims.mouths[4].SetActive(false);
				if (Mouth==Mouth_Expressions.Sad_closed)faceAnims.mouths[5].SetActive(true);
				else faceAnims.mouths[5].SetActive(false);
				if (Mouth==Mouth_Expressions.Happy_closed)faceAnims.mouths[6].SetActive(true);
				else faceAnims.mouths[6].SetActive(false);
				if (Mouth==Mouth_Expressions.Cute)faceAnims.mouths[7].SetActive(true);
				else faceAnims.mouths[7].SetActive(false);

		}
	}
}
