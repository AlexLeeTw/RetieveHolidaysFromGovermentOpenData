namespace RetieveHolidaysFromGovermentOpenData
{
    public interface IRetieveHolidaysFromGovermentOpenData
    {
        Task<(bool isSuccess, string errorMessageOrFileContent)> RetieveHolidaysFromGovermentOpenDataAsync(int year, FILETYPE fileType, bool isForGoogle);
    }
}
