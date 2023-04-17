using CommunityToolkit.Mvvm.ComponentModel;
using PassengersForm.Models.PassengerModels;

namespace PassengersForm.ViewModels;

public class PassengerFormViewModel : ObservableObject
{
    private readonly PassengerForm _passengerForm;
    public PassengerFormViewModel(PassengerForm passengerForm)
    {
        _passengerForm = passengerForm;
    }

    public string? FlightNumber => _passengerForm.FlightNumber;
    public string? FlightTime => _passengerForm.FlightTime;
    public string PassengerName => _passengerForm.FullName;
}