using System;
using signals.Models;

namespace signals.Services;

public interface IDataService
{
    Task<IList<StockItem>> FetchData();

}
