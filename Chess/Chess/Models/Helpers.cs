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
                    assetsImgFolder = InitProfectFolder("Chess","Assets", "Images");
                return assetsImgFolder; 
            } 
        }
        private static string assetsImgFolder = "";

        public static string AssetsSoundFolder
        {
            get
            {
                if (assetsSoundFolder == "")
                    assetsSoundFolder = InitProfectFolder("Chess", "Assets", "Sounds");
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

        public static string CoordinatesToNotation(int i, int j)
        {
            string[] a = new string[8] { "a", "b", "c", "d", "e", "f", "g", "h" };
            return i + a[j];
        }

        public static (int, int) NotationToCoordinates(string notation)
        {
            List<string> a = new List<string> { "a", "b", "c", "d", "e", "f", "g", "h" };
            return (int.Parse(notation[0].ToString()), a.FindIndex(x => x == notation[1].ToString()));
        }
    }
}
