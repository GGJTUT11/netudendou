using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class StageCreator : MonoBehaviour {

    public TextAsset textAsset;

    //配置するオブジェクト
    public GameObject[] block;
	[SerializeField] private GameObject weathenium_0;
	[SerializeField] private GameObject weathenium_100;
	[SerializeField] private GameObject windZoneObj;

    public Vector3 createPos;

    public Vector3 spaceScale = new Vector3(1.0f,1.0f,0f);

	Quaternion quaternion01 = Quaternion.Euler(0, 0, 0);
	Quaternion quaternion02 = Quaternion.Euler(90, 0, 0);
	Quaternion quaternion03 = Quaternion.Euler(180, 0, 0);

    void Start () {
        CreateStage(createPos);

        createPos = Vector3.zero;
    }

    void CreateStage(Vector3 pos){

        Vector3 originPos = pos;
        string stageTextData = textAsset.text;

        foreach(char c in stageTextData){

            GameObject obj = null;

            if(c == '0'){
				int random = Random.Range (0, block.Length);
				switch(random){
				case 0:
					obj = Instantiate(block[random], pos, quaternion01) as GameObject;
					break;
				case 1:
					obj = Instantiate(block[random], pos, quaternion02) as GameObject;
					break;	
				case 2:
					obj = Instantiate(block[random], pos, quaternion03) as GameObject;
					break;
				}
				obj.name = block[random].name;
				pos.x += spaceScale.x;
            }else if(c == '1'){
				obj = Instantiate(weathenium_0, pos, Quaternion.identity) as GameObject;
				pos.x += spaceScale.x;
			}else if(c == '2'){
				obj = Instantiate(weathenium_100, pos, Quaternion.identity) as GameObject;
				pos.x += spaceScale.x;
			}else if(c == '3'){
				obj = Instantiate(windZoneObj, pos, Quaternion.identity) as GameObject;
				pos.x += spaceScale.x;
			}else if(c == '4'){
				obj = Instantiate(windZoneObj, pos, Quaternion.identity) as GameObject;
				pos.x += spaceScale.x;
			}else if(c == '\n'){
                pos.y -= spaceScale.y;
                pos.x = originPos.x;
			}else if(c == ' '){
                pos.x += spaceScale.x;
            }
        }
    }
}