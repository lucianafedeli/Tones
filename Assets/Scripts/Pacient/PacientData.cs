using System;
using System.Collections.Generic;
using Tones.Sessions;

namespace Pacient
{
    [Serializable]
    public class PacientData
    {
        public int ID;
        public string firstName, lastName, DNI, birthDate, lastTestDate;
        public char gender;
        public bool enabled;
        public List<Classic> lastClassicSessions;
        public List<Experimental> lastExperimentalSessions;
        public List<Carhartt> carhartts;

        public PacientData(int ID, string firstName, string lastName, string DNI, string birthDate, char gender)
        {
            this.ID = ID;
            this.firstName = firstName;
            this.lastName = lastName;
            this.DNI = DNI;
            this.birthDate = birthDate;
            this.gender = gender;
            this.lastTestDate = "[Sin examen]";
            this.enabled = true;
        }

        public override string ToString()
        {
            return firstName + " " + lastName;
        }
    }
}
