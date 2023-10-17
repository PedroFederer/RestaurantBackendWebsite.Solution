using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace RestaurantBackendWebsite.Infra
{
	public interface IFileValidator
	{
		void Validate(HttpPostedFileBase file);
	}
	public class UploadFileHelper
	{
		public static string Save(HttpPostedFileBase file, string path, IFileValidator[] validators)
		{
			if (validators != null)
			{
				foreach (var validator in validators)
				{
					validator.Validate(file);
				}
			}
			if (file == null || file.ContentLength == 0) return string.Empty;

			string exe = Path.GetExtension(file.FileName);
			string fileName = Path.GetRandomFileName() + exe;
			string fullPath = Path.Combine(path, fileName);
			file.SaveAs(fullPath);

			return fileName;
		}
	}
}