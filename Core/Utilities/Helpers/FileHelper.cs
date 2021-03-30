using Core.Utilities;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Helpers
{
    public class FileHelper
    {
        public static string Add(IFormFile formFile)
        {

            var result = NewPath(formFile);

                using (var stream = new FileStream(result, FileMode.Create))
                {
                    formFile.CopyTo(stream);
                }
            return NewPath(formFile);
        }
        public static IResult Delete(string path)
        {
            try
            {
                File.Delete(path);
                return new SuccessResult();
            }
            catch (Exception ex)
            {

                return new ErrorResult(ex.Message);
            }
        }
        public static string Update(string path,IFormFile formFile)
        {
            var result = NewPath(formFile);

            using (var stream = new FileStream(result, FileMode.Create))
            {
                formFile.CopyTo(stream);
            }
            File.Delete(path);
            return NewPath(formFile);

        }
        public static string NewPath(IFormFile formFile)
        {
            FileInfo fileInfo = new FileInfo(formFile.FileName);
            string ff = fileInfo.Extension;
            var newpath = Guid.NewGuid().ToString() + ff;
            var path = Environment.CurrentDirectory + @"\Images";
            return $@"{path}\{newpath}";
        }
    }
}
