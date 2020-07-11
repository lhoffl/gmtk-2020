using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellPool : MonoBehaviour {

    [SerializeField]
    private Spell _spellPrefab;

    [SerializeField]
    private int _poolSize;

    private List<Spell> _freeList;
    private List<Spell> _usedList;

    public static PlayerSpellPool Instance { get; private set; }

    void Awake() {

        Instance = this;

        _freeList = new List<Spell>(_poolSize);
        _usedList = new List<Spell>(_poolSize);

        for(int i = 0; i < _poolSize; i++) {

            Spell spell = Instantiate(_spellPrefab, transform);
            spell.gameObject.SetActive(false);
            _freeList.Add(spell);
        }
    }

    public Spell GetSpell() {
        if(_freeList.Count <= 0) {
            return null;
        }

        Spell spell = _freeList[_freeList.Count - 1];
        _freeList.RemoveAt(_freeList.Count - 1);
        _usedList.Add(spell);

        return spell;
    }

    public void ReturnSpell(Spell spell) {
        _usedList.Remove(spell);
        _freeList.Add(spell);

        Transform spellTransform = spell.transform;
        spellTransform.parent = transform;
        spellTransform.localPosition = Vector3.zero;
        spell.gameObject.SetActive(false);        
    }

}
