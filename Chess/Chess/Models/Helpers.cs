using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace Chess.Models
{
    class Helpers
    {
        public static string AssetsImgFolder { get 
            { 
                if (assetsImgFolder == "")
                    assetsImgFolder = InitProfectFolder("Assets", "Images");
                return assetsImgFolder; 
            } 
        }
        private static string assetsImgFolder = "";

        public static string AssetsSoundFolder
        {
            get
            {
                if (assetsSoundFolder == "")
                    assetsSoundFolder = InitProfectFolder("Assets", "Sounds");
                return assetsSoundFolder;
            }
        }
        private static string assetsSoundFolder = "";

        private static string InitProfectFolder(params string[] folders)
        {
            List<string> execDirSplited = new List<string>(Directory.GetCurrentDirectory().Split('\\'));
            execDirSplited.RemoveRange(7, execDirSplited.Count - 7);
            return PathResolve(string.Join("\\", execDirSplited.ToArray()), folders) ;
        } 

        public static string PathResolve(string path, params string[] dirs)
        {
            string result = path;
            if (path[path.Length - 1] == '\\')
                result = result.Remove(path.Length - 1);
            for (int i = 0; i < dirs.Length; i++)
            {
                result += "\\" + dirs[i];
            }
            return result;
        }
    }
}
