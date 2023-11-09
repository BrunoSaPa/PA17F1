public partial class Product
{
    public decimal? Cost { get; set; }
}

namespace WorkingWithEFCore
{
    public static class ProductExtensions
    {
        public static double CalculateMean<TSource>(this IQueryable<TSource> source, Func<TSource, dynamic> selector)
        {
            var selectedData = source.Select(selector);
            var firstItem = selectedData.FirstOrDefault(); // Obtén el primer elemento

            if (firstItem != null && firstItem is string)
            {
                throw new NotSupportedException("Mean calculation not supported for string values.");
            }

            if (firstItem != null && firstItem is int)
            {
                return selectedData.Average(item => (int)item);
            }

            if (firstItem != null && firstItem is double)
            {
                return selectedData.Average(item => (double)item);
            }

            if (firstItem != null && firstItem is decimal)
            {
                return (double)selectedData.Average(item => (decimal)item);
            }

            if (firstItem != null && firstItem is bool)
            {
                return Convert.ToDouble(selectedData.Average(item => Convert.ToInt32(item)));
            }

            throw new NotSupportedException("Unsupported data type for Mean calculation.");
        }


        public static dynamic CalculateMedian<TSource>(this IQueryable<TSource> source, Func<TSource, dynamic> selector)
        {
            var orderedData = source.Select(selector).OrderBy(x => x).ToList();
            int count = orderedData.Count;
            int midpoint = count / 2;
            dynamic median;
            if (count % 2 == 0)
            {
                median = (orderedData[midpoint - 1] + orderedData[midpoint]) / 2;
            }
            else
            {
                median = orderedData[midpoint];
            }
            return median;
        }

        public static dynamic CalculateMode<TSource>(this IQueryable<TSource> source, Func<TSource, dynamic> selector)
        {
            var query = source.GroupBy(selector)
                             .Select(g => new { Value = g.Key, Count = g.Count() })
                             .OrderByDescending(x => x.Count);
            int maxCount = query.First().Count;
            var modes = query.TakeWhile(x => x.Count == maxCount).Select(x => x.Value).ToList();

            var firstItem = modes.FirstOrDefault(); // Obtén el primer elemento de la moda

            if (firstItem != null && firstItem is int)
            {
                return modes.Cast<int>().ToList();
            }

            if (firstItem != null && firstItem is double)
            {
                return modes.Cast<double>().ToList();
            }

            if (firstItem != null && firstItem is decimal)
            {
                return modes.Cast<decimal>().ToList();
            }

            if (firstItem != null && firstItem is string)
            {
                return modes.Cast<string>().ToList();
            }

            if (firstItem != null && firstItem is bool)
            {
                return modes.Cast<bool?>().ToList();
            }

            throw new NotSupportedException("Unsupported data type for Mode calculation.");
        }


        public static double CalculateMeanOfString<TSource>(this IQueryable<TSource> source, Func<TSource, string> selector)
        {
            var selectedData = source.Select(selector).ToList();
            if (selectedData.Count == 0)
            {
                return 0;
            }

            double totalLength = 0;
            foreach (var item in selectedData)
            {
                totalLength += item.Length; 
            }

            return totalLength / selectedData.Count; 
        }
    }
}