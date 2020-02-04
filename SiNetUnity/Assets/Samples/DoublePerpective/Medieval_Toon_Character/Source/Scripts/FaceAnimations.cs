using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceAnimations : MonoBehaviour {
	public class eyeExpressions {
		public GameObject rightEye;
		public GameObject leftEye;
	}
	private int curRightEye;
	private int oldRightEye;
	
	private int curLeftEye;
	private int oldLeftEye;

	private int curMouth;
	private int oldMouth;

	//public eyeExpressions[] faceExpressions;
	public GameObject[] mouths;
	public GameObject[] leftEye;
	public GameObject[] rightEye;

	void Start () {
		curLeftEye=0;
		oldLeftEye=0;
		curRightEye=0;
		oldRightEye=0;
		curMouth=0;
		oldMouth=0;
		if (leftEye.Length>0){
			leftEye[0].SetActive(true);
		}
		if (rightEye.Length>0){
			rightEye[0].SetActive(true);
		}
		if (mouths.Length>0){
			mouths[0].SetActive(true);
		}
	}
				
}
