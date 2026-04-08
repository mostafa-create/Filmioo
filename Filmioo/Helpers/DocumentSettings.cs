namespace Demo.PL.Helpers
{
    public class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string FolderName)
        {
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", FolderName);
            string FileName = $"{file.FileName}";
            string FilePath = Path.Combine(FolderPath, FileName);
            using var Fs = new FileStream(FilePath, FileMode.Create);
            file.CopyTo(Fs);

            return $"/files/{FolderName}/{FileName}";
        }
        public static void DeleteFile(string FileName, string FolderName)
        {
            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", FolderName, FileName);
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }

        }

    }
}
