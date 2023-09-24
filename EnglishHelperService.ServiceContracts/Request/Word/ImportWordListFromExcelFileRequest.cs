using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishHelperService.ServiceContracts
{

	/// <summary>
	/// Import word list from excel file request
	/// </summary>
	public class ImportWordListFromExcelFileRequest
	{
		public long UserId { get; set; }
		public XLWorkbook Workbook { get; set; }
	}
}
