namespace RetieveHolidaysFromGovermentOpenData
{
    public class RetieveHolidaysFromGovermentOpenData : IRetieveHolidaysFromGovermentOpenData
    {
        public async Task<(bool isSuccess, string errorMessageOrFileContent)> RetieveHolidaysFromGovermentOpenDataAsync(int year, FILETYPE fileType, bool isForGoogle)
        {
            try
            {
                var uri = "https://data.gov.tw/dataset/14718";
                var description = isForGoogle ? $"{year - 1911}年中華民國政府行政機關辦公日曆表_Google行事曆專用" : $"{year - 1911}年中華民國政府行政機關辦公日曆表";
                var pattern = $"description:\"{description}\"\\S+?{fileType}:\"(https:\\S+?)\"";
                var regEx = new System.Text.RegularExpressions.Regex(pattern);
                using (var client = new HttpClient())
                {
                    var htmlBuffer = await client.GetByteArrayAsync(uri);
                    var html = System.Text.Encoding.UTF8.GetString(htmlBuffer);
                    var fileURL = string.Empty;
                    var match = regEx.Match(html);
                    if (match.Success)
                    {
                        fileURL = match.Groups[1].Value.Replace("\\u002F", "/");
                        var fileContent = await GetFileContentAsync(fileURL);
                        return (true, fileContent);
                    }
                    else
                    {
                        return (false, $"無法取得{description}");
                    }
                }
            }
            catch 
            {
                throw;
            }
        }

        private async Task<string> GetFileContentAsync(string fileURL)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var fileBuffer = await client.GetByteArrayAsync(fileURL);
                    return System.Text.Encoding.UTF8.GetString(fileBuffer);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}