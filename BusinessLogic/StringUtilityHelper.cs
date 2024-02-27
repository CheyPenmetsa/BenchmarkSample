namespace BusinessLogic
{
    public class StringUtilityHelper
    {
        public string GetLastRowFromCSV(string csvContent)
        {
            if (string.IsNullOrEmpty(csvContent))
            {
                return string.Empty;
            }

            // Split the CSV content into rows
            string[] rows = csvContent.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            if (rows.Length == 0)
            {
                throw new ArgumentException("CSV content is empty");
            }

            // Get the last row
            string lastRow = rows.Last();

            return lastRow;
        }
    }
}
