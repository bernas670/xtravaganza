using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ter interfaces para o estilo/som/animation e cada arma tem o seu metodo especifico >> no shooter é so chamar que funciona para qq arma */
public class CerelacKiller : Weapon {
    public void Awake(){
        _damage = 10;
        _range = 100f;
    }
}
