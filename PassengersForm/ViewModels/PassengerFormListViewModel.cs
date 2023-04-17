using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using OfficeOpenXml;
using PassengersForm.Models;
using PassengersForm.Models.PassengerModels;
using PassengersForm.Services;

namespace PassengersForm.ViewModels;

public partial class PassengerFormListViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<PassengerFormViewModel> _passengerForms;

    private IDialogService _dialogService;
    private IExcelData<PassengerFormViewModel> _excelData;

    public PassengerFormListViewModel(IDialogService dialogService, IExcelData<PassengerFormViewModel> excelData)
    {
        _dialogService = dialogService;
        _excelData = excelData;
        _passengerForms = new();
    }

    

   
    [RelayCommand]
    private async Task ReadExcelFile()
    {
        if(!_dialogService.OpenFileDialog("Excel Files (*.xlsx)|*.xlsx")) return;
        var excelData = _excelData.FillCollectionFromExcel(_dialogService.FilePath);
        
        _passengerForms.Clear();
        foreach (var data in excelData)
        {
            _passengerForms.Add(data);
        }

        await Task.CompletedTask;
    }
}