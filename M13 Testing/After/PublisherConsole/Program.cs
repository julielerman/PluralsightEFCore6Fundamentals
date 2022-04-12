using PublisherConsole;

AddSomeAuthors();

void AddSomeAuthors()
{
    var authorList = new Dictionary<string, string>
    {
       { "Ruth","Ozeki" },
       { "Sofia", "Segovia" },
       { "Ursula K.", "LeGuin" },
       { "Hugh",  "Howey" },
       { "Isabelle", "Allende" }
    };
    var dl = new DataLogic();
    dl.ImportAuthors(authorList);
}



