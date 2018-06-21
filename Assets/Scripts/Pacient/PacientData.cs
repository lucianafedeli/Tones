﻿using System;

[Serializable]
public class PacientData
{
    public ulong ID;
    public string firstName, lastName, DNI, birthDate;
    public char gender;

    public PacientData(ulong ID, string firstName, string lastName, string DNI, string birthDate, char gender)
    {
        this.ID = ID;
        this.firstName = firstName;
        this.lastName = lastName;
        this.DNI = DNI;
        this.birthDate = birthDate;
        this.gender = gender;
    }
}
