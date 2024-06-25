using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigJaula : MonoBehaviour
{
    [SerializeField] private bool _isInTrunk;
    private Inventory _myInventory;
    public GameObject _azul;
    private bool _azulOpen;
    public GameObject _rojo;
    private bool _rojoOpen;
    public GameObject _violeta;
    private bool _violetaOpen;
    public AudioSource audioSource;
    public AudioClip openSound;
    public AudioClip magicSound;
    public AudioClip noAccessSound;
    private bool onlyOneTime;
    public GameplayCanvasManager gameplayCanvas;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (_isInTrunk)
            {
                if (_myInventory.HasItemsy("orbe_azul") || _myInventory.HasItemsy("orbe_rojo") || _myInventory.HasItemsy("orbe_violeta"))
                {
                    if (_myInventory.HasItemsy("orbe_azul"))
                    {
                        _azul.SetActive(true);
                        _myInventory.DeleteOrbe("orbe_azul");
                        _azulOpen = true;
                    }

                    if (_myInventory.HasItemsy("orbe_rojo"))
                    {
                        _rojo.SetActive(true);
                        _myInventory.DeleteOrbe("orbe_rojo");
                        _rojoOpen = true;
                    }

                    if (_myInventory.HasItemsy("orbe_violeta"))
                    {
                        _violeta.SetActive(true);
                        _myInventory.DeleteOrbe("orbe_violeta");
                        _violetaOpen = true;
                    }
                    audioSource.PlayOneShot(magicSound);
                }
                else
                {
                    audioSource.PlayOneShot(noAccessSound);
                }
            }
        }

        if(_rojoOpen && _azulOpen && _violeta)
        {
            StartCoroutine("AbrirJaula");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _myInventory = other.GetComponent<Inventory>();
        if (_myInventory != null)
        {
            _isInTrunk = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _isInTrunk = false;
    }

    private IEnumerator AbrirJaula()
    {
        yield return new WaitForSeconds(3);
        audioSource.PlayOneShot(openSound);
        gameplayCanvas.onWin();
        Destroy(gameObject, 3f);

        

        //LLAMAR A LA FUNCION DE VICTORIA DE LA UI
    }
}
