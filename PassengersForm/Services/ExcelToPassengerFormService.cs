using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using Microsoft.VisualBasic;
using OfficeOpenXml;
using PassengersForm.Models.PassengerModels;
using PassengersForm.ViewModels;

namespace PassengersForm.Services;

public class ExcelToPassengerFormService : IExcelData<PassengerFormViewModel>
{
    public ExcelToPassengerFormService()
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
    }

    public IEnumerable<PassengerFormViewModel> FillCollectionFromExcel(string filePath)
    {
        var collection = new ObservableCollection<PassengerFormViewModel>();
        using ExcelPackage excelPackage = new ExcelPackage(filePath);
        var worksheet = excelPackage.Workbook.Worksheets[0];
        var skippedFormsCount = 0;
        var rowCount = worksheet.Dimension.Rows;

        for (var i = 1; i <= rowCount; i++)
        {
            var isEmptyRow = true;
            for (var j = 1; j <= 5; j++)
            {
                if (worksheet.Cells[i, j].Value == null) continue;
                isEmptyRow = false;
                break;
            }

            if (isEmptyRow) continue;
            var flightNumberColumn = worksheet.Cells[i, 1].Value != null ? worksheet.Cells[i, 1].Value.ToString() : "";
            var timeColumn = worksheet.Cells[i, 2].Value != null ? worksheet.Cells[i, 2].Value.ToString() : "";
            var firstNameColumn = worksheet.Cells[i, 3].Value != null ? worksheet.Cells[i, 3].Value.ToString() : "";
            var lastNameColumn = worksheet.Cells[i, 4].Value != null ? worksheet.Cells[i, 4].Value.ToString() : "";
            var patronymicNameColumn =
                worksheet.Cells[i, 5].Value != null ? worksheet.Cells[i, 5].Value.ToString() : "";

            
            
            DateTime timeValue;
            var isValidTime = DateTime.TryParseExact(timeColumn, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out timeValue);

            var isNumeric = int.TryParse(flightNumberColumn, out int flightNumber);



            if (string.IsNullOrEmpty(firstNameColumn) || !isValidTime || !isNumeric)
            {
                skippedFormsCount++;
                continue;
            }
                

            var form = new PassengerForm(firstNameColumn, lastNameColumn, patronymicNameColumn,
                int.Parse(flightNumberColumn), timeValue);

            collection?.Add(new PassengerFormViewModel(form));
        }

        MessageBox.Show($"Данные загружены. \nПропущено строк: {skippedFormsCount}");
        return collection;
    }
}
