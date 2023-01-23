using Chess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Managers
{
    class SoundManager
    {
        public static SoundManager Instance 
        { 
            get 
            {
                if (_instance == null)
                    _instance = new SoundManager();
                return _instance;
            } 
        }
        private static SoundManager _instance;

        public void PlayMoveSound()
        {
            SoundPlayer simpleSound = new SoundPlayer(Helpers.PathResolve(Helpers.AssetsSoundFolder, "moveSound.wav"));
            simpleSound.Play();
        }
    }
}
