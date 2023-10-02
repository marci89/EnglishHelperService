using ClosedXML.Excel;

namespace EnglishHelperService.ServiceContracts
{

    /// <summary>
    /// Import word list from excel file request
    /// </summary>
    public class ImportWordListFromExcelFileRequest
    {
        /// <summary>
        /// User id
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// Excel object
        /// </summary>
        public XLWorkbook Workbook { get; set; }
    }
}
