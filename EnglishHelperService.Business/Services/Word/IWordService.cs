using EnglishHelperService.ServiceContracts;

namespace EnglishHelperService.Business
{
    public interface IWordService
    {
        Task<ReadWordByIdResponse> ReadById(long id);
        Task<ListWordResponse> List(long userId);
        Task<CreateWordResponse> Create(CreateWordRequest request);
        Task<UpdateWordResponse> Update(UpdateWordRequest request);
        Task<ResponseBase> Delete(long id);
        Task<ResponseBase> DeleteAll(long userId);
        Task<ListWordResponse> ResetResults(long userId);
        Task<ExportWordListToFileResponse> ExportWordListToTextFile(long userId);
        Task<ResponseBase> ImportWordListFromTextFile(ImportWordListFromTextFileRequest request);
        Task<ExportWordListToFileResponse> ExportWordListToExcelFile(long userId);
		Task<ResponseBase> ImportWordListFromExcelFile(ImportWordListFromExcelFileRequest request);
		
	}
}
