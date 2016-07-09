using System.Windows.Forms;

namespace WindowsFormsApplication1.Core.Engines
{
    using System;
    using StateManager;
    using Interfaces;
    using Sound;

    public class CoreGameEngine
    {
        private readonly ISound sound = new Sound();

        public bool IsRunning { get; private set; }

        //Singleton patern
        private static CoreGameEngine instance;

        public static CoreGameEngine Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CoreGameEngine();
                }

                return instance;
            }
        }

        public void Run()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Display());
            //AdjustSettings();

            //StateManager.Instance.StartState(StateConstants.BeginGame);
        }

        private void AdjustSettings()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            //Console.SetBufferSize(90, 45);
            Console.ForegroundColor = ConsoleColor.Cyan;
            //Console.SetWindowSize(90, 45);
            //Console.SetWindowPosition(90, 45);
            Console.CursorVisible = false;
            StartMusic(SoundEffects.DefaultTheme);
            Console.Clear();
        }

        private void StartMusic(SoundEffects stage)
        {
            sound.SFX(stage);
        }
    }
}

