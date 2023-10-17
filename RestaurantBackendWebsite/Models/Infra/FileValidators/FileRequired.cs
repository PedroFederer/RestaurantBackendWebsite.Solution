using RestaurantBackendWebsite.Infra;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace RestaurantBackendWebsite.Infra.FileValidators
{
	public class FileRequired : IFileValidator
	{
		public void Validate(HttpPostedFileBase file)
		{
			if (file == null || file.ContentLength == 0)
			{
				throw new Exception("請選擇檔案");
			}
		}
	}
}
public class ImageValidator : IFileValidator
{
	public void Validate(HttpPostedFileBase file)
	{
		if (file == null || file.ContentLength == 0) return;

		string[] imgExts = new string[] { ".jpg", ".png", ".gif" };
		string ext = Path.GetExtension(file.FileName).ToLower();
		if (!imgExts.Contains(ext))
		{
			throw new Exception($"請選擇圖片檔，你上傳的檔案為" + ext);
		}
	}
}