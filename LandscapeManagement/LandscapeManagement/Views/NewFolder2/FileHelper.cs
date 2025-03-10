
using LandscapeManagement.Droid;
using LandscapeManagement.Services;
using Xamarin.Forms;
using LandscapeManagement.Services;


[assembly: Dependency(typeof(FileHelper))]
namespace LandscapeManagement.Droid
{
    public class FileHelper : IFileHelper
    {
        public string GetDownloadFolderPath()
        {
            return Environment.GetExternalStoragePublicDirectory(Environment.DirectoryDownloads).AbsolutePath;
        }
    }
}
