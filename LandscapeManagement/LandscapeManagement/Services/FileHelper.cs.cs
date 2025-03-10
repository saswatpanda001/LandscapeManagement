using Android.OS;
using LandscapeManagement.Droid;
using LandscapeManagement.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace LandscapeManagement.Droid
{
    public class FileHelper : IFileHelper
    {
        public string GetDownloadFolderPath()
        {
            // Returns the public Downloads folder on Android.
            return Environment.GetExternalStoragePublicDirectory(Environment.DirectoryDownloads).AbsolutePath;
        }
    }
}
