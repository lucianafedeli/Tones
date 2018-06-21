///—————————————————————–
///   File: HistoriaClinicaInjector.cs
///   Author: Luciano Donati
///   me@lucianodonati.com	www.lucianodonati.com
///   Last edit: 20-Jun-18
///   Description: 
///—————————————————————–

using System.Collections.Generic;
using UnityEngine;
public class HistoriaClinicaInjector : MonoBehaviour
{
    [SerializeField]
    private PacientRow pacientRowPrefab = null;

    private List<PacientRow> rows = null;

    private void Start()
    {
        rows = new List<PacientRow>();
        AddPacients();
    }

    private void AddPacients()
    {
        Dictionary<ulong, PacientData> pacientsData = DataManager.Instance.GetPacientsData();
        foreach (var pacientData in pacientsData)
        {
            PacientRow newPacient = Instantiate(pacientRowPrefab, transform);
            newPacient.LoadData(
                pacientData.Value.firstName + ' ' + pacientData.Value.lastName + " - " + pacientData.Value.DNI,
                DataManager.Instance.GetLatestTest(pacientData.Key)
                );

            rows.Add(newPacient);
        }
    }

    public void SortBy(string sortString)
    {
        foreach (var element in rows)
        {
            if (sortString == "" || element.pacientData.text.Contains(sortString))
                element.gameObject.SetActive(true);
            else
                element.gameObject.SetActive(false);
        }
    }
}
