using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Eixo;
    public float Max;
    public float Min;
    
    void Afastar(){
        if(transform.localPosition.z < Max){
            transform.Translate(0, 0, 1 * Time.deltaTime);
        }
    }
    void Aproximar(){
        if (transform.localPosition.z > Min){
            transform.Translate(0, 0, -1 * Time.deltaTime);
        }
    }
    void GirarEixoParaDireita(){
        Eixo.transform.Rotate(0, 360 * Time.deltaTime, 0);
    }
    void GirarEixoParaEsquerda(){
        Eixo.transform.Rotate(0, 360 * Time.deltaTime, 0);
    }
    void GirarCameraParaCima(){
        transform.Rotate( 360 * Time.deltaTime, 0, 0);
    }
    void GirarCameraParaBaixo(){
        transform.Rotate(360 * Time.deltaTime, 0, 0);
    }
}
