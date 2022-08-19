using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

namespace Engine
{
    public class HerbalismManager
    {
        private List<MedicalEventPrototype> _medicalEventPrototypes;
        private List<Substance> _substancePrototype;
        private List<HealItem> _healItemsPrototypes;
        private List<MedicalEventChance> _primaryDiseasesPrototypes;
        private List<MedicalEventChance> _dependentDiseasesPrototypes;
        private List<Patient> _patientsInHospital;
        private List<Patient> _patientsFree;

        private static HerbalismManager _instance;
        public static HerbalismManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new HerbalismManager();
                
                return _instance;
            }
        } 
        private HerbalismManager ()
        {
            _medicalEventPrototypes = new List<MedicalEventPrototype>();
            _substancePrototype = new List<Substance>();
            _healItemsPrototypes = new List<HealItem>();
            _primaryDiseasesPrototypes = new List<MedicalEventChance>();
            _dependentDiseasesPrototypes = new List<MedicalEventChance>();
            _patientsInHospital = new List<Patient>();
            _patientsFree = new List<Patient>();
        }

        public void LoadData()
        {
            try
            {
                IEnumerable<XElement> eventsNodesList =
                    Extras.Functions.LoadResourceXMLAsElements("data/events", "events", null);

                if (eventsNodesList != null)
                {
                    foreach (XElement element in eventsNodesList)
                    {
                        MedicalEventPrototype newEvent = new MedicalEventPrototype();
                        newEvent.LoadData(element);
                        _medicalEventPrototypes.Add(newEvent);
                    }
                }

                IEnumerable<XElement> chancesNodesList =
                    Extras.Functions.LoadResourceXMLAsElements("data/chances", "chances", null);

                if (chancesNodesList != null)
                {
                    foreach (XElement element in chancesNodesList)
                    {
                        MedicalEventChance newEventChance = new MedicalEventChance();
                        newEventChance.LoadData(element);
                        if (newEventChance.PreviousEventType == null)
                            _primaryDiseasesPrototypes.Add(newEventChance);
                        else
                            _dependentDiseasesPrototypes.Add(newEventChance);
                    }
                }


                IEnumerable<XElement> substancesNodesList =
                    Extras.Functions.LoadResourceXMLAsElements("data/substances", "substances", null);

                if (substancesNodesList != null)
                {
                    foreach (XElement element in eventsNodesList)
                    {
                        Substance newSubstance = new Substance();
                        newSubstance.LoadData(element);
                        _substancePrototype.Add(newSubstance);
                    }
                }
                
                IEnumerable<XElement> itemsNodesList =
                    Extras.Functions.LoadResourceXMLAsElements("data/items", "items", null);

                if (itemsNodesList == null) return;
                {
                    foreach (XElement element in itemsNodesList)
                    {
                        HealItem newItem = new HealItem();
                        newItem.LoadData(element, _substancePrototype);
                        _healItemsPrototypes.Add(newItem);
                    }
                }
            }
            catch (System.Exception exception)
            {
                Debug.LogError("Common exception: " + exception.Message);
            }
        }

        public MedicalEvent CreatePrimaryMedicalEvent()
        {
            MedicalEventChance drawnEvent = null;
            foreach (MedicalEventChance chance in _primaryDiseasesPrototypes)
            {
                if (Random.Range(0, 1f) < chance.Chance)
                    drawnEvent = chance;
            }

            if (drawnEvent != null)
            {
                float newIntensity = Random.Range(0.001f, 1f);
                float newExacerbation = Random.Range(0f, 2f) - 1f;
                return CreateMedicalEventBasedOnChance(drawnEvent, drawnEvent.NextLocalization, newIntensity, newExacerbation);
            }
            
            return null;
        }
        
        public List<MedicalEvent> ProcessMedicalEventStatus(MedicalEvent medicalEvent)
        {
            List<MedicalEvent> newEvents = new List<MedicalEvent>();
            foreach (MedicalEventChance chance in _dependentDiseasesPrototypes)
            {
                if ((chance.PreviousLocalization & medicalEvent.Localization) != 0
                    && chance.PreviousEventType == medicalEvent.Name && Random.Range(0, 1f) < chance.Chance)
                {
                    MedicalEvent newEvent = CreateMedicalEventBasedOnChance(chance,
                        chance.NextLocalization == BodyParts.Any ? medicalEvent.Localization : chance.NextLocalization,
                        medicalEvent.Intensity, medicalEvent.Exacerbation);
                }
            }
            
            medicalEvent.ProcessHourly();
            return newEvents;
        }
        
        private MedicalEvent CreateMedicalEventBasedOnChance (MedicalEventChance eventChance, BodyParts localization, float intensity, float exacerbation)
        {
            MedicalEventPrototype newPrototype = null;
            foreach (MedicalEventPrototype prototype in _medicalEventPrototypes)
            {
                if (prototype.Name == eventChance.NextEventType)
                    newPrototype = prototype;
            }

            if (newPrototype == null)
            {
                Debug.LogError("Can't find medical event pattern " + eventChance.NextEventType);
                return null;
            }

            MedicalEvent medicalEvent = new MedicalEvent(newPrototype);
            medicalEvent.Localization = localization;
            medicalEvent.Intensity = intensity;
            medicalEvent.Exacerbation = exacerbation;
            return medicalEvent;
        }
    }
}