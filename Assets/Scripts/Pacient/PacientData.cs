﻿using System;
using System.Collections.Generic;
using Tones.Sessions;

namespace Pacient
{
    [Serializable]
    public class PacientData
    {
        public ulong ID;
        public string firstName, lastName, DNI, birthDate;
        public char gender;
        public bool enabled;
        public List<Manual> lastSessions;
        public Carhartt[] carhartts;

        public PacientData(ulong ID, string firstName, string lastName, string DNI, string birthDate, char gender)
        {
            this.ID = ID;
            this.firstName = firstName;
            this.lastName = lastName;
            this.DNI = DNI;
            this.birthDate = birthDate;
            this.gender = gender;
            this.enabled = true;
        }

        public override string ToString()
        {
            return firstName + " " + lastName;
        }
    }
}
