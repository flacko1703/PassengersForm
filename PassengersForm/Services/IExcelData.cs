using System.Collections.Generic;

namespace PassengersForm.Services;

public interface IExcelData<out T>
{
    IEnumerable<T> FillCollectionFromExcel(string filePath);
}