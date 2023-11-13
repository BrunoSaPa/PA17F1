using Microsoft.EntityFrameworkCore.Diagnostics;

namespace WorkingWithEFCore
{
    class Program
    {
        static void Main()
        {
            Clear();
            using (Northwind db = new Northwind())
            {
                var products = db.Products;

                #region SupplierID
                double meanIdSup = products.CalculateMean(p => (int)p.SupplierId);
                double medianIdSup = products.CalculateMedian(p => (int)p.SupplierId);
                var modeIdSup = products.CalculateMode(p => (int)p.SupplierId);
                string ListModeId = "";
                WriteLine("| {0,-15} | {1,-8:0.00} | {2,-8:0.00} | {3}",
                "Categoria", "Media", "Mediana", "Moda");
                WriteLine("----------------------------------------------------");
                if (modeIdSup != null)
                {
                    foreach (var mode in modeIdSup)
                    {
                        ListModeId += mode;
                        ListModeId += "  ";
                    }
                }

                  WriteLine("| {0,-15} | {1,-8:0.00} | {2,-8:0.00} | {3}",
                "SupplierId", meanIdSup, medianIdSup,ListModeId);
                #endregion

                #region CategoryID

                double meanIdC = products.CalculateMean(p => (int)p.CategoryId);
                double medianIdC = products.CalculateMedian(p => (int)p.CategoryId);
                var modeIdC = products.CalculateMode(p => (int)p.CategoryId);
                string ListModeIDc = "";
                if (modeIdC != null)
                {
                    foreach (var mode in modeIdC)
                    {
                        ListModeIDc += mode;
                        ListModeIDc += "  ";
                    }
                }
                WriteLine("| {0,-15} | {1,-8:0.00} | {2,-8:0.00} | {3}",
                "CategoryId", meanIdC, medianIdC,ListModeIDc);
                #endregion

                #region QuantityPerUnit
                double meanQ = products.CalculateMeanOfString(p => p.Quantity);
                var medianQ = products.CalculateMedian(p => p.Quantity);
                var modeQ = products.CalculateMode(p => p.Quantity) as List<string>;
                string ListModeQ = "";
                if (modeQ != null)
                {
                    foreach (var mode in modeQ)
                    {
                        ListModeQ += mode;
                        ListModeQ += "  ";
                    }
                }
                WriteLine("| {0,-15} | {1,-8:0.00} | {2,-8:0.00} | {3}",
                "QuantityPerUnit", meanQ, medianQ,ListModeQ);
                #endregion

                #region Cost
                double meanCost = products.CalculateMean(p => (double)p.Cost);
                double medianCost = products.CalculateMedian(p => (double)p.Cost);
                var modeCost = products.CalculateMode(p => (double)p.Cost);
                string ListModeCost = "";
                if (modeCost != null)
                {
                    foreach (var mode in modeCost)
                    {
                        ListModeCost += mode;
                        ListModeCost += "  ";
                    }
                }
                WriteLine("| {0,-15} | {1,-8:0.00} | {2,-8:0.00} | {3}",
                "Cost", meanCost, medianCost,ListModeCost);
                #endregion

                #region Stock
                double meanStock = products.CalculateMean(p => (double)p.Stock);
                double medianStock = products.CalculateMedian(p => (double)p.Stock);
                var modeStock = products.CalculateMode(p => (double)p.Stock);
                string ListModeStock = "";
                if (modeStock != null)
                {
                    foreach (var mode in modeStock)
                    {
                        ListModeStock += mode;
                        ListModeStock += "  ";
                    }
                }
                WriteLine("| {0,-15} | {1,-8:0.00} | {2,-8:0.00} | {3}",
                "Stock", meanStock, medianStock,ListModeStock);
                #endregion

                #region UnitsonOrder
                double meanUnitsInOrder = products.CalculateMean(p => (double)p.UnitsOnOrder);
                double medianUnitsInOrder = products.CalculateMedian(p => (double)p.UnitsOnOrder);
                var modeUnitsInOrder = products.CalculateMode(p => (double)p.UnitsOnOrder);
                string ListModeUnitsInOrder = "";
                if (modeUnitsInOrder != null)
                {
                    foreach (var mode in modeUnitsInOrder)
                    {
                        ListModeUnitsInOrder += mode;
                        ListModeUnitsInOrder += "  ";
                    }
                }
                WriteLine("| {0,-15} | {1,-8:0.00} | {2,-8:0.00} | {3}",
                "UnitsOnOrder", meanUnitsInOrder, medianUnitsInOrder,ListModeUnitsInOrder);
                #endregion

                #region ReordLevel
                double meanReorderLevel = products.CalculateMean(p => (double)p.ReorderLevel);
                double medianReorderLevel = products.CalculateMedian(p => (double)p.ReorderLevel);
                var modeReorderLevel = products.CalculateMode(p => (double)p.ReorderLevel);
                string ListModeLevel = "";
                if (modeReorderLevel != null)
                {
                    foreach (var mode in modeReorderLevel)
                    {
                        ListModeLevel += mode;
                        ListModeLevel += "  ";
                    }
                }
                WriteLine("| {0,-15} | {1,-8:0.00} | {2,-8:0.00} | {3}",
                "ReorderLevel", meanReorderLevel, medianReorderLevel,ListModeLevel);
                #endregion

                #region Discontinued
                var meanDiscontinued = products.CalculateMean(p => p.Discontinued == true ? 1 : 0);
                var medianDiscontinued = products.CalculateMedian(p => p.Discontinued == true ? 1 : 0);
                var modeDiscontinued = products.CalculateMode(p => p.Discontinued == true ? 1 : 0);
                string ListModeDisc = "";
                string state,state2;
                if (medianDiscontinued == 0){
                    state = "False";
                }
                else {
                    state = "True";
                }
                if (modeDiscontinued != null)
                {
                    foreach (var mode in modeDiscontinued)
                    {
                        ListModeDisc += mode;
                        ListModeDisc += "  ";
                    }
                }
                if (ListModeDisc == "0  "){
                    state2 = "False";
                }
                else {
                    state2 = "True";
                }
                WriteLine("| {0,-15} | {1,-8:0.00} | {2,-8:0.00} | {3}",
                "Discontinued", meanDiscontinued, state,state2);
                #endregion

                #region ProductID
                double meanId = products.CalculateMean(p => (int)p.ProductId);
                double medianId = products.CalculateMedian(p => (int)p.ProductId);
                var modeId = products.CalculateMode(p => (int)p.ProductId);
                string listModeID = "";
                if (modeId != null)
                {
                    foreach (var mode in modeId)
                    {
                        listModeID += mode;
                        listModeID += "  ";
                    }
                }
                WriteLine("| {0,-15} | {1,-8:0.00} | {2,-8:0.00} | {3}",
                "ProductID", meanId, medianId,listModeID);
                #endregion

                #region ProductName
                double meanNameLength = products.CalculateMeanOfString(p => p.ProductName);
                var medianName = products.CalculateMedian(p => p.ProductName);
                var modeProductName = products.CalculateMode(p => p.ProductName) as List<string>;
                string ListModeProductName = "";
                if (modeProductName != null)
                {
                    foreach (var mode in modeProductName)
                    {
                        ListModeProductName += mode;
                        ListModeProductName += "  ";
                    }
                }
                WriteLine("| {0,-15} | {1,-8:0.00} | {2,-8:0.00} | {3}",
                "ProductName", meanNameLength, medianName,ListModeProductName);
                #endregion

            }
        }
    }
}
