using AMing.Plugins.Core.Modules;
using System.Collections.Generic;
using System.Linq;
using AMing.Plugins.Core.Extensions;
using System.Windows.Media;
using System.Windows;
using System;

namespace AMing.SoundNotifier.Modules
{
    public class SoundsModules : ModulesBase
    {
        #region Current

        private static SoundsModules _current = new SoundsModules();

        public static SoundsModules Current
        {
            get { return _current; }
            set { _current = value; }
        }

        #endregion

        public override void Initialize()
        {
            base.Initialize();
            InitPublicModules();
        }

        #region method

        private static readonly string soundFilePath = System.IO.Path.Combine(
            Environment.CurrentDirectory,
            "Plugins",
            "sounds",
            "{1}.{0}");

        MediaPlayer mediaPlayer;

        public void Play(string filename, Action<Exception> failed = null)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                try
                {
                    var path = string.Format(soundFilePath, "wav", filename);
                    if (!System.IO.File.Exists(path))
                    {
                        path = string.Format(soundFilePath, "mp3", filename);
                    }
                    if (!System.IO.File.Exists(path))
                    {
                        throw new System.IO.FileNotFoundException();
                    }
                    if (mediaPlayer == null)
                    {
                        mediaPlayer = new MediaPlayer { Volume = 1 };
                    }
                    mediaPlayer.Open(new Uri(path, UriKind.Absolute));
                    mediaPlayer.Play();
                }
                catch (Exception ex)
                {
                    AMing.Plugins.Core.GenericMessager.Current.SendToException(ex);
                    if (failed != null)
                        failed(ex);
                }
            }));
        }


        public void Notify(Action<Exception> failed = null)
        {
            this.Play("notify", failed);
        }

        public void Warning(Action<Exception> failed = null)
        {
            this.Play("warning", failed);
        }

        #endregion

        #region PublicModules

        private void InitPublicModules()
        {
            AMing.Plugins.Core.GenericMessager.Current.RegisterForNotification(this, obj => Notify());
            AMing.Plugins.Core.GenericMessager.Current.RegisterForWarning(this, obj => Warning());
        }

        #endregion


        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
