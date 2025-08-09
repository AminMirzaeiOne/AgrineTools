namespace AgrineCore.Actions
{
    public class AppsDefinitions
    {
        public enum AppsActionTypes
        {
            None, Start, Exit
        }

        public enum AppsWindowStatus
        {
            Normal, Maximize, Minimize
        }

        public AppsActionTypes ActionType { get; set; } = AppsActionTypes.None;
        public AppsWindowStatus AppWindowStatus { get; set; } = AppsWindowStatus.Normal;
        public int ActionWait { get; set; } = 1;

        public struct TopApps
        {
            public enum Windows
            {
                None, Notepad, Wordpad, Calculator, ControlPanel, Camera, Paint, Explorer, Terminal, PowerShell
            }

            public enum Microsoft
            {
                Mail, Maps, Calendar, Clock
            }

            public enum Office
            {
                Word, PowerPoint, Excel, Access,
            }

            public enum Adobe
            {
                Photoshop, Illustrator, XD, InDesign, Premiere, Dreamweaver, AfterEffects, Lightroom
            }

            public enum WebBrowser
            {
                Edge, Chrome, Firefox, Opera, Vivaldi, Brave
            }

            public enum CodeEditor
            {
                NotepadPlusPlus, VSCode, Atom
            }

            public enum IDE
            {
                QtCreator, VisualStudio, CLion
            }
        }

    }
}

