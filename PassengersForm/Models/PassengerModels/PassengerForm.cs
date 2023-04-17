using System;

namespace PassengersForm.Models.PassengerModels;

public class PassengerForm
{
    private string _firstName;
    private string _lastName;
    private string _patronymicName;
    private int _flightNumber;
    private DateTime _flightTime;

    public PassengerForm(string firstName, string lastName, string patronymicName, int flightNumber, DateTime flightTime)
    {
        _firstName = firstName;
        _lastName = lastName;
        _patronymicName = patronymicName;
        _flightNumber = flightNumber;
        _flightTime = flightTime;
    }

    public string FullName => $"{_firstName} {_lastName} {_patronymicName}";

    public string FlightNumber => _flightNumber.ToString();
    public string FlightTime => _flightTime.ToShortTimeString();
}